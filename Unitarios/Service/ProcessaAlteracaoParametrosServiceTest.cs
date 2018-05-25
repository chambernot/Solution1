using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using Moq;
using Ninject;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using GreenPipes;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service
{
    [TestFixture]
    public class ProcessaAlteracaoParametrosServiceTest : UnitTesBase
    {
        private IProposta _proposta;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            ObterProposta();
        }

        [Test]
        public async Task Quando_o_servico_for_disparado_o_EventoAlteracao_deve_ser_processado()
        {
            //MockingKernel.GetMock<IPipe<AlteracaoContext>>()
            //    .Setup(x => x.Send(It.IsAny<AlteracaoContext>()))
            //    .Returns(Task.FromResult(_proposta));

            await MockingKernel.Get<AlteracaoPropostaService>().Execute(_proposta);
            
            MockingKernel.MockRepository.VerifyAll();
        }

        [Test]
        public async Task Dado_Um_Contrato_O_Mesmo_Deve_Ser_Compensado()
        {
            //MockingKernel.GetMock<IPipe<CompensacaoContext<EventoAlteracao>>>()
            //    .Setup(x => x.Send(It.IsAny<CompensacaoContext<EventoAlteracao>>()))
            //    .Returns(Task.CompletedTask);

            var service = MockingKernel.Get<AlteracaoPropostaService>();

            await service.Compensate(_proposta);

            MockingKernel.MockRepository.VerifyAll();
        }

        private void ObterProposta()
        {
            _proposta = PropostaBuilder.UmaProposta()
                .Padrao()
                .Com(DadosPagamentoBuilder.UmPagamento())
                .Com(ProponenteBuider.UmProponente().ComMatricula(20)
                    .Com(PessoaBuilder.UmaPessoa().ComMatricula(IdentificadoresPadrao.Matricula)))
                .Com(ProdutoBuilder.UmProduto()
                    .ComMatricula(IdentificadoresPadrao.Matricula)
                    .ComInscricao(IdentificadoresPadrao.InscricaoId)
                    .Com(BeneficiarioBuilder.UmBeneficiario())
                    .Com(CoberturaBuilder.UmaCobertura()
                        .ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId)
                        .ComItemProdutoId(IdentificadoresPadrao.ItemProdutoId)
                        .Com(ContratacaoBuilder.UmaContratacao()
                            .ComTipoFormaContratacao(TipoFormaContratacaoEnum.RendaMensal)
                            .ComTipoRenda(TipoDeRendaEnum.NaoSeAplica))
                    )
                ).Build();
        }
    }
}
