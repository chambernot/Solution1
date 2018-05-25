using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;

namespace Mongeral.Provisao.V2.Service.Premio.VG
{
    public class ProcessaCancelamentoVgService : OrchestrationSubscribeService<IParcelaFaturaCancelada>
    {
        public Task<IParcelaFaturaCancelada> Execute(IParcelaFaturaCancelada message)
        {
            message.Parcelas.SafeForEach(parcela => CriarProvisaoPPNG(parcela, message.DataExecucaoEvento));
            return Task.FromResult(message);
        }

        public Task Compensate(IParcelaFaturaCancelada message)
        {
            return Task.CompletedTask;
        }

        private List<IProvisao> CriarProvisaoPPNG(IParcela parcela, DateTime dataEvento)
        {
            var movimentos = new List<IProvisao>
            {
                new MovimentoProvisao
                {
                    Valor = 0,
                    DataMovimentacao = new DateTime(dataEvento.Year, dataEvento.Month, 1),
                    ProvisaoId = (short) TipoProvisaoEnum.PPNG
                } as IProvisao
            };

            movimentos.SafeForEach(provisao => parcela.Provisoes.Add(provisao));

            return movimentos;
        }
    }
}
