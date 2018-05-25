using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class EmissaoPremioExtensions
    {
        public static void Validar(this IParcelaEmitida premio)
        {
            premio.ValidarEvento();

            premio.Parcelas.ForEach(p => p.Validar());
        }
    }
}
