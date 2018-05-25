using NUnit.Framework;
using System.Threading.Tasks;
using Ninject;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using System.Linq;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaCancelamentoPremioVgServiceTest : UnitTesBase
    {
        private IParcelaFaturaCancelada _parcelaCancelada;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            ObterCancelamento();
        }

        [Test]        
        public async Task AoCancelarUmPremioDeveSerProcessado()
        {   
            await MockingKernel.Get<CancelamentoFaturaVgService>().Execute(_parcelaCancelada);            
            MockingKernel.MockRepository.VerifyAll();

            Assert.That(_parcelaCancelada.Parcelas.First().Provisoes.Count, Is.EqualTo(1));
            Assert.That(_parcelaCancelada.Parcelas.First().Provisoes.First().ProvisaoId, Is.EqualTo((short)TipoProvisaoEnum.PPNG));
            Assert.That(_parcelaCancelada.Parcelas.First().Provisoes.First().Valor, Is.EqualTo(0));
            Assert.That(_parcelaCancelada.Parcelas.First().Provisoes.First().DataMovimentacao, Is.EqualTo(new DateTime(_parcelaCancelada.DataExecucaoEvento.Year, _parcelaCancelada.DataExecucaoEvento.Month, 1)));
        }

        private void ObterCancelamento()
        {            
            _parcelaCancelada = ParcelaCanceladaBuilder.UmBuilder().Padrao().Com(ParcelaBuilder.UmBuilder())
                .ComDataExecucaoEvento(IdentificadoresPadrao.Competencia).Build();
        }
    }
}
