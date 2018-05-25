using System;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Provisao
{
    public class PmbacBuilder : BaseBuilder<ProvisaoMatematicaBeneficioAConceder>
    {
        private PmbacBuilder()
        {
            Instance = new ProvisaoMatematicaBeneficioAConceder();
        }

        public static PmbacBuilder UmBuilder()
        {
            return new PmbacBuilder();
        }

        public PmbacBuilder ComValorProvisao(decimal valor)
        {
            Instance.Valor = valor;
            return this;
        }

        public PmbacBuilder ComDataCompetencia(DateTime competencia)
        {
            Instance.DataMovimentacao = competencia;
            return this;
        }

        public PmbacBuilder ComFator(decimal fator)
        {
            Instance.Fator = fator;
            return this;
        }

        public PmbacBuilder ComDesvio(decimal desvio)
        {
            Instance.Desvio = desvio;
            return this;
        }

        public PmbacBuilder ComProvisaoId(TipoProvisaoEnum provisaoId)
        {
            Instance.ProvisaoId = (short)provisaoId;
            return this;
        }

        public PmbacBuilder Padrao()
        {
            ComProvisaoId(TipoProvisaoEnum.PMBAC);
            ComValorProvisao(IdentificadoresPadrao.ValorProvisao);
            ComDataCompetencia(IdentificadoresPadrao.Competencia);
            ComFator(IdentificadoresPadrao.Fator);
            ComDesvio(IdentificadoresPadrao.Desvio);
            return this;
        }
    }
}
