using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    public class EventosImplantadoDaoTest : IntegrationTestBase
    {
        private Guid _identificador = new Guid("97FFD4EA-39AD-468F-B212-10F6984BA61F");
        private EventoImplantacao _eventoImplantacao;

        private IEventosBase<EventoImplantacao> _eventos;        

        [OneTimeSetUp]
        protected new void FixtureSetUp()
        {
            _eventoImplantacao = EventoImplantacaoBuilder.UmEvento(_identificador)                
                .Com(CoberturaContratadaBuilder.Uma()
                    .Com(DadosProdutoBuilder.Um().Padrao()).Build())
                .Build();

            _eventos = GetInstance<IEventosBase<EventoImplantacao>>();
        }

        [Test]
        public async Task Dado_um_Evento_implantacao_deve_persistir_os_dados()
        {            
            await _eventos.Salvar(_eventoImplantacao);
        }

        [Test]
        public async Task Dado_uma_proposta_existente_deve_verificar_e_retornar_verdadeiro()
        {
            await _eventos.Salvar(_eventoImplantacao);

            var existe = await _eventos.ExisteEvento(_identificador);

            Assert.That(existe, Is.EqualTo(true));
        }

        [Test]
        public async Task Dado_uma_proposta_nova_deve_verificar_se_ja_Existe_e_retornar_falso()
        {   
            var result = await _eventos.ExisteEvento(new Guid());

            Assert.That(result, Is.EqualTo(false));
        } 
    }
}
