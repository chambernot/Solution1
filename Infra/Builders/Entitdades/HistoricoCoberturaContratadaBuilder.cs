using System;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Testes.Infra.Builders;


namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class HistoricoCoberturaContratadaBuilder : BaseBuilder<HistoricoCoberturaContratada>
    {           
        private HistoricoCoberturaContratadaBuilder(CoberturaContratada cobertura)
        {   
            Instance = new HistoricoCoberturaContratada(cobertura);            
        }        

        public static HistoricoCoberturaContratadaBuilder UmHistorico()
        {
            var cobertura = CoberturaContratadaBuilder.Uma().Build();

            return new HistoricoCoberturaContratadaBuilder(cobertura);
        }

        public HistoricoCoberturaContratadaBuilder ComId(Guid id)
        {
            Instance.Id = id;
            return this;
        }

        public HistoricoCoberturaContratadaBuilder ComDadosPadroes()
        {
            Instance.DataNascimentoBeneficiario = IdentificadoresPadrao.DataNascimento;
            Instance.SexoBeneficiario = IdentificadoresPadrao.Sexo;
            Instance.PeriodicidadeId = IdentificadoresPadrao.PeriodicidadeId;
            Instance.ValorBeneficio = IdentificadoresPadrao.ValorBeneficio;
            Instance.ValorCapital = IdentificadoresPadrao.ValorCapital;
            Instance.ValorContribuicao = IdentificadoresPadrao.ValorContribuicao;

            return this;
        }
        
        public HistoricoCoberturaContratadaBuilder ComSexoBeneficiario(string sexoBeneficiario)
        {
            Instance.SexoBeneficiario = sexoBeneficiario;
            return this;
        }
        
        public HistoricoCoberturaContratadaBuilder ComDataNascimento(DateTime dataNascimento)
        {
            Instance.DataNascimentoBeneficiario = dataNascimento;
            return this;
        }
        public HistoricoCoberturaContratadaBuilder ComPeriodicidadeId(int periodicidadeId)
        {
            Instance.PeriodicidadeId = periodicidadeId;
            return this;
        }

        public HistoricoCoberturaContratadaBuilder Com(CoberturaContratadaBuilder cobertura)
        {
            Instance.AdicionarCobertura(cobertura.Build());
            return this;
        }
    }
}
