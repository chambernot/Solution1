using System.Linq;
using System.Threading.Tasks;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain.Factories;

namespace Mongeral.Provisao.V2.Service.Premio.VG
{
    public class ProcessarEmissaoPremioVgService : OrchestrationSubscribeService<IParcelaFaturaEmitida>
    {        
        private readonly CalculadorProvisaoPremioNaoGanhoPremio _calculaProvisao;

        public ProcessarEmissaoPremioVgService(CalculadorProvisaoPremioNaoGanhoPremio calculaProvisao)
        {
            _calculaProvisao = calculaProvisao;
        }

        public Task<IParcelaFaturaEmitida> Execute(IParcelaFaturaEmitida message)
        {
            message.Parcelas.ForEach(parcela => parcela.Provisoes = _calculaProvisao.CalcularProvisao(parcela, message.DataExecucaoEvento).ToList<IProvisao>());

            return Task.FromResult( message ) ;
        }

        public Task Compensate(IParcelaFaturaEmitida message)
        {
            return Task.FromResult(message);
        }
    }
}
