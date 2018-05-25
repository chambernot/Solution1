using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Unitarios.Controller
{
    [TestFixture]
    public class EventoControllerTest
    {
        private long eventoImplantadoId = 1009988979870;
        private EventoImplantado _retorno;
        private IEventoImplantadoRepository _mockRepository;
        private EventoController _controller;

        [SetUp]
        public void FixtureSetUp()
        {
            _retorno = new EventoImplantado();
            _mockRepository = MockRepository.GenerateMock<IEventoImplantadoRepository>();
            _controller = new EventoController(_mockRepository);
        }

        [Test]
        public async Task Ao_Solicitar_Um_Evento_Por_Id_Deve_Ser_Executado_O_Metodo_Obter()
        {   
            _mockRepository.Expect(x => x.Obter(default(int), FiltroEventoEnum.PorEventoImplantadoId)).IgnoreArguments().Return(Task.FromResult(_retorno));
                        
            await _controller.ObterPorId(eventoImplantadoId);

            _mockRepository.AssertWasCalled(x => x.Obter(eventoImplantadoId, FiltroEventoEnum.PorEventoImplantadoId));
        }

        [Test]
        public async Task Ao_Solicitar_Um_Evento_Por_ItemCertificadoApoliceId_Deve_Ser_Executado_O_Metodo_Obter()
        {
            _mockRepository.Expect(x => x.Obter(default(int), FiltroEventoEnum.PorItemCertificadoApoliceId)).IgnoreArguments().Return(Task.FromResult(_retorno));
                        
            await _controller.ObterPorItemCertificadoApoliceId(eventoImplantadoId);

            _mockRepository.AssertWasCalled(x => x.Obter(eventoImplantadoId, FiltroEventoEnum.PorItemCertificadoApoliceId));
        }

        [Test]
        public async Task Ao_Solicitar_Um_Evento_Por_InscricaoId_Deve_Ser_Executado_O_Metodo_Obter()
        {            
            _mockRepository.Expect(x => x.Obter(default(int), FiltroEventoEnum.PorEventoImplantadoId)).IgnoreArguments().Return(Task.FromResult(_retorno));

            await _controller.ObterPorInscricaoId(eventoImplantadoId);

            _mockRepository.AssertWasCalled(x => x.Obter(eventoImplantadoId, FiltroEventoEnum.PorInscricaoId));
        }

        [Test]
        public async Task Ao_Solicitar_Todos_Os_Eventos_Deve_Ser_Executado_O_Metodo_Obter_Todos()
        {            
            var listaRetorno = new List<EventoImplantado>();
            listaRetorno.Add(_retorno);
            
            _mockRepository.Expect(x => x.ObterTodos()).Return(Task.FromResult<IEnumerable<EventoImplantado>>(listaRetorno)).Repeat.Any();

            await _controller.ObterTodos();

            _mockRepository.AssertWasCalled(x => x.ObterTodos());
        }
    }    
}
