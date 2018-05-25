using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class AlteracaoPropostaService : OrchestrationSubscribeService<IProposta>
    {
        private IExecucaoEvento _app;
        private PropostaToEventoAlteracao _mapear;

        public AlteracaoPropostaService(IExecucaoEvento app, PropostaToEventoAlteracao mapear)
        {
            _app = app;
            _mapear = mapear;
        }

        public async Task<IProposta> Execute(IProposta message)
        {
            message.Validar();

            var evento = await _mapear.ToEventoAlteracao(message);

            if (!await _app.ExisteEvento(message.Identificador))
                await _app.Executar(evento);

            return message;
        }

        public async Task Compensate(IProposta message)
        {
            await _app.Compensate(message.Identificador);
        }
    }
}
