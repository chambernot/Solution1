using Mongeral.Infrastructure.Extensions;
using Mongeral.Infrastructure.ServiceBus.Subscribe;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Eventos
{
    public class CancelamentoFaturaVgService : OrchestrationSubscribeService<IParcelaFaturaCancelada>
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
                new ProvisaoDto
                {
                    Valor = 0,
                    DataMovimentacao = new DateTime(dataEvento.Year, dataEvento.Month, 1),
                    ProvisaoId = (short) TipoProvisaoEnum.PPNG
                } as IProvisao
            };

            parcela.Provisoes = movimentos;

            return movimentos;
        }

    }
}
