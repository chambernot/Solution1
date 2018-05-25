using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using NUnit.Framework;
using System.Linq;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Moq;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using Mongeral.Provisao.V2.Service.Premio.Validacao;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class PropostaImplantacaoExtensionTest : UnitTesBase
    {
        private IProposta _proposta;
        
        [Test]
        public void Dado_uma_proposta_deve_mapear_um_Evento()
        {
            ObterProposta();

            var eventoImplantacao = _proposta.ToEvento();
            
            Assert.AreEqual(_proposta.Identificador, eventoImplantacao.Identificador);
            Assert.AreEqual(_proposta.DataExecucaoEvento, eventoImplantacao.DataExecucaoEvento);
        }

        [Test]
        public void Dado_um_Produto_com_uma_cobertura_deve_Adicionar_dados_de_cobertura()
        {
            ObterProposta();

            var eventoImplantacao = _proposta.ToEvento();

            var coberturaContratada = eventoImplantacao.Coberturas.First();

            Assert.AreEqual(IdentificadoresPadrao.ItemCertificadoApoliceId, coberturaContratada.ItemCertificadoApoliceId);
            Assert.AreEqual(IdentificadoresPadrao.ItemProdutoId, coberturaContratada.ItemProdutoId);
        }

        [Test]
        public void Dado_uma_Proposta_com_beneficiario_deve_mapear_Historico_De_Cobertura()
        {
            ObterProposta();

            var eventoImplantacao = _proposta.ToEvento();

            var proposta = (_proposta.Produtos.Select(x => x.Beneficiarios)).First().First();

            var historico = eventoImplantacao.Coberturas.Select(c => c.Historico).First();

            Assert.AreEqual(proposta.DataNascimento, historico.DataNascimentoBeneficiario);
            Assert.AreEqual(proposta.Sexo, historico.SexoBeneficiario);
            Assert.AreEqual((int)_proposta.DadosPagamento.Periodicidade, historico.PeriodicidadeId);
        }

        [Test]
        public void Dado_um_Produto_com_Contratacao_deve_Adicionar_dados_de_contratacao()
        {
            ObterProposta();

            var eventoImplantacao = _proposta.ToEvento();

            var coberturaContratada = eventoImplantacao.Coberturas.First();

            Assert.AreEqual((int)TipoDeRendaEnum.NaoSeAplica, coberturaContratada.TipoRendaId);
            Assert.AreEqual((int)TipoFormaContratacaoEnum.RendaMensal, coberturaContratada.TipoFormaContratacaoId);
        }

        [Test]
        public void Dado_uma_proposta_sem_produto_ao_tentar_mapear_deve_gerar_excecao()
        {
            var proposta = new Mock<IProposta>().Object;

            Assert.That(() => proposta.Validar(), GeraErro("A Proposta não possui Produtos."));
        }

        [Test]
        public void Dado_uma_proposta_sem_Proponente_ao_tentar_mapear_deve_gerar_excecao()
        {
            var proposta = new Mock<IProposta>().Object;

            Assert.That(() => proposta.Validar(), GeraErro("A Proposta não possui Proponente."));
        }

        [Test]
        public void Ao_Processar_uma_proposta_sem_Produtos_deve_retornar_erro()
        {
            var proposta = new Mock<IProposta>().Object;

            Assert.That(() => proposta.Validar(), GeraErro("A Proposta não possui Produtos."));
        }

        [Test]
        public void Ao_Processar_uma_proposta_sem_DataExecucao_deve_retornar_erro()
        {
            var proposta = new Mock<IProposta>().Object;

            Assert.That(() => proposta.Validar(), GeraErro("A Data de Execução do Evento não pode ser nula."));
        }

        [Test]
        public void Ao_Processar_uma_proposta_sem_DataAssinatura_deve_retornar_erro()
        {
            var proposta = new Mock<IProposta>().Object;

            Assert.That(() => proposta.Validar(), GeraErro("A Data de Assinatura do Evento não pode ser nula."));
        }

        [Test]
        public void Ao_Compensar_uma_proposta_sem_Identificador_deve_retornar_erro()
        {
            var proposta = PropostaBuilder.UmaProposta().Build();

            Assert.That(() => proposta.Validar(), GeraErro("Não foi possível fazer a compensação dos eventos para o contrato com parâmetros nulo."));
        }

        [Test]
        public void Dado_uma_Proposta_com_matricula_igual_do_conjuge_deve_Adicionar_dados_do_conjuge()
        {
            _proposta = PropostaBuilder.UmaProposta()
                .Padrao()
                .Com(DadosPagamentoBuilder.UmPagamento())
                .Com(ProponenteBuider.UmProponente().Padrao()
                    .Com(PessoaBuilder.UmaPessoa().ComMatricula(IdentificadoresPadrao.Matricula)))
                .Com(ProdutoBuilder.UmProduto()
                    .ComMatricula(IdentificadoresPadrao.Matricula)
                    .ComInscricao(IdentificadoresPadrao.InscricaoId)
                    .Com(BeneficiarioBuilder.UmBeneficiario())
                    .Com(CoberturaBuilder.UmaCobertura()
                        .ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId)
                        .ComItemProdutoId(IdentificadoresPadrao.ItemProdutoId)
                        .ComInicioVigencia(IdentificadoresPadrao.DataInicioVigencia)
                        .Com(ContratacaoBuilder.UmaContratacao()
                            .ComTipoFormaContratacao(TipoFormaContratacaoEnum.RendaMensal)
                            .ComTipoRenda(TipoDeRendaEnum.NaoSeAplica))
                    )
                ).Build();

            var eventoImplantacao = _proposta.ToEvento();

            var coberturaContratada = eventoImplantacao.Coberturas.First();

            Assert.AreEqual(_proposta.Proponente.Conjuge.Matricula, coberturaContratada.Matricula);
        }

        private void ObterProposta()
        {
            _proposta = PropostaBuilder.UmaProposta()
                .Padrao()
                .Com(DadosPagamentoBuilder.UmPagamento())
                .Com(ProponenteBuider.UmProponente().Padrao())
                .Com(ProdutoBuilder.UmProduto()
                    .ComInscricao(IdentificadoresPadrao.InscricaoId)
                    .Com(BeneficiarioBuilder.UmBeneficiario())
                    .Com(CoberturaBuilder.UmaCobertura()
                        .ComItemCertificadoApolice(IdentificadoresPadrao.ItemCertificadoApoliceId)
                        .ComItemProdutoId(IdentificadoresPadrao.ItemProdutoId)
                        .ComInicioVigencia(IdentificadoresPadrao.DataInicioVigencia)
                        .Com(ContratacaoBuilder.UmaContratacao()
                            .ComTipoFormaContratacao(TipoFormaContratacaoEnum.RendaMensal)
                            .ComTipoRenda(TipoDeRendaEnum.NaoSeAplica))
                    )
                ).Build();
        }
    }
}
