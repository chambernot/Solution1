using System;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Domain.Factories
{
    public class CalculadorProvisaoPremioNaoGanhoPremio
    {
        public virtual IEnumerable<ProvisaoDto> CalcularProvisao(Premio premio)
        {
            var dataExecucao = premio.EventoOperacional.DataExecucaoEvento;
            var competenciaCalculo = dataExecucao.Date.AddDays(1 - dataExecucao.Day);
            do
            {
                yield return new ProvisaoPremioNaoGanho()
                {
                    ProvisaoId = (short)TipoProvisaoEnum.PPNG,
                    DataMovimentacao = competenciaCalculo,
                    Valor = CalcularPPNG(competenciaCalculo, premio.InicioVigencia, premio.FimVigencia, premio.ValorPremio),
                };
                competenciaCalculo = competenciaCalculo.AddMonths(1);
            } while (competenciaCalculo < premio.FimVigencia);
        }

        public virtual IEnumerable<ProvisaoDto> CalcularProvisao(IParcela parcela, DateTime dataExecucao)
        {
            var competenciaCalculo = dataExecucao.Date.AddDays(1 - dataExecucao.Day);
            do
            {
                yield return new ProvisaoPremioNaoGanho()
                {
                    ProvisaoId = (short)TipoProvisaoEnum.PPNG,
                    DataMovimentacao = competenciaCalculo,
                    Valor = CalcularPPNG(competenciaCalculo, parcela.Vigencia.Inicio, parcela.Vigencia.Fim, parcela.Valores.Contribuicao)
                };
                competenciaCalculo = competenciaCalculo.AddMonths(1);
            } while (competenciaCalculo < parcela.Vigencia.Fim);
        }

        private ProvisaoPremioNaoGanho CalcularPPNGNaData(IParcela parcela, DateTime competencia)
        {
            return new ProvisaoPremioNaoGanho()
            {
                ProvisaoId = (short)TipoProvisaoEnum.PPNG,
                DataMovimentacao = competencia,
                Valor = CalcularPPNG(competencia, parcela.Vigencia.Inicio, parcela.Vigencia.Fim, parcela.Valores.Contribuicao)
            };
        }

        private static decimal CalcularPPNG(DateTime competencia, DateTime inicioVigencia, DateTime fimVigencia, decimal valorContribuicao)
        {
            var ultimoDiaMes = new DateTime(competencia.Year, competencia.Month, DateTime.DaysInMonth(competencia.Year, competencia.Month));
            var riscoADecorrer = (short)((fimVigencia > ultimoDiaMes) ? fimVigencia.Subtract(ultimoDiaMes).Days : 0);
            var periodoDaCobertura = (short)(fimVigencia.Subtract(inicioVigencia).Days + 1);

            var percentualPpng = riscoADecorrer / (decimal)periodoDaCobertura;

            percentualPpng = percentualPpng > 1 ? 1 : percentualPpng;
            percentualPpng = percentualPpng < 0 ? 0 : percentualPpng;

            var valorPpng = Math.Round(percentualPpng * valorContribuicao, 12);

            return valorPpng;
        }
    }
}
