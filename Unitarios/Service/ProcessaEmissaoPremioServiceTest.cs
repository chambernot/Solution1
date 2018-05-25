using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using Mongeral.Integrador.Contratos.Premio;
using System.Threading.Tasks;
using Ninject;
using GreenPipes;
using Moq;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    public class ProcessaEmissaoPremioServiceTest : UnitTesBase
    {
        private IParcelaEmitida _premioEmitido;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _premioEmitido = ParcelaEmitidaBuilder.UmBuilder().Padrao().Build();
        }

        [Test]
        public async Task AoEmitirOPremioDeveSerProcessado()
        {
            //MockingKernel.GetMock<IPipe<EmissaoPremioContext>>()
            //    .Setup(x => x.Send(It.IsAny<EmissaoPremioContext>())).Returns(Task.FromResult(_premioEmitido));

            //await MockingKernel.Get<ProcessaEmissaoPremioService>().Execute(_premioEmitido);

            //MockingKernel.GetMock<IPipe<EmissaoPremioContext>>().Verify();
        }

        [Test]
        public async Task AoCompensarUmPremioOEventoDeveSerExcluido()
        {
            //MockingKernel.GetMock<IPipe<CompensacaoContext<EventoEmissaoPremio>>>()
            //    .Setup(x => x.Send(It.IsAny<CompensacaoContext<EventoEmissaoPremio>>())).Returns(Task.CompletedTask);

            await MockingKernel.Get<EmissaoPremioService>().Compensate(_premioEmitido);
        }
    }
}
