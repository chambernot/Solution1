using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders
{
    public static class IdentificadoresPadrao
    {
        public enum ItensProduto : int
        {
            SeguroMorte = 300001
        }

        // ATENÇÂO: O Random não é ThreadSafe, use os métodos NextValue() para obter números únicos
        private static readonly Random RandomGenerator = new Random();
        private static readonly object RndLock = new object();
        private static readonly HashSet<int> GeneratedNumbers = new HashSet<int>();

        private static int NextValue(int minValue = 1, int maxValue = int.MaxValue)
        {
            lock (RndLock)
            {
                // Para estar seguros de só gerar números únicos
                int number;

                do
                {
                    number = RandomGenerator.Next(minValue, maxValue);
                } while (GeneratedNumbers.Contains(number));

                GeneratedNumbers.Add(number);

                return number;
            }
        }

        public static Guid Identificador = new Guid("0D7A50B1-0F50-4477-8235-44412783B5ED");

        public const short ProvisaoId = 8684;
        public const short NaturezaRenda = 1;
        public const short TipoEventoId = 1;
        public const short TabuaId = 22;
        public const short IndiceId = 11;
        public const short RegimeFinanceiroId = (short)TipoRegimeFinanceiroEnum.Capitalizacao;

        public const int PeriodicidadeId = (int)PeriodicidadeEnum.Anual;
        public const int IndiceBeneficioId = 2;
        public const int IndiceContribuicaoId = 1;
        public const int ModalidadeCoberturaId = 3;
        public const int ProdutoId = 202;        
        public const int TipoItemProdutoId = (short)TipoItemProdutoEnum.VidaIndividual;
        public const int PlanoFipSusep = 0;
        public const int TipoProvisoes = (int)TipoProvisaoEnum.PMBAC;        
        public const int NumeroParcela = 12;
        public const int SequencialEndosso = 1;
        public const int ItemProdutoId = 202806;
        public const int TipoFormaContratacaoId = 1;
        public const int ClasseRiscoId = 1479;
        public const int TipoRendaId = 0;
        public const int PrazoPagamento = 10;
        public const int PrazoDecrescimo = 3;
        public const int PrazoCobertura = 4;

        public const long ItemCertificadoApoliceId = 10188195015341011;
        public const long InscricaoId = 99999999;
        public const long Matricula = 100;
        public const long NumeroProposta = 9999;
        public static long ParcelaId { get { return NextValue(); } }

        public const decimal ValorContribuicao = 20.0M;
        public const decimal ValorBeneficio = 300.0M;
        public const decimal ValorCapital = 35.0M;
        public const decimal TaxaJurosContribuicao = 1.3M;
        public const decimal ValorAdicionalEntrada = 39.45M;
        public const decimal PercentualEntrada = 12.3M;
        public const decimal ValorProvisao = 15M;        
        public const decimal ValorCapitalSegurado = 12345.67m;
        public const decimal ValorIOF = 150m;
        public const decimal ValorCarregamento = 1234;
        public const decimal ValorProlaboreDescontado = 32423.34m;
        public const decimal ValorAtualizacao = 1234.56m;
        public const decimal ValorJuros = 1234.56m;
        public const decimal PercentualTaxaJuros = 12.55m;
        public const decimal ValorAtualizadoUltimaContribuicao = 100m;
        public const decimal ValorUltimaContribuicao = 120m;
        public const decimal ValorSobrevivencia = 57.53m;
        public const decimal ValorPago = 60m;
        public const decimal Desconto = 13m;
        public const decimal Multa = 3.5m;
        public const decimal IOFRetido = 0.12m;
        public const decimal IOFARecolher = 0.79m;
        public const decimal Desvio = 1.2m;
        public const decimal Fator = 2.1m;

        public const string Sexo = "Masculino";
        public const string FormulaPMBAC = "Formula_PMBAC32";
        public const string IdentificadorNegocio = "0D7A50B1-0F50-9977-8235-44412783B5HA";
        public const string IdentificadorCorrelacao = "0D7A50B1";
        public const string NomeProduto = "DIT";
        public const string NumeroProcessoSusep = "";
        public const string CodigoPortabilidadeInterna = "54321";
        public const string IdentificadorCredito = "88399";
        public const string CodigoSusep = "Susep03_Portabilidade";

        public static DateTime Competencia = new DateTime(2016, 02, 01);
        public static DateTime DataInicioVigencia = new DateTime(2016, 01, 01);
        public static DateTime DataFimVigencia = new DateTime(2016, 12, 31);
        public static DateTime DataInicioVigenciaProduto = DataInicioVigencia.AddMonths(-6);
        public static DateTime DataFimVigenciaProduto = DataInicioVigencia.AddMonths(6);
        public static DateTime DataNascimento = new DateTime(1980, 1, 1);
        public static DateTime DataExecucaoEvento = DateTime.Now;
        public static DateTime DataAssinatura = new DateTime(2016, 01, 01);
        public static DateTime DataUltimaAtualizacaoContribuicao = DateTime.Today;
        public static DateTime DataImplantacao = DateTime.Now;
        public static DateTime DataMovimentacao { get { return DateTime.Today; } }
        public static DateTime DataPagamento = Competencia.AddDays(10);
        public static DateTime DataApropriacao = DataPagamento.AddDays(2);


        public const bool PermiteResgateParcial = false;



        public static List<TabuaBiometrica> TabuaContribuicao = new List<TabuaBiometrica>() { new TabuaBiometrica() } ;
    }
}
