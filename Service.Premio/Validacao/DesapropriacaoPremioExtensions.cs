using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class DesapropriacaoPremioExtensions
    {
        public static void Validar(this IParcelaDesapropriada premio)
        {
            premio.ValidarEvento();

            premio.Parcelas.ForEach(p => p.Validar());
        }
    }
}
