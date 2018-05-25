using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using NUnit.Framework;
using System.Linq;
using System;

namespace Mongeral.Provisao.V2.Testes.Integracao.DAL
{
    [TestFixture]
    public class CoberturaContratadaBaseTest : IntegrationTestBase
    {
        protected CoberturaContratada _coberturaCadastrada;
        protected Guid _identificador = Guid.NewGuid();
        protected Guid _historicoId;

        protected ICoberturas _coberturas;
        protected IEventosBase<EventoImplantacao> _eventos;
        protected EventoImplantacao _eventoImplantacao;
        protected CoberturaContratada _cobertura;


        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _eventoImplantacao = EventoImplantacaoBuilder.UmEvento(_identificador)
                .Com(CoberturaContratadaBuilder.Uma().ComPrazo(5,1,1)
                .Com(DadosProdutoBuilder.Um().Padrao()).Build())
                .Build();

            _eventos = GetInstance<IEventosBase<EventoImplantacao>>();

            _coberturas = GetInstance<ICoberturas>();

            _eventos.Salvar(_eventoImplantacao).Wait();

            _cobertura = _eventoImplantacao.Coberturas.First();

            _historicoId = _cobertura.Historico.Id;

            _coberturaCadastrada = _coberturas.ObterPorItemCertificado(_cobertura.ItemCertificadoApoliceId).Result;
        }
    }
    
    public class CoberturaContratadaDaoTest: CoberturaContratadaBaseTest
    {
        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_a_InscricaoId()
        {
            Assert.That(_cobertura.InscricaoId, Is.EqualTo(_coberturaCadastrada.InscricaoId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_ItemCertificadoApoliceId()
        {
            Assert.That(_cobertura.ItemCertificadoApoliceId, Is.EqualTo(_coberturaCadastrada.ItemCertificadoApoliceId));
        }


        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_ItemProdutoId()
        {
            Assert.That(_cobertura.ItemProdutoId, Is.EqualTo(_coberturaCadastrada.ItemProdutoId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_TipoFormaContratacaoId()
        {
            Assert.That(_cobertura.TipoFormaContratacaoId, Is.EqualTo(_coberturaCadastrada.TipoFormaContratacaoId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_TipoRendaId()
        {
            Assert.That(_cobertura.TipoRendaId, Is.EqualTo(_coberturaCadastrada.TipoRendaId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_Sexo()
        {
            Assert.That(_cobertura.Sexo, Is.EqualTo(_coberturaCadastrada.Sexo));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_a_DataNascimento()
        {
            Assert.That(_cobertura.DataNascimento, Is.EqualTo(_coberturaCadastrada.DataNascimento));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_a_DataInicioVigencia()
        {
            Assert.That(_cobertura.DataInicioVigencia, Is.EqualTo(_coberturaCadastrada.DataInicioVigencia));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_a_DataFimVigencia()
        {
            Assert.That(_cobertura.DataFimVigencia, Is.EqualTo(_coberturaCadastrada.DataFimVigencia));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_PrazoPagamento()
        {
            Assert.That(_cobertura.PrazoPagamentoEmAnos, Is.EqualTo(_coberturaCadastrada.PrazoPagamentoEmAnos));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_PrazoDecrescimo()
        {
            Assert.That(_cobertura.PrazoDecrescimoEmAnos, Is.EqualTo(_coberturaCadastrada.PrazoDecrescimoEmAnos));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_PrazoCobertura()
        {
            Assert.That(_cobertura.PrazoCoberturaEmAnos, Is.EqualTo(_coberturaCadastrada.PrazoCoberturaEmAnos));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_IndiceBeneficioId()
        {
            Assert.That(_cobertura.IndiceBeneficioId, Is.EqualTo(_coberturaCadastrada.IndiceBeneficioId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_IndiceContribuicaoId()
        {
            Assert.That(_cobertura.IndiceContribuicaoId, Is.EqualTo(_coberturaCadastrada.IndiceContribuicaoId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_TipoItemProdutoId()
        {
            Assert.That(_cobertura.TipoItemProdutoId, Is.EqualTo(_coberturaCadastrada.TipoItemProdutoId));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_TipoProvisoes()
        {
            Assert.That(_cobertura.TipoProvisoes, Is.EqualTo(_coberturaCadastrada.TipoProvisoes));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_o_NomeProduto()
        {
            Assert.That(_cobertura.NomeProduto, Is.EqualTo(_coberturaCadastrada.NomeProduto));
        }

        [Test]
        public void Ao_cadastrar_uma_Cobertura_deve_persistir_se_PermiteResgateParcial()
        {
            Assert.That(_cobertura.PermiteResgateParcial, Is.EqualTo(_coberturaCadastrada.PermiteResgateParcial));
        }
    }
}
