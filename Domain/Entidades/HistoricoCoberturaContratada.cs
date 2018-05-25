using Mongeral.Infrastructure.Domain.Model;
using System;
using Mongeral.Provisao.V2.Domain.Dominios;

namespace Mongeral.Provisao.V2.Domain
{
    public class HistoricoCoberturaContratada: Entidade<Guid>
    {        
        public HistoricoCoberturaContratada() { }
        public HistoricoCoberturaContratada(CoberturaContratada cobertura)
        {
            Cobertura = cobertura;
        }

        private EventoOperacional _evento;
        public Guid? EventoId => _evento?.Id;
        public Guid CoberturaContratadaId => Cobertura.Id;
        public int?  PeriodicidadeId { get; set; }
        public int Sequencia { get; set; }
        public DateTime? DataNascimentoBeneficiario { get; set; }
        public string SexoBeneficiario { get; set; }
        public decimal? ValorCapital { get; set; }
        public decimal? ValorContribuicao { get; set; }
        public decimal? ValorBeneficio { get; set; }
        protected short StatusId { get; set; }
        public DateTime DataStatus { get; protected set; }
        public StatusCobertura Status => StatusCobertura.Instance(StatusId);
        public CoberturaContratada Cobertura { get; protected set; }

        public void InformaEvento(EventoOperacional eventoAlteracao)
        {
            this._evento = eventoAlteracao;
        }

       

        public void InformaStatus(StatusCobertura.StatusCoberturaEnum status, DateTime dataStatus)
        {
            DataStatus = dataStatus;
            StatusId = (short) status;
        }

        public void AdicionarCobertura(CoberturaContratada cobertura)
        {
            Cobertura = cobertura;
        }

        public void AdicionarEvento(Guid eventoId)
        {
            _evento.Id = eventoId;
        }
    }
}
