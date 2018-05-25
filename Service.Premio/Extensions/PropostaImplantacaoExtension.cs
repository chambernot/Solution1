using System;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos.Enum;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class PropostaImplantacaoExtension
    {
        public static void Validar(this IProposta self)
        {
            var identificadorValidator = Assertion.GreaterThan(self.Identificador, default(Guid), "O Identificador não pode ser nulo.");
            var dataAssinaturaValidator = Assertion.GreaterThan(self.DataAssinatura, default(DateTime), "A Data de Assinatura do Evento não pode ser nula.");
            var dataExecucaoValidator = Assertion.GreaterThan(self.DataExecucaoEvento, default(DateTime), "A Data de Execução do Evento não pode ser nula.");
            var proponenteValidator = Assertion.NotNull(self.Proponente, $"A Proposta não possui Proponente.");
            var produtosValidator = Assertion.NotNull(self.Produtos, $"A Proposta não possui Produtos.");

            identificadorValidator
                .and(dataExecucaoValidator)
                .and(dataAssinaturaValidator)
                .and(produtosValidator)
                .and(proponenteValidator)
                .Validate();
        }

        public static void ValidarCompensacao(this IProposta proposta)
        {
            Assertion.NotNull(proposta, "Não foi possível fazer a compensação dos eventos para o contrato com parâmetros nulo.").Validate();
            Assertion.GreaterThan(proposta.Identificador, default(Guid), "Não foi possível fazer a compensação dos eventos para o contrato com parâmetros nulo.").Validate();
        }

        public static EventoImplantacao ToEvento(this IProposta proposta)
        {
                var evento = new EventoImplantacao(proposta.Identificador, proposta.IdentificadorCorrelacao, proposta.IdentificadorNegocio, proposta.DataExecucaoEvento);

                AdicionarCoberturas(evento, proposta);

                return evento;
        }

        public static void AdicionarCoberturas(EventoImplantacao evento, IProposta proposta)
        {
            proposta.ObterCoberturasDaProposta().SafeForEach(evento.Adicionar);
        }

        public static IEnumerable<CoberturaContratada> ObterCoberturasDaProposta(this IProposta proposta)
        {
            var coberturas = new List<CoberturaContratada>();

            foreach (var produto in proposta.Produtos)
            foreach (var cobertura in produto.Coberturas)
            {
                int tipoFormaContratacao = cobertura.Contratacao != null ? (int) cobertura.Contratacao.TipoFormaContratacao : default(int);

                var coberturaContratada = new CoberturaContratada(proposta.DataImplantacao, produto.InscricaoCertificado,
                    long.Parse(cobertura.IdentificadorExterno), cobertura.CodigoItemProduto,
                    proposta.Proponente.Matricula, proposta.Proponente.Sexo,
                    cobertura.InicioVigencia, proposta.Proponente.DataNascimento, proposta.DataAssinatura, tipoFormaContratacao)
                {
                    ProdutoId = produto.Codigo,
                    ClasseRiscoId = cobertura.ClasseRisco,
                    TipoRendaId = cobertura.Contratacao != null ? (int) cobertura.Contratacao.TipoDeRenda : default(int),
                    DataFimVigencia = cobertura.FimVigencia,
                    PrazoPagamentoEmAnos = cobertura.Prazos?.PagamentoEmAnos,
                    PrazoCoberturaEmAnos = cobertura.Prazos?.CoberturaEmAnos,
                    PrazoDecrescimoEmAnos = cobertura.Prazos?.DecrescimoEmAnos
                };

                if (produto.Matricula != proposta.Proponente.Matricula && proposta.Proponente.Conjuge != null)
                {
                    if (produto.Matricula == proposta.Proponente.Conjuge.Matricula)
                    {
                        coberturaContratada.Matricula = proposta.Proponente.Conjuge.Matricula;
                        coberturaContratada.Sexo = proposta.Proponente.Conjuge.Sexo;
                        coberturaContratada.DataNascimento = proposta.Proponente.Conjuge.DataNascimento;
                    }
                }

                coberturaContratada.Historico = ObterHistorico(coberturaContratada, produto.Beneficiarios, proposta.DadosPagamento?.Periodicidade,
                    cobertura.ValorBeneficio, cobertura.ValorCapital, cobertura.ValorContribuicao);

                coberturas.Add(coberturaContratada);
            }

            return coberturas;
        }

        public static HistoricoCoberturaContratada ObterHistorico(CoberturaContratada coberturaContratada, 
            List<IBeneficiario> beneficiarios, Periodicidade? periodicidade,
            decimal valorBeneficio, decimal valorCapital, decimal valorContribuicao)
        {
            var dto = new HistoricoCoberturaContratada(coberturaContratada);

            var beneficiarioMaisNovo = beneficiarios?.OrderByDescending(x => x.DataNascimento).FirstOrDefault();
            dto.SexoBeneficiario = beneficiarioMaisNovo?.Sexo;
            dto.DataNascimentoBeneficiario = beneficiarioMaisNovo?.DataNascimento;

            if (periodicidade != null)
                dto.PeriodicidadeId = (int) periodicidade;

            dto.ValorBeneficio = valorBeneficio;
            dto.ValorCapital = valorCapital;
            dto.ValorContribuicao = valorContribuicao;

            return dto;
        }
    }
}
