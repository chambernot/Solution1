using Domain.Entities;
using Domain.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Unitarios
{
    public class HistoricoControllerTest
    {
        private long eventoImplantadoId = 1009988979870;
        private HistoricoCobertura _retorno;
        private IHistoricoCoberturaRepository _mockRepository;
        private HistoricoController _controller;

        [SetUp]
        public void FixtureSetUp()
        {
            _retorno = new HistoricoCobertura();
            _mockRepository = MockRepository.GenerateMock<IHistoricoCoberturaRepository>();
            _controller = new HistoricoController(_mockRepository);
        }

        [Test]
        public async Task Ao_Obter_Um_Historico_Por_Id_Deve_Ser_Executado_O_Metodo_Obter()
        {
            _mockRepository.Expect(x => x.Obter(default(int))).IgnoreArguments().Return(Task.FromResult(_retorno));

            await _controller.Obter((int)eventoImplantadoId);

            _mockRepository.AssertWasCalled(x => x.Obter((int)eventoImplantadoId));
        }

        [Test]
        public async Task Ao_Obter_Todos_Os_Historicos_Deve_Ser_Executado_O_Metodo_Obter_Todos()
        {
            var listaRetorno = new List<HistoricoCobertura>();
            listaRetorno.Add(_retorno);

            _mockRepository.Expect(x => x.ObterTodos()).Return(Task.FromResult<IEnumerable<HistoricoCobertura>>(listaRetorno)).Repeat.Any();

            await _controller.ObterTodos();

            _mockRepository.AssertWasCalled(x => x.ObterTodos());
        }
    }
}
