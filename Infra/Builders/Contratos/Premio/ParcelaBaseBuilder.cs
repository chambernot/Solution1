using Mongeral.Integrador.Contratos.Premio;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public abstract class ParcelaBaseBuilder<T> : MockBuilder<T> where T : class, IParcela
    {
        public ParcelaBaseBuilder<T> ComParcelaBuilder(ParcelaIdBuilder parcelaId)
        {
            Mock.SetupGet(x => x.ParcelaId).Returns(parcelaId.Build());
            return this;
        }

        public ParcelaBaseBuilder<T> ComValorBuilder(ValorBuilder valor)
        {
            Mock.SetupGet(x => x.Valores).Returns(valor.Build());
            return this;
        }

        public ParcelaBaseBuilder<T> ComVigenciaBuilder(VigenciaBuilder vigencia)
        {
            Mock.SetupGet(x => x.Vigencia).Returns(vigencia.Build());
            return this;
        }

        public ParcelaBaseBuilder<T> ComPremioPadrao()
        {
            ComParcelaBuilder(ParcelaIdBuilder.UmBuilder().Padrao());
            ComValorBuilder(ValorBuilder.UmBuilder().Padrao());
            ComVigenciaBuilder(VigenciaBuilder.UmBuilder().Padrao());
            return this;
        }

        public ParcelaBaseBuilder<T> ComProvisaoBuilder(params ProvisaoBuilder[] provisoes)
        {
            Mock.SetupGet(x => x.Provisoes).Returns(provisoes.Select(p => p.Build()).ToList());
            return this;
        }
        public ParcelaBaseBuilder<T> ComProvisaoVaziaBuilder()
        {
            Mock.SetupGet(x => x.Provisoes).Returns(new List<IProvisao>());
            return this;
        }
    }
}
