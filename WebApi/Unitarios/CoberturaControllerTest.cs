using Domain.Entities;
using Domain.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Unitarios
{
    public class CoberturaControllerTest
    {
        private long eventoImplantadoId = 1009988979870;
        private CoberturaContratada _retorno;
        private ICoberturaContratadaRepository _mockRepository;
        private CoberturaController _controller;

        [SetUp]
        public void FixtureSetUp()
        {
            _retorno = new CoberturaContratada();
            _mockRepository = MockRepository.GenerateMock<ICoberturaContratadaRepository>();
            _controller = new CoberturaController(_mockRepository);
        }

        [Test]
        public async Task Ao_Obter_Uma_Cobertura_Por_Id_Deve_Ser_Executado_O_Metodo_Obter()
        {
            _mockRepository.Expect(x => x.Obter(default(int))).IgnoreArguments().Return(Task.FromResult(_retorno));

            await _controller.Obter((int)eventoImplantadoId);

            _mockRepository.AssertWasCalled(x => x.Obter((int)eventoImplantadoId));
        }

        [Test]
        public async Task Ao_Obter_Todas_As_Cobertura_Deve_Ser_Executado_O_Metodo_Obter_Todos()
        {
            var listaRetorno = new List<CoberturaContratada>();
            listaRetorno.Add(_retorno);

            _mockRepository.Expect(x => x.ObterTodos()).Return(Task.FromResult<IEnumerable<CoberturaContratada>>(listaRetorno)).Repeat.Any();

            await _controller.ObterTodos();

            _mockRepository.AssertWasCalled(x => x.ObterTodos());

        }

        [Test]
        public async Task Ao_Obter_Todas_As_Cobertura_Por_Matricula()
        {
            var listaRetorno = new List<CoberturaContratada>();
            listaRetorno.Add(_retorno);

            _mockRepository.Expect(x => x.ObterPorMatricula(0)).IgnoreArguments().Return(Task.FromResult<IEnumerable<CoberturaContratada>>(listaRetorno)).Repeat.Any();

            await _controller.ObterPorMatricula(0);

            _mockRepository.AssertWasCalled(x => x.ObterTodos());
        }
    }
}
