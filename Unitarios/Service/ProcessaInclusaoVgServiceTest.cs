using System.Threading.Tasks;
using GreenPipes;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.VG;
using Moq;
using Ninject;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaInclusaoVgServiceTest: UnitTesBase
    {
        private IInclusaoCoberturaGrupal _inclusaoVg;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            ObterProposta();
        }

        [Test]
        public async Task AoDispararOServicoaAplicacaoDeveProcessar()
        {
            //MockingKernel.GetMock<IPipe<InclusaoVgContext>>()
            //    .Setup(x => x.Send(It.IsAny<InclusaoVgContext>()))
            //    .Returns(Task.FromResult(_inclusaoVg));

            await MockingKernel.Get<InclusaoCoberturaVgService>().Execute(_inclusaoVg);

            MockingKernel.MockRepository.Verify();
        }

        [Test]
        public async Task Dado_Um_Contrato_O_Mesmo_Deve_Ser_Compensado()
        {
            //MockingKernel.GetMock<IPipe<CompensacaoContext<EventoInclusaoVg>>>()
            //    .Setup(x => x.Send(It.IsAny<CompensacaoContext<EventoInclusaoVg>>()))
            //    .Returns(Task.CompletedTask);

            var service = MockingKernel.Get<InclusaoCoberturaVgService>();
            await service.Compensate(_inclusaoVg);

            //MockingKernel.Get<IPipe<CompensacaoContext<EventoInclusaoVg>>>();
        }


        private void ObterProposta()
        {
            _inclusaoVg = InclusaoVGBuilder.UmaInclusao().Padrao().Build();
        }
    }
}
