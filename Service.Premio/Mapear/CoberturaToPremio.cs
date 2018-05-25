using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Mapear
{
    public class CoberturaToPremio
    {
        public Domain.Premio ToPremio(dynamic parcela)
        {
            return new Domain.Premio()
            {
                Id = parcela.PremioId,
                Numero = parcela.Numero ?? 0,
                Competencia = parcela.Competencia ?? DateTime.MinValue,
                InicioVigencia = parcela.InicioVigencia ?? DateTime.MinValue,
                FimVigencia = parcela.FimVigencia ?? DateTime.MinValue,
                ValorPremio = parcela.ValorPremio ?? 0.0M,
                ValorCarregamento = parcela.Valores?.Carregamento ?? 0.0M,
                ValorBeneficio = parcela.ValorBeneficio ?? 0.0M,
                ValorCapitalSegurado = parcela.ValorCapitalSegurado ?? 0.0M
            };
        }

        public Domain.EventoPremio ToEvento(dynamic evento, DateTime DataExecucaoEvento)
        {
            EventoPremio eventopremio;
            switch ((TipoEventoEnum)evento.TipoEventoId)
            {
                
               case TipoEventoEnum.EmissaoPremio:
                    eventopremio = new EventoEmissaoPremio(evento.Identificador, evento.IdentificadorCorrelacao, evento.IdentificadorNegocio, DataExecucaoEvento);
                    eventopremio.Id = evento.IDEvento;
                    break;
                case TipoEventoEnum.ApropriacaoPremio:
                    eventopremio = new EventoApropriacaoPremio(evento.Identificador, evento.IdentificadorCorrelacao, evento.IdentificadorNegocio, DataExecucaoEvento);
                    eventopremio.Id = evento.IDEvento;
                    break;
                case TipoEventoEnum.DesapropriacaoPremio:
                    eventopremio = new EventoDesapropriacaoPremio(evento.Identificador, evento.IdentificadorCorrelacao, evento.IdentificadorNegocio, DataExecucaoEvento);
                    eventopremio.Id = evento.IDEvento;
                    break;
                case TipoEventoEnum.AjustePremio:
                    eventopremio = new EventoAjustePremio(evento.Identificador, evento.IdentificadorCorrelacao, evento.IdentificadorNegocio, DataExecucaoEvento);
                    eventopremio.Id = evento.IDEvento;
                    break;
                case TipoEventoEnum.PortabilidadePremio:
                    eventopremio = new EventoPortabilidadePremio(evento.Identificador, evento.IdentificadorCorrelacao, evento.IdentificadorNegocio, DataExecucaoEvento);
                    eventopremio.Id = evento.IDEvento;
                    break;
                default:
                    eventopremio = null;
                    break;
            }

            return eventopremio;
        }
    }
}
