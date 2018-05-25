using Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Entitdades;
using Mongeral.Provisao.V2.Application;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System;
using Mongeral.Infrastructure.Cache;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Application
{
    [TestFixture]
    public class ProcessaImplantacaoTest
    {
        private EventoImplantacao _eventoImplantacao;
        private Mock<IValidaEventoImplantacao> _validador;
        private Mock<IEventos<EventoImplantacao>> _eventos;
        private Mock<IndexedCachedContainer<ChaveProduto,DadosProduto>> _dadosProduto;

        static readonly Task ReturnTask = Task.FromResult<object>(null);
        private Guid _identificador = Guid.NewGuid();

        [OneTimeSetUp]
        [Ignore("Foi substituido por Filter")]
        public void FixtureSetUp()
        {
            _eventoImplantacao = EventoImplantacaoBuilder.UmEvento(_identificador)
                .Com(CoberturaContratadaBuilder.Uma()
                    .Com(HistoricoCoberturaContratadaBuilder.UmHistorico()))
                .Build();

            _validador = new Mock<IValidaEventoImplantacao>();
            _validador.Setup(x => x.Validar(It.IsAny<CoberturaContratada>())).Returns(ReturnTask);

            _dadosProduto = new Mock<IndexedCachedContainer<ChaveProduto, DadosProduto>>();
            _dadosProduto.Setup(p => p.GetValue(It.IsAny<ChaveProduto>()));

            _eventos = new Mock<IEventos<EventoImplantacao>>();
            _eventos.Setup(x => x.Adicionar(It.IsAny<EventoImplantacao>())).Returns(ReturnTask);
            _eventos.Setup(x => x.Contem(Guid.NewGuid())).Returns(Task.FromResult(false));
        }

        [Test]
        [Ignore("Foi substituido por Filter")]
        public async Task Se_a_inscricao_for_valida_deve_ser_persistida()
        {
            var evento = new Mock<EventoImplantacao>();

            var processarImplantacao = new ProcessaEventoImplantacao(_eventos.Object, _validador.Object, _dadosProduto.Object);

            await processarImplantacao.Processar(_eventoImplantacao);
            
            evento.Verify(x => x.Adicionar(It.IsAny<CoberturaContratada>()));            
        }

        [Test]
        [Ignore("Foi substituido por Filter")]
        public async Task Se_a_proposta_já_foi_Implantada_nao_deve_presistir()
        {
            _eventos.Setup(x => x.Contem(_identificador)).Returns(Task.FromResult<bool>(true));

            var processarImplantacao = new ProcessaEventoImplantacao(_eventos.Object, _validador.Object, _dadosProduto.Object);

            await processarImplantacao.Processar(_eventoImplantacao);

            _eventos.Verify(m => m.Adicionar(It.IsAny<EventoImplantacao>()));
        }

        [Test]
        [Ignore("Foi substituido por Filter")]
        public async Task Se_a_proposta_não_foi_Implantada_deve_persistir()
        {
            _eventos.Setup(x => x.Contem(_identificador)).Returns(Task.FromResult<bool>(false));

            var processarImplantacao = new ProcessaEventoImplantacao(_eventos.Object, _validador.Object, _dadosProduto.Object);

            await processarImplantacao.Processar(_eventoImplantacao);

            _eventos.Verify(m => m.Adicionar(It.IsAny<EventoImplantacao>()));
        }

        [Test]
        [Ignore("Foi substituido por Filter")]
        public async Task Dado_Um_Identificador_Ao_Compensar_Deve_Buscar_Os_Eventos_E_Chamar_O_Metodo_Apagar()
        {
            _eventos.Setup(x => x.Remover(_identificador)).Returns(ReturnTask);

            var processaCompensacao = new ProcessaEventoImplantacao(_eventos.Object, _validador.Object, _dadosProduto.Object);
            await processaCompensacao.Compensar(_identificador);

            _eventos.Verify(m => m.Remover(It.IsAny<Guid>()));

            Assert.That(() => _eventos.Object.Contem(_identificador), Is.EqualTo(false));
        }

        [Test]
        [Ignore("Foi substituido por Filter")]
        public async Task Dado_Um_Identificador_Caso_Nenhum_Evento_For_Encontrado_O_Metodo_Apagar_Nao_Pode_Ser_Executado()
        {
            _eventos.Setup(x => x.Remover(_identificador)).Returns(Task.CompletedTask);

            var processaCompensacao = new ProcessaEventoImplantacao(_eventos.Object, _validador.Object, _dadosProduto.Object);
            await processaCompensacao.Compensar(_identificador);

            _eventos.VerifyAll();
        }
    }
}
