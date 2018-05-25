namespace Mongeral.Provisao.V2.Domain.Enum
{
    public enum TipoMovimentoEnum: short
    {
        Inexistente = -1,
        Emissao = 0,
        Apropriacao = 1,
        Desapropriacao = 2,
        Ajuste = 3,
        Portabilidade = 4,
        Aporte = 5,
        Reemissao = 6,
        CancelamentoAjuste = 7,
        CancelamentoEmissao = 8
    }
}
