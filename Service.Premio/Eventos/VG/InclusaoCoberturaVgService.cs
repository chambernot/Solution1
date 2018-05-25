using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.VG;
using Mongeral.Provisao.V2.Application.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class InclusaoCoberturaVgService : OrchestrationSubscribeService<IInclusaoCoberturaGrupal>
    {
        private IExecucaoEvento _app;

        public InclusaoCoberturaVgService(IExecucaoEvento app)
        {
            _app = app;
        }

        public async Task<IInclusaoCoberturaGrupal> Execute(IInclusaoCoberturaGrupal message)
        {
            message.Validar();

            var evento = message.ToEventoVG();

            if (!await _app.ExisteEvento(evento.Identificador))
                await _app.Executar(evento); 

            return message;
        }

        public async Task Compensate(IInclusaoCoberturaGrupal message)
        {
            await _app.Compensate(message.Identificador);
        }
    }
}
