using System;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Calculo.Contratos.Assinatura;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Adapters
{
    public class CalculoFacede : ICalculoFacade
    {
        private readonly ICalculoService _calculoService;

        public CalculoFacede(ICalculoService calculoService)
        {
            _calculoService = calculoService;
        }

        public virtual void ValidarDadosCalculoPMBAC(CoberturaContratada cobertura, DateTime dataExecucao)
        {
            _calculoService.CalcularPmbac(ObterParametrosCalculoPmbac(cobertura, dataExecucao));
        }

        public ProvisaoMatematicaBeneficioAConceder CalcularPMBAC(CoberturaContratada cobertura, DateTime dataExecucao, decimal valorProvisaoAnterior)
        {
            return MapearPMBAC(_calculoService.CalcularPmbac(ObterParametrosCalculoPmbac(cobertura, dataExecucao, valorProvisaoAnterior)));
        }

        private static ParametrosCalculoPmbac ObterParametrosCalculoPmbac(CoberturaContratada cobertura, DateTime dataExecucao)
        {
            return ObterParametrosCalculoPmbac(cobertura, dataExecucao, default(decimal));
        }

        private static ParametrosCalculoPmbac ObterParametrosCalculoPmbac(CoberturaContratada cobertura, DateTime dataExecucao, decimal valorProvisaoAnterior)
        {
            return new ParametrosCalculoPmbac()
            {
                Competencia = dataExecucao,
                ContribuicaoMensal = cobertura.Historico?.ValorContribuicao,
                DataAssinatura = cobertura.DataAssinatura,
                DataInicioVigencia = cobertura.DataInicioVigencia,
                DataNascimentoCliente = cobertura.DataNascimento,
                IdClasseRisco = (cobertura.ClasseRiscoId != null && cobertura.ClasseRiscoId != 0) ? (short)cobertura.ClasseRiscoId : (short?)null,
                IdItemProduto = cobertura.ItemProdutoId,
                IdSexoSegurado = cobertura.Sexo,
                TipoFormaContratacaoId = (short)cobertura.TipoFormaContratacaoId,
                TipoRendaId = (short)cobertura.TipoRendaId.GetValueOrDefault(),
                Identificador = cobertura.ItemCertificadoApoliceId,
                CapitalSegurado = cobertura.Historico?.ValorCapital,
                Periodicidade = ObterPeriodicidade(cobertura.Historico?.PeriodicidadeId),
                Fracionamento = (int)ObterPeriodicidade(cobertura.Historico?.PeriodicidadeId),
                PrazoPagamento = (short)cobertura.PrazoPagamentoEmAnos.GetValueOrDefault(),
                PrazoDiferimento = cobertura.PrazoPagamentoEmAnos,
                PrazoDecrescimo = cobertura.PrazoDecrescimoEmAnos != null
                    ? (short)cobertura.PrazoDecrescimoEmAnos
                    : (short?)null,
                ValorRenda = cobertura.Historico?.ValorBeneficio,
                DataNascimentoFilho = cobertura.Historico?.DataNascimentoBeneficiario,
                NumeroContribuicoes = cobertura.NumeroContribuicao,
                IdadePagamentoAntecipado = cobertura.DataNascimento.Year + cobertura.PrazoPagamentoEmAnos.GetValueOrDefault(),
                DataUltimaCorrecaoMonetaria = cobertura.DataUltimaCorrecaoMonetaria,
                ValorProvisaoAnterior = valorProvisaoAnterior,
                DataSaldamento = cobertura.DataSaldamento
            };
        }

        private static Periodicidade ObterPeriodicidade(int? periodicidadeId)
        {
            if (periodicidadeId == null)
                return Periodicidade.Mensal;

            return (Periodicidade)System.Enum.ToObject(typeof(Periodicidade), periodicidadeId);
        }

        private ProvisaoMatematicaBeneficioAConceder MapearPMBAC(CalculoPMBACResultado dto)
        {
            return new ProvisaoMatematicaBeneficioAConceder
            {
                Valor = dto.ValorProvisao,
                AtualizacaoMonetaria = dto.AberturaValorAtualizacaoMonetaria,
                Juros = dto.AberturaValorJuros,
                PercentualTaxaJuros = dto.PercentualJuros,
                ValorSobrevivencia = dto.AberturaValorSobrevivencia,
                Fator = dto.Fator,                
                Desvio = dto.ValorDesvioProvisao,
                ValorBeneficioCorrigido = dto.ValorBeneficioCorrigido,
                //DataUltimaAtualizacaoContribuicao = dto.va
            };
        }
    }
}
