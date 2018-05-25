using System;
using System.Threading.Tasks;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;

namespace Mongeral.Provisao.V2.Application.Validador
{
    public class ValidadorCobertura
    {
        private readonly ICoberturas _coberturas;
        private readonly ICalculoFacade _facade;

        public ValidadorCobertura(
            ICoberturas cobertura,
            ICalculoFacade facade)
        {
            _coberturas = cobertura;
            _facade = facade;
        }

        public async Task Validar(CoberturaContratada cobertura)
        {
            var existe = await _coberturas.Contem(cobertura.ItemCertificadoApoliceId);

            Assertion.IsFalse(existe, $"O identificador externo {cobertura.ItemCertificadoApoliceId} já foi implantado anteriormente.").Validate();

            if (cobertura.RegimeFinanceiroId.Equals((short) TipoRegimeFinanceiroEnum.Capitalizacao))
                _facade.ValidarDadosCalculoPMBAC(cobertura, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
        }
    }
}