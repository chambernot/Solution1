using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Extensions;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Services
{
    public class PremioService: IPremioService
    {
        private readonly IPremios _premios;
        private readonly ICoberturas _coberturas;
        private readonly ICalculadorProvisaoPremio _calculador;
        
        public PremioService(IPremios premios, ICoberturas coberturas, ICalculadorProvisaoPremio calculador)
        {
            _premios = premios;
            _coberturas = coberturas;
            _calculador = calculador;
        }

        public async Task<Premio> CriarPremio(Premio premio, EventoPremio evento)
        {
            var cobertura = await _coberturas.ObterPorItemCertificado(premio.ItemCertificadoApoliceId);

            if (!evento.RegimeFinanceiroPermitido.Contains(cobertura.RegimeFinanceiroId.GetValueOrDefault()))
                return null;

            var contribuicoes = await ObterNumeroContribuicoes(premio, cobertura.NumeroContribuicoesInicial);

            cobertura.ComNumeroContribuicoes(contribuicoes);

            premio.ComCobertura(cobertura)
                .ComTipoMovimento((short)evento.TipoMovimento)
                .InformaEvento(evento);

            await ValidarPremioAnterior(premio);

            premio.AdicionarMovimentoProvisao(_calculador.CriarProvisao(premio));            

            return premio;
        }

        private async Task<int> ObterNumeroContribuicoes(Premio premio, int numeroContibuicoesInicial)
        {
            var apropriacoes = (await _premios.ObterPremiosPorCertificado(premio.ItemCertificadoApoliceId, TipoMovimentoEnum.Apropriacao)).Count();

            var desapropriacoes = (await _premios.ObterPremiosPorCertificado(premio.ItemCertificadoApoliceId, TipoMovimentoEnum.Desapropriacao)).Count();

            return numeroContibuicoesInicial + apropriacoes - desapropriacoes;
        }

        private async Task ValidarPremioAnterior(Premio premio)
        {
            var premioAnterior = await _premios.ObterPremioAnterior(premio.ItemCertificadoApoliceId, premio.Numero);

            premio.ValidaPremioAnterior(premioAnterior);
        }
    }
}
