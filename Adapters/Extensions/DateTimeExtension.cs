using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Adapters
{
    public static class DateTimeExtension
    {
        public static void ExtractSeconds(this System.DateTime datetime)
        {
            datetime -= System.TimeSpan.FromSeconds((double)datetime.Second);
        }
        public static int CalcularAnos(this System.DateTime dataFinal, System.DateTime dataInicial)
        {
            int result;
            if (dataInicial >= dataFinal)
            {
                result = 0;
            }
            else
            {
                int num = dataFinal.Year - dataInicial.Year;
                if (dataFinal.Month < dataInicial.Month || (dataFinal.Month == dataInicial.Month && dataFinal.Day < dataInicial.Day))
                {
                    num--;
                }
                result = num;
            }
            return result;
        }

        public static int CalcularMeses(this System.DateTime dataFinal, System.DateTime dataInicial)
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
        public static System.DateTime ModificarDiaPara(this System.DateTime data, int diaNovo)
        {
            if (diaNovo < 0)
            {
                throw new System.ArgumentException("Erro: Não foi possível modificar data. Dia novo é negativo.");
            }
            int num = System.DateTime.DaysInMonth(data.Year, data.Month);
            if (diaNovo > num)
            {
                diaNovo = num;
            }
            int num2 = diaNovo - data.Day;
            return data.AddDays((double)num2);
        }
        public static System.DateTime Competencia(this System.DateTime data)
        {
            return new System.DateTime(data.Year, data.Month, 1);
        }
        public static string ToXmlDateString(this System.DateTime data)
        {
            return data.ToString("yyyy-MM-dd");
        }
        public static System.DateTime ObterMaisProxima(this System.DateTime competencia, int dia)
        {
            System.DateTime result;
            if (dia <= 0)
            {
                result = competencia;
            }
            else
            {
                int num = dia;
                System.DateTime dateTime;
                bool flag = !System.DateTime.TryParse(string.Format("{0:0000}-{1:00}-{2:00}", competencia.Year, competencia.Month, num), out dateTime);
                while (flag)
                {
                    flag = !System.DateTime.TryParse(string.Format("{0:0000}-{1:00}-{2:00}", competencia.Year, competencia.Month, num), out dateTime);
                    num--;
                }
                result = dateTime;
            }
            return result;
        }
    }
}
