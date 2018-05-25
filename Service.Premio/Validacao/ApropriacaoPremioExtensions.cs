using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos.Premio;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Validacao
{
    public static class ApropriacaoPremioExtensions
    {
        public static void Validar(this IParcelaApropriada premio)
        {
            premio.ValidarEvento();

            foreach (var parcela in premio.Parcelas)
            {
                parcela.Validar();
                parcela.Pagamento.Validar(parcela.ParcelaId.IdentificadorExternoCobertura);
            }
        }
    }

   
}
