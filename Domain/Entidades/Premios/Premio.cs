using Mongeral.Infrastructure.Domain.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Domain
{
    public class Premio: Entidade<Guid>
    {
        public Premio()
        {
            _listaMovimentoProvisao = new List<MovimentoProvisao>();
            _pagamento = new PagamentoPremio();
        }

        public EventoPremio EventoOperacional { get; protected set; }
        public EventoCobertura EventoOperacionalCobertura { get; protected set; }
        public Guid? EventoId => EventoOperacional?.Id;
        public short TipoMovimentoId { get; set; }
        public int Numero { get; set; }
        public long ItemCertificadoApoliceId { get; set; }
        public DateTime Competencia { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FimVigencia { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataApropriacao { get; set; }
        public decimal ValorPremio { get; set; }
        public decimal ValorCarregamento { get; set; }
        public decimal ValorBeneficio { get; set; }
        public decimal ValorCapitalSegurado { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Multa { get; set; }
        public decimal? IOFRetido { get; set; }
        public decimal? IOFARecolher { get; set; }        
        public string IdentificadorCredito { get; set; }
        public string CodigoSusep { get; set; }
        public decimal ValorProvisaoAnterior { get; set; }
        public Guid HistoricoCoberturaId => Cobertura.Historico.Id;
        public CoberturaContratada Cobertura { get; protected set; }
        private PagamentoPremio _pagamento;
        public PagamentoPremio Pagamento => _pagamento;
        protected List<MovimentoProvisao> _listaMovimentoProvisao;
        public IEnumerable<MovimentoProvisao> MovimentosProvisao => _listaMovimentoProvisao.AsEnumerable();

        public void InformaEvento(EventoPremio evento)
        {
            Assertion.NotNull(evento, "O evento operacional não foi informado").Validate();
            EventoOperacional = evento;
        }

        public void InformaEventoCobertura(EventoCobertura evento)
        {
            Assertion.NotNull(evento, "O evento operacional não foi informado").Validate();
            EventoOperacionalCobertura = evento;
            
        }
        public Premio ComCobertura(CoberturaContratada cobertura)
        {
            Assertion.NotNull(cobertura, "Não foi informada a cobertura do prêmio").Validate();
            Cobertura = cobertura;

            return this;
        }        

        public Premio ComTipoMovimento(short tipoMovimentoId)
        {
            TipoMovimentoId = tipoMovimentoId;
            return this;
        }

        public Premio ComPagamento(PagamentoPremio pagamento)
        {
            if (pagamento == null)
                return this;

            DataApropriacao = pagamento.DataApropriacao;
            DataPagamento = pagamento.DataPagamento;
            Desconto = pagamento.Desconto;
            Multa = pagamento.Multa;
            ValorPago = pagamento.ValorPago;
            IOFRetido = pagamento.IOFRetido;
            IOFARecolher = pagamento.IOFARecolher;
            IdentificadorCredito = pagamento.IdentificadorCredito;

            return this;
        }

        public Premio ComCodigoSusep(string codigo)
        {
            CodigoSusep = codigo;
            return this;
        }

        public Premio ComNumero(int numero)
        {
            Numero = numero;
            return this;
        }

        public IList<TipoProvisaoEnum> ObterTiposProvisaoPossiveis()
        {
            IList<TipoProvisaoEnum> provisoes = new List<TipoProvisaoEnum>();

            if (Cobertura.RegimeFinanceiroId.Equals(default(short)))
                return provisoes;

            var provisoesPossiveis = EventoOperacional.ObterProvisaoPossiveisDoEvento((short)Cobertura.RegimeFinanceiroId);

            foreach (var eventoProvisoesPossiveis in provisoesPossiveis)
            {
                var prov = Cobertura.TipoProvisoes & (int)eventoProvisoesPossiveis;

                if (prov == (int)eventoProvisoesPossiveis)
                {
                    provisoes.Add((TipoProvisaoEnum)prov);
                }
            }

            return provisoes;
        }

        public void AdicionarMovimentoProvisao(IEnumerable<MovimentoProvisao> listaMovimentoProvisao)
        {
            if (listaMovimentoProvisao == null)
                return;

            foreach (var movimentoProvisao in listaMovimentoProvisao)
            {
                _listaMovimentoProvisao.Add(movimentoProvisao);
            }
        }
    }   
}