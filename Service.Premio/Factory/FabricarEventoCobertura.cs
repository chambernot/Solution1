using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Service.Premio.Mapear;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Service.Premio.Factory
{
    public class FabricarEventoCobertura
    {
        
        private readonly ICalculoFacade _calculoFacade;
        private CoberturaContratada coberturacontratada;
        private readonly ICoberturas _cobertura;
        private readonly IProvisoes _provisao;
        private long itemCertificadoApoliceId;
        private EventoCobertura evento;

        public FabricarEventoCobertura(ICoberturas cobertura, IProvisoes provisao, ICalculoFacade calculoFacade)
        {
            _calculoFacade = calculoFacade;
            _provisao = provisao;
            _cobertura = cobertura;
           
        }

        public  async Task<EventoCobertura> Com(IEvento message)
        {
            
            
            switch (message)
            {
                
                case ICoberturaSaldada eventosaldada:
                    evento =  new EventoSaldamento(message.Identificador, message.IdentificadorCorrelacao, message.IdentificadorNegocio, message.DataExecucaoEvento);
                    evento.ComHistorico(await RetornaHistoricoCoberturaPeloIdentificador(eventosaldada.IdentificadorExternoCobertura, eventosaldada.ValorBeneficio, eventosaldada.ValorCapitalSegurado, StatusCobertura.StatusCoberturaEnum.Saldamento));
                    await PreencherProvisaoes(eventosaldada.IdentificadorExternoCobertura, evento);
                    break;
                default:
                    evento = null;
                    break;
            }
            
            
            return evento;
        }

        private async Task<HistoricoCoberturaContratada> RetornaHistoricoCoberturaPeloIdentificador(string ItemCertificadoApoliceId, decimal valorbeneficio, decimal valorcapital, StatusCobertura.StatusCoberturaEnum statuscobertura)
        {
            itemCertificadoApoliceId = Convert.ToInt64(ItemCertificadoApoliceId);
            coberturacontratada = await _cobertura.ObterPorItemCertificado(itemCertificadoApoliceId);
            coberturacontratada.Historico.ValorBeneficio = valorbeneficio;
            coberturacontratada.Historico.ValorCapital = valorcapital;
            coberturacontratada.Historico.InformaStatus(statuscobertura, DateTime.Now);
            coberturacontratada.Historico.AdicionarCobertura(coberturacontratada);
            return coberturacontratada.Historico;
        }

        private async Task PreencherProvisaoes(string ItemCertificadoApoliceId, EventoCobertura evento)
        {
             
            itemCertificadoApoliceId = Convert.ToInt64(ItemCertificadoApoliceId);
            var provisoescobertura = await _cobertura.ObterProvisaoPorCobertura(itemCertificadoApoliceId);
            new CalculadorEventoCobertura(provisoescobertura, evento, _calculoFacade, _provisao).CalcularProvisao();
           
        }
    }
}
