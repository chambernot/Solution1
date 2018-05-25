namespace Mongeral.Provisao.V2.Domain.Enum
{
    public enum TipoEventoEnum: short
    {
        Implantacao = 1,
        AlteracaoParametros = 2,
        EmissaoPremio = 3,
        ApropriacaoPremio = 4,
        DesapropriacaoPremio = 5,
        AjustePremio = 6,
        PortabilidadePremio = 7,
        AportePremio = 8,
        CancelamentoPremio = 9,
        InclusaoVg = 10,
        EmissaoPremioVg = 11,
        Saldamento = 12
    }
}
