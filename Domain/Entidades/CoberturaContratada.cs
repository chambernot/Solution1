using System;
using Mongeral.Infrastructure.Domain.Model;
using Mongeral.Provisao.V2.Domain.Dominios;
using Mongeral.Provisao.V2.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Domain
{
    public class CoberturaContratada: Entidade<Guid>
    {
        private EventoImplantacao _eventoImplantacao;

        protected CoberturaContratada() { _listaProvisaoCobertura = new List<Domain.ProvisaoCobertura>(); }

        public CoberturaContratada(long itemCertificadoApoliceId)
        {
            ItemCertificadoApoliceId = itemCertificadoApoliceId;
            
        }
        
        public Guid EventoId => _eventoImplantacao.Id;
        public long InscricaoId { get; set; }
        public long ItemCertificadoApoliceId { get; set; }
        public int ProdutoId { get; set; }
        public int ItemProdutoId { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public DateTime DataAssinatura { get; set; }
        public int? ClasseRiscoId { get; set; }
        public int TipoFormaContratacaoId { get; set; }
        public int? TipoRendaId { get;set; }
        public long Matricula { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? PrazoPagamentoEmAnos { get; set; }
        public int? PrazoCoberturaEmAnos { get; set; }
        public int? PrazoDecrescimoEmAnos { get; set; }        
        public HistoricoCoberturaContratada Historico { get; set; }
        public int? IndiceBeneficioId { get; protected set; }
        public int? IndiceContribuicaoId { get; protected set; }
        public int ModalidadeCoberturaId { get; protected set; }
        public short? RegimeFinanceiroId { get; set; }
        public int TipoItemProdutoId { get; set; }
        public string NomeProduto { get; protected set; }
        public short? NumeroBeneficioSusep { get; protected set; }
        public string NumeroProcessoSusep { get; protected set; }
        public int PlanoFipSusep { get; protected set; }
        public int TipoProvisoes { get; protected set; }
        public int NumeroContribuicoesInicial { get; set; }
        public bool PermiteResgateParcial { get; protected set; }
        public int NumeroContribuicao { get; set; }
        public DateTime DataUltimaCorrecaoMonetaria { get; set; }
        public DateTime DataSaldamento { get; set; }

        protected List<ProvisaoCobertura> _listaProvisaoCobertura;
        public IEnumerable<ProvisaoCobertura> ProvisaoCobertura => _listaProvisaoCobertura.AsEnumerable();

        public void AdicionarProvisao(IEnumerable<ProvisaoCobertura> listaProvisaoCobertura)
        {
            if (listaProvisaoCobertura == null)
                return;

            foreach (var Provisao in listaProvisaoCobertura)
            {
                _listaProvisaoCobertura.Add(Provisao);
            }
        }

        public CoberturaContratada ComNumeroContribuicoes(int contribuicoes)
        {
            NumeroContribuicao = contribuicoes;
            return this;
        }
        public int Status
        {
            get {
                return (int)Historico.Status.Staus;
            }
            set {
                Historico.InformaStatus((StatusCobertura.StatusCoberturaEnum)value, DateTime.Now);
            }
        }

        public virtual Premio CriarPremio(Premio premioEmitido)
        {
            throw new NotImplementedException();
        }

        public void AdicionarTipoProvisoes(int tipoProvisoes)
        {
            TipoProvisoes = tipoProvisoes;
        }

        public void AdicionarEventoId(Guid eventoId)
        {
            _eventoImplantacao.Id = eventoId;
        }

        public CoberturaContratada ComDadosProduto(DadosProduto dadosProduto)
        {
            ProdutoId = dadosProduto.ProdutoId;
            NumeroProcessoSusep = dadosProduto.NumeroProcessoSusep;
            IndiceBeneficioId = dadosProduto.IndiceBeneficioId;
            IndiceContribuicaoId = dadosProduto.IndiceContribuicaoId;
            ModalidadeCoberturaId = dadosProduto.ModalidadeCoberturaId;
            NumeroBeneficioSusep = dadosProduto.NumeroBeneficioSusep;
            NomeProduto = dadosProduto.NomeProduto;
            TipoProvisoes = dadosProduto.ProvisoesPossiveis;
            PermiteResgateParcial = dadosProduto.PermiteResgateParcial;
            PlanoFipSusep = dadosProduto.PlanoFipSusep;
            TipoItemProdutoId = dadosProduto.TipoItemProdutoId;
            RegimeFinanceiroId = dadosProduto.RegimeFinanceiroId;

            return this;
        }

        public void InformaEvento(EventoImplantacao evento)
        {
            _eventoImplantacao = evento;
        }
    }
}
