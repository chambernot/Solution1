using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class AjustePremioExtensions
    {
        public static void Validar(this IParcelaAjustada premio)
        {
            premio.ValidarEvento();

            premio.Parcelas.ForEach(p => p.Validar());
        }
    }
}
