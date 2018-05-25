using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class AportePremioExtensions
    {
        public static void Validar(this IAporteApropriado premio)
        {
            premio.ValidarEvento();

            foreach (var aporte in premio.Aportes)
            {
                aporte.Validar();
                aporte.Pagamento.Validar(aporte.ParcelaId.IdentificadorExternoCobertura);
            }
        }
    }
}
