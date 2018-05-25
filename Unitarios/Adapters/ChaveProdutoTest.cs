using Mongeral.Provisao.V2.Adapters.Containers;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Conteiners;
using NUnit.Framework;
using System;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Adapters.Containers
{
    [TestFixture]
    public class ChaveProdutoTest
    {
        private ChaveProduto _chave1;
        private ChaveProduto _chave2;
        private ChaveProduto _chave3;
        private ChaveProduto _chave4;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _chave1 = ChaveProdutoBuilder.UmaChave().Build();
            _chave2 = ChaveProdutoBuilder.UmaChave().Build();
            _chave3 = ChaveProdutoBuilder.UmaPersonalizada(
                    IdentificadoresPadrao.ItemProdutoId, 
                    IdentificadoresPadrao.TipoFormaContratacaoId, 
                    null, IdentificadoresPadrao.DataInicioVigencia)
                .Build();

            _chave4 = ChaveProdutoBuilder.UmaPersonalizada(
                    IdentificadoresPadrao.ItemProdutoId,
                    IdentificadoresPadrao.TipoFormaContratacaoId,
                    IdentificadoresPadrao.TipoRendaId, IdentificadoresPadrao.DataInicioVigencia.AddYears(2))
                .Build();

            _chave1.SetVigencia(IdentificadoresPadrao.DataInicioVigenciaProduto, IdentificadoresPadrao.DataFimVigenciaProduto);
            _chave2.SetVigencia(IdentificadoresPadrao.DataInicioVigenciaProduto, IdentificadoresPadrao.DataFimVigenciaProduto);
            _chave3.SetVigencia(IdentificadoresPadrao.DataInicioVigenciaProduto, IdentificadoresPadrao.DataFimVigenciaProduto);
            _chave4.SetVigencia(IdentificadoresPadrao.DataInicioVigenciaProduto, IdentificadoresPadrao.DataFimVigenciaProduto);
        }

        [Test]
        public void Dado_duas_Chave_iguais_ao_validar_deve_retornar_verdadeiro()
        {
            Assert.IsTrue(_chave1.Equals(_chave2));
        }

        [Test]
        public void Dado_duas_Chave_diferentes_ao_validar_deve_retornar_falso()
        {
            Assert.IsFalse(_chave1.Equals(_chave3));
        }

        [Test]
        public void Dado_uma_Chave_com_vigencia_invalida_deve_retornar_falso()
        {
            Assert.IsFalse(_chave4.Equals(_chave2));
        }
    }
}
