using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using NUnit.Framework;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Transformação
{
    [TestFixture]
    public class EventoImplantacaoPropostaTest: UnitTesBase
    {
        private IProposta _proposta;
        private EventoImplantacao _evento;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            ObterProposta();

            _evento = _proposta.ToEvento();
        }

        [Test]
        public void MapearEventoImplantacao()
        {
            Assert.That(_proposta.Identificador, Is.EqualTo(_evento.Identificador));
            Assert.That(_proposta.IdentificadorNegocio, Is.EqualTo(_evento.IdentificadorNegocio));
            Assert.That(_proposta.IdentificadorCorrelacao, Is.EqualTo(_evento.IdentificadorCorrelacao));
            Assert.That(_proposta.DataExecucaoEvento, Is.EqualTo(_evento.DataExecucaoEvento));
        }

        [Test]
        public void MapearProduto()
        {
            var produto = _proposta.Produtos.First();
            var cobertura = _evento.Coberturas.First();

            Assert.That(produto.Codigo, Is.EqualTo(cobertura.ProdutoId));
            Assert.That(produto.InscricaoCertificado, Is.EqualTo(cobertura.InscricaoId));
        }

        [Test]
        public void MapearCobertura()
        {
            var coberturaContratada = _evento.Coberturas.First();

            var produto = _proposta.Produtos.First();
            var cobertura = produto.Coberturas.First();

            Assert.That(_proposta.DataAssinatura, Is.EqualTo(coberturaContratada.DataAssinatura));

            Assert.That(cobertura.IdentificadorExterno, Is.EqualTo(coberturaContratada.ItemCertificadoApoliceId.ToString()));                        
            Assert.That(cobertura.InicioVigencia, Is.EqualTo(coberturaContratada.DataInicioVigencia));
            Assert.That(cobertura.FimVigencia, Is.EqualTo(coberturaContratada.DataFimVigencia));
            Assert.That(cobertura.CodigoItemProduto, Is.EqualTo(coberturaContratada.ItemProdutoId));
            Assert.That(cobertura.ClasseRisco, Is.EqualTo(coberturaContratada.ClasseRiscoId));
            Assert.That((int)cobertura.Contratacao.TipoDeRenda, Is.EqualTo(coberturaContratada.TipoRendaId));

            Assert.That(cobertura.Prazos.CoberturaEmAnos, Is.EqualTo(coberturaContratada.PrazoCoberturaEmAnos));
            Assert.That(cobertura.Prazos.DecrescimoEmAnos, Is.EqualTo(coberturaContratada.PrazoDecrescimoEmAnos));
            Assert.That(cobertura.Prazos.PagamentoEmAnos, Is.EqualTo(coberturaContratada.PrazoPagamentoEmAnos));            
        }

        [Test]
        public void MapearProponente()
        {
            var coberturaContratada = _evento.Coberturas.First();            

            Assert.That(_proposta.Proponente.DataNascimento, Is.EqualTo(coberturaContratada.DataNascimento));
            Assert.That(_proposta.Proponente.Matricula, Is.EqualTo(coberturaContratada.Matricula));
            Assert.That(_proposta.Proponente.Sexo, Is.EqualTo(coberturaContratada.Sexo));
        }

        [Test]
        public void MapearHistorico()
        {
            var coberturaContratada = _evento.Coberturas.First();
            var cobertura = _proposta.Produtos.First().Coberturas.First();
            var beneficiario = _proposta.Produtos.First().Beneficiarios.First();

            Assert.That(beneficiario.DataNascimento, Is.EqualTo(coberturaContratada.Historico.DataNascimentoBeneficiario));
            Assert.That(beneficiario.Sexo, Is.EqualTo(coberturaContratada.Historico.SexoBeneficiario));

            Assert.That((int)_proposta.DadosPagamento.Periodicidade, Is.EqualTo(coberturaContratada.Historico.PeriodicidadeId));
            Assert.That(cobertura.ValorBeneficio, Is.EqualTo(coberturaContratada.Historico.ValorBeneficio));
            Assert.That(cobertura.ValorCapital, Is.EqualTo(coberturaContratada.Historico.ValorCapital));
            Assert.That(cobertura.ValorContribuicao, Is.EqualTo(coberturaContratada.Historico.ValorContribuicao));
        }

        private void ObterProposta()
        {
            _proposta = PropostaBuilder.UmaProposta()
                .Padrao()
                .Com(DadosPagamentoBuilder.UmPagamento())
                .Com(ProponenteBuider.UmProponente().Padrao())
                .Com(ProdutoBuilder.UmProduto()
                    .ComInscricao(IdentificadoresPadrao.InscricaoId)
                    .Com(BeneficiarioBuilder.UmBeneficiario().Padrao())
                    .Com(CoberturaBuilder.UmaCobertura().Padrao()
                        .Com(PrazosBuilder.Um().Padrao())
                        .Com(ContratacaoBuilder.UmaContratacao()
                            .ComTipoFormaContratacao(TipoFormaContratacaoEnum.RendaMensal)
                            .ComTipoRenda(TipoDeRendaEnum.NaoSeAplica))                        
                    )
                ).Build();
        }
    }
}


