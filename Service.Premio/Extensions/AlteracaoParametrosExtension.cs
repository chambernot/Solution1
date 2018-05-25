using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mongeral.Infrastructure.Extensions;
using Mongeral.Integrador.Contratos;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Service.Premio.Extensions
{
    public static class AlteracaoParametrosExtension
    {
        public static EventoAlteracao ToEventoAlteracao(this IProposta proposta)
        {
            var evento = new EventoAlteracao(proposta.Identificador,proposta.IdentificadorCorrelacao,proposta.IdentificadorNegocio,proposta.DataExecucaoEvento);

            proposta.ToHistoricoCoberturas().SafeForEach(h=> evento.AdicionaHistorico(h));

            return evento;
        }

        public static IEnumerable<HistoricoCoberturaContratada> ToHistoricoCoberturas(this IProposta proposta)
        {
            foreach (var produto in proposta.Produtos)
            {
                var beneficiario = produto.Beneficiarios?.OrderByDescending(d => d.DataNascimento).FirstOrDefault();
                foreach (var cobertura in produto.Coberturas)
                {
                    var cobContratada = new CoberturaContratada(Convert.ToInt64(cobertura.IdentificadorExterno));
                    yield return new HistoricoCoberturaContratada(cobContratada)
                    {   
                        SexoBeneficiario = beneficiario?.Sexo,
                        DataNascimentoBeneficiario = beneficiario?.DataNascimento,
                        PeriodicidadeId = (short)proposta.DadosPagamento.Periodicidade
                    };
                }
            }
        }
    }
}