
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;


namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class CalculadorEventoCobertura : CalculadorEvento
    {
        public CalculadorEventoCobertura(CoberturaContratada coberturaContratada, EventoCobertura eventooperacional, ICalculoFacade calculo, IProvisoes provisao) : base(coberturaContratada, eventooperacional, calculo, provisao)
        {
            eventooperacional.MovimentosProvisao = movimentosProvisao;
        }
    }
}
