using Mongeral.Integrador.Contratos.Premio;
using Ninject;
using NUnit.Framework;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    public class ProcessaAjustePremioServiceTest: UnitTesBase
    {
        private IParcelaAjustada _ajustePremio;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _ajustePremio = ParcelaAjustadaBuilder.UmBuilder().Padrao().Build();
        }

        [Test]
        public async Task DadoUmEventoDeAjustePremioDeveSerProcessado()
        {
            await MockingKernel.Get<AjustePremioService>().Execute(_ajustePremio);            
        }

        [Test]
        public async Task DadoUmEventoDeCompensacaoDeAjustePremioDeveCompensar()
        {
            //MockingKernel.GetMock<IPipe<CompensacaoContext<EventoAjustePremio>>>()
            //    .Setup(x => x.Send(It.IsAny<CompensacaoContext<EventoAjustePremio>>())).Returns(Task.CompletedTask);

            await MockingKernel.Get<AjustePremioService>().Compensate(_ajustePremio);
        }
    }
}
