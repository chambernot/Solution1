using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class ImplantacaoPropostaService : OrchestrationSubscribeService<IProposta>
    {
        private IExecucaoEvento _app;        

        public ImplantacaoPropostaService(IExecucaoEvento app)
        {            
            _app = app;
        }

        public async Task<IProposta> Execute(IProposta message)
        {
            message.Validar();

            var evento = message.ToEvento();

            if (!await _app.ExisteEvento(evento.Identificador))
                await _app.Executar(evento);

            return message;
        }

        public async Task Compensate(IProposta message)
        {
            await _app.Compensate(message.Identificador);
        }
    }
}
