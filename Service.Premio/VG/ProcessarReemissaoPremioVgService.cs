using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.VG
{
    public class ProcessarReemissaoPremioVgService: OrchestrationSubscribeService<IParcelaFaturaReemitida>
    {
        private CalculadorProvisaoPremioNaoGanhoPremio _calculaProvisao;

        public ProcessarReemissaoPremioVgService(CalculadorProvisaoPremioNaoGanhoPremio calculaProvisao)
        {
            _calculaProvisao = calculaProvisao;
        }

        public Task<IParcelaFaturaReemitida> Execute(IParcelaFaturaReemitida message)
        {
            message.Parcelas.ForEach(parcela => parcela.Provisoes = _calculaProvisao.CalcularProvisao(parcela,message.DataExecucaoEvento).ToList<IProvisao>());

            return Task.FromResult(message);
        }

        public Task Compensate(IParcelaFaturaReemitida message)
        {
            throw new NotImplementedException();
        }
    }
}
