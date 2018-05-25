using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using System.Threading.Tasks;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using System;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class EventoCoberturaFactory
    {
        private FabricarEventoCobertura _fabricarEventoCobertura;
        
        
      

        public EventoCoberturaFactory(FabricarEventoCobertura fabricarEventoCobertura, ICoberturas cobertura, IProvisoes provisao, CoberturaToPremio mapear)
        {
            _fabricarEventoCobertura = fabricarEventoCobertura;
          
        }
        
        public async Task<EventoCobertura> Fabricar(IEvento evento)
        {
            EventoCobertura eventoCobertura;

            eventoCobertura = await _fabricarEventoCobertura.Com(evento);
                   
           
            Assertion.NotNull(eventoCobertura, $"Impossivel criar um evento para {evento.GetType()}");
                      

            return eventoCobertura;
        }

       
               

    }
 

}
