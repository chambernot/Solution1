using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain.Factories;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class ReemissaoPremioVgService : OrchestrationSubscribeService<IParcelaFaturaReemitida>
    {
        private CalculadorProvisaoPremioNaoGanhoPremio _calculaProvisao;

        public ReemissaoPremioVgService(CalculadorProvisaoPremioNaoGanhoPremio calculaProvisao)
        {
            _calculaProvisao = calculaProvisao;
        }

        public Task<IParcelaFaturaReemitida> Execute(IParcelaFaturaReemitida message)
        {
            message.Parcelas.ForEach(parcela => parcela.Provisoes = _calculaProvisao.CalcularProvisao(parcela, message.DataExecucaoEvento).ToList<IProvisao>());

            return Task.FromResult(message);
        }

        public Task Compensate(IParcelaFaturaReemitida message)
        {
            return Task.FromResult(message);
        }
    }
}
