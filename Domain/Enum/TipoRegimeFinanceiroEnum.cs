using System.ComponentModel;

namespace Mongeral.Provisao.V2.Domain.Enum
{
    public enum TipoRegimeFinanceiroEnum: short
    {
        [Description("CAPITALIZAÇÃO")]
        Capitalizacao = 1,
        [Description("REPARTIÇÃO SIMPLES")]
        ReparticaoSimples = 2,
        [Description("REPARTIÇÃO DE CAPITAIS DE COBERTURA")]
        ReparticaoCapitaisCobertura = 3,
        [Description("FUNDO DE ACUMULAÇÃO")]
        FundoAcumulacao = 4
    }
}
