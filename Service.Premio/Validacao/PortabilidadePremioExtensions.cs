using Mongeral.Integrador.Contratos.Premio;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class PortabilidadePremioExtensions
    {
        public static void Validar(this IPortabilidadeApropriada premio)
        {
            premio.ValidarEvento();

            foreach (var portabilidade in premio.Portabilidades)
            {
                portabilidade.Validar();
                portabilidade.Pagamento.Validar(portabilidade.ParcelaId.IdentificadorExternoCobertura);
            }
        }
    }
}
