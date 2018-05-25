using System;
using Mongeral.Calculo.Contratos.Assinatura;
using Mongeral.Provisao.V2.Adapters;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Integracao.Adapters
{
    [TestFixture]
    public class CalculoAdaptersTest: IntegrationTestBase
    {
        private ICalculoService _calculoService;
        private CalculoFacede _ambienteCalculo;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _calculoService = GetInstance<ICalculoService>();

            _ambienteCalculo = new CalculoFacede(_calculoService);
        }

        [Test]        
        public void Dado_uma_Cobertura_deve_validar_dados_para_CalcularPMBAC()
        {
            var cobertura = CarregarCoberturaValidaParaCalculoPMBAC();

            Assert.That(() => _ambienteCalculo.ValidarDadosCalculoPMBAC(cobertura, DateTime.Now), Throws.Nothing);
        }
        
        private CoberturaContratada CarregarCoberturaValidaParaCalculoPMBAC()
        {
            var cobertura = CoberturaContratadaBuilder.Uma().Padrao()
                .Com(HistoricoCoberturaContratadaBuilder.UmHistorico()).Build();
            
            cobertura.DataInicioVigencia = new DateTime(2017, 06, 01);
            cobertura.DataNascimento = new DateTime(1959, 12, 04);
            cobertura.Historico.ValorContribuicao = (decimal) 895.58;
            cobertura.ClasseRiscoId = 1479;
            cobertura.ItemProdutoId = 202806;
            cobertura.Sexo = "M";
            cobertura.TipoFormaContratacaoId = 1;
            cobertura.TipoRendaId = 0;
            cobertura.Historico.ValorCapital = 0;
            cobertura.Historico.ValorBeneficio = 0;

            return cobertura;
        }
    }
}
