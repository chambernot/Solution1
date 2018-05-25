using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Service.Premio.Mapear
{
    public class PropostaToEventoAlteracao
    {
        private readonly ICoberturas _coberturas;

        public PropostaToEventoAlteracao(ICoberturas coberturas)
        {
            _coberturas = coberturas;
        }

        public async Task<EventoAlteracao> ToEventoAlteracao(IProposta proposta)
        {
            var historicos = new List<HistoricoCoberturaContratada>();

            foreach (var produto in proposta.Produtos)
            {
                var beneficiario = produto.Beneficiarios?.OrderByDescending(d => d.DataNascimento).FirstOrDefault();
                foreach (var cobertura in produto.Coberturas)
                {
                    var historico = new HistoricoCoberturaContratada(await _coberturas.ObterPorItemCertificado(long.Parse(cobertura.IdentificadorExterno)))
                    {
                        SexoBeneficiario = beneficiario?.Sexo,
                        DataNascimentoBeneficiario = beneficiario?.DataNascimento,
                        PeriodicidadeId = (short)proposta.DadosPagamento?.Periodicidade,
                        ValorBeneficio = cobertura.ValorBeneficio,
                        ValorCapital = cobertura.ValorCapital,
                        ValorContribuicao = cobertura.ValorContribuicao,
                    };

                    historico.InformaStatus(StatusCobertura.StatusCoberturaEnum.Activa, proposta.DataImplantacao);

                    historicos.Add(historico);
                }
            }

            return new EventoAlteracao(proposta.Identificador, proposta.IdentificadorCorrelacao, proposta.IdentificadorNegocio, proposta.DataExecucaoEvento)
                .ComHistorico(historicos);
        }
    }
}
