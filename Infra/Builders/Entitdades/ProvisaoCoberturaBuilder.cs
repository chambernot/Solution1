using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class ProvisaoCoberturaBuilder : BaseBuilder<ProvisaoCobertura>
    {
        private ProvisaoCoberturaBuilder() { Instance = new ProvisaoCobertura(); }

        private ProvisaoCoberturaBuilder(CoberturaContratada cobertura, short tipoProvisaoId)
        {
            Instance = new ProvisaoCobertura(cobertura, tipoProvisaoId);
        }

        public static ProvisaoCoberturaBuilder UmBuilder()
        {
            return new ProvisaoCoberturaBuilder();
        }

        public static ProvisaoCoberturaBuilder UmBuilder(CoberturaContratada cobertura, TipoProvisaoEnum tipoProvisao)
        {
            return new ProvisaoCoberturaBuilder(cobertura, (short)tipoProvisao);
        }
    }
}
