using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio
{
    public class ParcelaBuilder: ParcelaBaseBuilder<IParcela>
    {
        public static ParcelaBuilder UmBuilder()
        {
            return new ParcelaBuilder();
        }

        public ParcelaBuilder Com(ParcelaIdBuilder parcelaId)
        {
            ComParcelaBuilder(parcelaId);
            return this;
        }

        public ParcelaBuilder Com(ValorBuilder valor)
        {
            ComValorBuilder(valor);
            return this;
        }

        public ParcelaBuilder Com(VigenciaBuilder vigencia)
        {
            ComVigenciaBuilder(vigencia);
            return this;
        }

        public ParcelaBuilder Padrao()
        {
            ComPremioPadrao();
            ComProvisaoVazia();
            return this;
        }

        public ParcelaBuilder ComProvisaoVazia()
        {
            ComProvisaoVaziaBuilder();
            return this;
        }

        public ParcelaBuilder ComProvisoes(params ProvisaoBuilder[] provisoes)
        {
            ComProvisaoBuilder(provisoes);
            return this;
        }
    }
}
