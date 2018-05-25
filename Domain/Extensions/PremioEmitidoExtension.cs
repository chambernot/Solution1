using System;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.Domain.Extensions
{
    public static class PremioEmitidoExtension
    {
        public static IEnumerable<DateTime> CompeteciasProvisao(this Premio premio)
        {
            return premio.Competencia.ObterCompetenciasProvisao(premio.FimVigencia);
        }
    }

    public static class DateTimeExtension
    {
        public static int CalcularMeses(this DateTime dataFinal, DateTime dataInicial)
        {
            int result;
            if (dataInicial >= dataFinal)
            {
                result = 0;
            }
            else
            {
                int num = dataFinal.Year - dataInicial.Year;
                int num2 = dataFinal.Month - dataInicial.Month;
                if (dataFinal.Day < dataInicial.Day)
                {
                    num2--;
                }
                result = num * 12 + num2;
            }
            return result;
        }

        public static IEnumerable<DateTime> ObterCompetenciasProvisao(this DateTime dataInicial, DateTime dataFinal)
        {
            List<DateTime> competencias = new List<DateTime>();

            var qtdCompetencias = dataFinal.CalcularMeses(dataInicial);

            var primeiraCompetencia = dataInicial;

            for (var meses = 0; meses <= qtdCompetencias; meses++)
            {
                var mesCorrente = primeiraCompetencia.AddMonths(meses);

                competencias.Add(new DateTime(mesCorrente.Year, mesCorrente.Month, 1));
            }

            return competencias;
        }
    }    
}
