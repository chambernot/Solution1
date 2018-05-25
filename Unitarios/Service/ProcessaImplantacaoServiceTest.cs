using GreenPipes;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Ninject;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaImplantacaoServiceTest : UnitTesBase
    {
        private IProposta _proposta;

        static readonly Task ReturnTask = Task.FromResult<object>(null);

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            ObterProposta();            
        }

        [Test]
        public async Task AoDispararOServicoaAplicacaoDeveProcessar()
        {
            //MockingKernel.GetMock<IPipe<ImplantacaoContext>>()
            //    .Setup(x => x.Send(It.IsAny<ImplantacaoContext>()))
            //    .Returns(Task.FromResult(_proposta));

            await MockingKernel.Get<ImplantacaoPropostaService>().Execute(_proposta);
            
            MockingKernel.MockRepository.Verify();
        }

        [Test]
        public async Task Dado_Um_Contrato_O_Mesmo_Deve_Ser_Compensado()
        {
            //MockingKernel.GetMock<IPipe<CompensacaoContext<EventoImplantacao>>>()
            //    .Setup(x => x.Send(It.IsAny< CompensacaoContext<EventoImplantacao>>()))
            //    .Returns(Task.CompletedTask);

            var service = MockingKernel.Get<ImplantacaoPropostaService>();
            await service.Compensate(_proposta);

            //MockingKernel.Get<IPipe<CompensacaoContext<EventoImplantacao>>>();
        }

        private void ObterProposta()
        {
            _proposta = PropostaBuilder.UmaProposta().Padrao().Build();
        }
    }
}
