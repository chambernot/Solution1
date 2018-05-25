using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Application
{
    public class ProcessaEventoAlteracaoParametros: IProcessaEvento<EventoAlteracao>
    {        
        private readonly IEventos<EventoAlteracao> _eventos;
        private readonly ICoberturas _coberturas;

        public ProcessaEventoAlteracaoParametros(IEventos<EventoAlteracao> eventos,ICoberturas coberturas)
        {
            _eventos = eventos;
            _coberturas = coberturas;
        }

        public async Task<EventoAlteracao> Processar(EventoAlteracao evento)
        {
            Assertion.NotNull(evento, "O eventos não foi informado.").Validate();
            Assertion.IsTrue(evento.Historicos.Any(), "Não foi informado nenhum novo histórico.").Validate();

            await ObtemIdentificadoresCoberturas(evento);

            await _eventos.Adicionar(evento);

            return evento;
        }

        private async Task ObtemIdentificadoresCoberturas(EventoAlteracao evento)
        {
            var certificados = evento.Historicos.Select(h => h.Cobertura.ItemCertificadoApoliceId).ToList();
            var coberturas = await _coberturas.ObterPorItensCertificadosApolices(certificados);

            foreach (var historico in evento.Historicos)
            {
                historico.Cobertura.Id = coberturas.First(x => x.ItemCertificadoApoliceId == historico.Cobertura.ItemCertificadoApoliceId).Id;
            }

            Assertion.IsFalse(evento.Historicos.Any(h => h.Cobertura.IsNew), "Não foram encontradas todas as coberturas").Validate();
        }

        public async Task Compensar(Guid identificador)
        {
            await _eventos.Remover(identificador);
        }
    }
}
