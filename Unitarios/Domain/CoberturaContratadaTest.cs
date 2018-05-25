using System;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain
{
    [TestFixture]
    public class CoberturaContratadaTest: UnitTesBase
    {
        [Test]
        public void Dado_uma_lista_de_Coberturas_deve_ser_preenchido_os_dados_do_Produto()
        {
            var cobertura = CoberturaContratadaBuilder.Uma().Build();
            var dadosProduto = DadosProdutoBuilder.Um().Padrao().Build();

            cobertura.ComDadosProduto(dadosProduto);

            Assert.That(cobertura.IndiceBeneficioId, Is.EqualTo(dadosProduto.IndiceBeneficioId));
            Assert.That(cobertura.IndiceContribuicaoId, Is.EqualTo(dadosProduto.IndiceContribuicaoId));
            Assert.That(cobertura.ModalidadeCoberturaId, Is.EqualTo(dadosProduto.ModalidadeCoberturaId));
            Assert.That(cobertura.NomeProduto, Is.EqualTo(dadosProduto.NomeProduto));
            Assert.That(cobertura.NumeroBeneficioSusep, Is.EqualTo(dadosProduto.NumeroBeneficioSusep));
            Assert.That(cobertura.NumeroProcessoSusep, Is.EqualTo(dadosProduto.NumeroProcessoSusep));
            Assert.That(cobertura.PermiteResgateParcial, Is.EqualTo(dadosProduto.PermiteResgateParcial));
            Assert.That(cobertura.PlanoFipSusep, Is.EqualTo(dadosProduto.PlanoFipSusep));
            Assert.That(cobertura.ProdutoId, Is.EqualTo(dadosProduto.ProdutoId));
            Assert.That(cobertura.TipoProvisoes, Is.EqualTo(dadosProduto.ProvisoesPossiveis));
            Assert.That(cobertura.TipoItemProdutoId, Is.EqualTo(dadosProduto.TipoItemProdutoId));
            Assert.That(cobertura.RegimeFinanceiroId, Is.EqualTo(dadosProduto.RegimeFinanceiroId));
        }
    }
}
