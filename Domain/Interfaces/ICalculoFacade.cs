using System;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface ICalculoFacade
    {
        void ValidarDadosCalculoPMBAC(CoberturaContratada cobertura, DateTime dataExecucao);
        ProvisaoMatematicaBeneficioAConceder CalcularPMBAC(CoberturaContratada premio, DateTime dataExecucao, decimal valorUltimaProvisao);
    }
}
