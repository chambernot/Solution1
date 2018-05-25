using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Mapear
{
    public static class PropostaToEvento
    {
        public static EventoImplantacao ToEvento(this IProposta proposta)
        {
            return new EventoImplantacao(proposta.Identificador, proposta.IdentificadorCorrelacao, proposta.IdentificadorNegocio, proposta.DataExecucaoEvento)
                .ComCoberturas(proposta.ObterCoberturasDaProposta());
        }

        public static EventoInclusaoVg ToEventoVG(this IInclusaoCoberturaGrupal proposta)
        {
            return new EventoInclusaoVg(proposta.Identificador, proposta.IdentificadorCorrelacao, proposta.IdentificadorNegocio, proposta.DataExecucaoEvento)
                .ComCoberturasVg(proposta.ObterCoberturasDaProposta());
        }

        public static IEnumerable<CoberturaContratada> ObterCoberturasDaProposta(this IProposta proposta)
        {
            var coberturas = new List<CoberturaContratada>();

            foreach (var produto in proposta.Produtos)
                foreach (var cobertura in produto.Coberturas)
                {
                    var coberturaContratada = new CoberturaContratada(long.Parse(cobertura.IdentificadorExterno))
                    {
                        InscricaoId = produto.InscricaoCertificado,
                        ItemProdutoId = cobertura.CodigoItemProduto,                        
                        DataInicioVigencia = cobertura.InicioVigencia,
                        DataAssinatura = proposta.DataAssinatura,
                        DataNascimento = proposta.Proponente.DataNascimento,
                        Matricula = proposta.Proponente.Matricula,
                        Sexo = proposta.Proponente.Sexo,
                        TipoFormaContratacaoId = (int)cobertura.Contratacao?.TipoFormaContratacao,
                        ProdutoId = produto.Codigo,
                        ClasseRiscoId = cobertura.ClasseRisco,
                        TipoRendaId = (int)cobertura.Contratacao?.TipoDeRenda,
                        DataFimVigencia = cobertura.FimVigencia,
                        PrazoPagamentoEmAnos = cobertura.Prazos?.PagamentoEmAnos,
                        PrazoCoberturaEmAnos = cobertura.Prazos?.CoberturaEmAnos,
                        PrazoDecrescimoEmAnos = cobertura.Prazos?.DecrescimoEmAnos,                        
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
                        cobertura.ValorBeneficio, cobertura.ValorCapital, cobertura.ValorContribuicao, proposta.DataImplantacao);
                    
                    coberturas.Add(coberturaContratada);
                }

            return coberturas;
        }

        public static HistoricoCoberturaContratada ObterHistorico(CoberturaContratada coberturaContratada,
            List<IBeneficiario> beneficiarios, Periodicidade? periodicidade,            
            decimal valorBeneficio, decimal valorCapital, decimal valorContribuicao, DateTime dataImplantacao)
        {
            var beneficiarioMaisNovo = beneficiarios?.OrderByDescending(x => x.DataNascimento).FirstOrDefault();

            var dto = new HistoricoCoberturaContratada(coberturaContratada)
            {
                SexoBeneficiario = beneficiarioMaisNovo?.Sexo,
                DataNascimentoBeneficiario = beneficiarioMaisNovo?.DataNascimento,
                PeriodicidadeId = (int)periodicidade.GetValueOrDefault(),
                ValorBeneficio = valorBeneficio,
                ValorCapital = valorCapital,
                ValorContribuicao = valorContribuicao
            };

            dto.InformaStatus(StatusCobertura.StatusCoberturaEnum.Activa, dataImplantacao);

            return dto;
        }
    }
}
