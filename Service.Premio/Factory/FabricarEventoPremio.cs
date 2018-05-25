using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class FabricarEventoPremio
    {
        private readonly IPremioService _premios;
        private readonly ParcelaToPremio _mapear;

        public FabricarEventoPremio(IPremioService premios, ParcelaToPremio mapear)
        {
            _mapear = mapear;
            _premios = premios;
        }
        
        public async Task<EventoPremio> Com(IParcelaEmitida message)
        {
            var evento = new EventoEmissaoPremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);
            
            foreach (var parcela in message.Parcelas)
            {
                evento.AdicionarPremio(await _premios.CriarPremio(_mapear.ToPremio(parcela), evento));
            }

            return evento;
        }

        public async Task<EventoPremio> Com(IParcelaApropriada message)
        {
            var evento = new EventoApropriacaoPremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);

            foreach (var parcela in message.Parcelas)
            {
                var premio = (await _premios.CriarPremio(_mapear.ToPremio(parcela), evento))
                    .ComPagamento(_mapear.ToPagamentoPremio(parcela.Pagamento));

                evento.AdicionarPremio(premio);
            }

            return evento;
        }

        public async Task<EventoPremio> Com(IParcelaDesapropriada message)
        {
            var evento = new EventoDesapropriacaoPremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);

            foreach (var parcela in message.Parcelas)
            {
                var premio = (await _premios.CriarPremio(_mapear.ToPremio(parcela), evento))
                    .ComPagamento(_mapear.ToPagamentoPremio(parcela.Pagamento));

                evento.AdicionarPremio(premio);
            }

            return evento;
        }

        public async Task<EventoPremio> Com(IParcelaAjustada message)
        {
            var evento = new EventoAjustePremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);

            foreach (var parcela in message.Parcelas)
            {
                evento.AdicionarPremio(await _premios.CriarPremio(_mapear.ToPremio(parcela), evento));
            }

            return evento;
        }

        public async Task<EventoPremio> Com(IAporteApropriado message)
        {
            var evento = new EventoAportePremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);

            foreach (var parcela in message.Aportes)
            {
                var premio = (await _premios.CriarPremio(_mapear.ToPremio(parcela), evento))
                    .ComPagamento(_mapear.ToPagamentoPremio(parcela.Pagamento))
                    .ComNumero(evento.ParcelaAporte);                    

                evento.AdicionarPremio(premio);
            }
            return evento;
        }

        public async Task<EventoPremio> Com(IPortabilidadeApropriada message)
        {
            var evento = new EventoPortabilidadePremio(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);

            foreach (var parcela in message.Portabilidades)
            {
                var premio = (await _premios.CriarPremio(_mapear.ToPremio(parcela), evento))
                    .ComPagamento(_mapear.ToPagamentoPremio(parcela.Pagamento))
                    .ComNumero(evento.ParcelaPortabilidade)
                    .ComCodigoSusep(parcela.CodigoSusep);

                evento.AdicionarPremio(premio);
            }
            return evento;
        }
    }
}
