using Mongeral.Provisao.V2.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.Entidades;
using System.Linq;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Application
{
    public class ProcessaEmissaoPremio : IProcessaEvento<EventoPremio>
    {     
        private readonly IPremios _premios;
        private readonly IEventosBase<EventoEmissaoPremio> _eventos;
                
        public ProcessaEmissaoPremio(IPremios premios, IEventosBase<EventoEmissaoPremio> eventos)
        {   
            _premios = premios;
            _eventos = eventos;
        }

        public async Task Processar(EventoEmissaoPremio evento)
        {
            foreach (var premio in evento.PremiosValidos)
            {
                await evento.ValidaMovimentacao(premio.ItemCertificadoApoliceId, premio.Numero);
            }

            await _eventos.Salvar(evento);
        }

        public Task<EventoPremio> Processar(EventoPremio evento)
        {
            throw new NotImplementedException();
        }

        Task IProcessaEvento<EventoPremio>.Compensar(Guid identificador)
        {
            throw new NotImplementedException();
        }
    }
}
