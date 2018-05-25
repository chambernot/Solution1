using System;
using System.Reflection;
using Mongeral.Provisao.V2.Domain;
using static Mongeral.Provisao.V2.Domain.Dominios.StatusCobertura;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades
{
    public class CoberturaContratadaBuilder: BaseBuilder<CoberturaContratada>
    {
        private CoberturaContratadaBuilder(long itemCertificadoApoliceId)
        {
            Instance = new CoberturaContratada(itemCertificadoApoliceId);
        }
                
        public static CoberturaContratadaBuilder Uma()
        {
            return new CoberturaContratadaBuilder(IdentificadoresPadrao.ItemCertificadoApoliceId);
        }
                
        public CoberturaContratadaBuilder Com(HistoricoCoberturaContratadaBuilder historico)
        {
            Instance.Historico = historico.Build();
            return this;
        }

        public CoberturaContratadaBuilder Com(DadosProdutoBuilder dadosProduto)
        {
            Instance.ComDadosProduto(dadosProduto.Build());
            return this;
        }

        public CoberturaContratadaBuilder ComInscricaoCertificado(long inscricaoId)
        {
            Instance.InscricaoId = inscricaoId;
            return this;
        }

        public CoberturaContratadaBuilder ComItemProdutoId(int itemProdutoId)
        {
            Instance.ItemProdutoId = itemProdutoId;
            return this;
        }

        public CoberturaContratadaBuilder ComMatricula(long matricula)
        {
            Instance.Matricula = matricula;
            return this;
        }

        public CoberturaContratadaBuilder ComDataInicioVigencia(DateTime data)
        {
            Instance.DataInicioVigencia = data;
            return this;
        }

        public CoberturaContratadaBuilder ComDataAssinatura(DateTime data)
        {
            Instance.DataAssinatura = data;
            return this;
        }

        public CoberturaContratadaBuilder ComDataNascimento(DateTime data)
        {
            Instance.DataNascimento = data;
            return this;
        }

        public CoberturaContratadaBuilder ComSexo(string sexo)
        {
            Instance.Sexo = sexo;
            return this;
        }

        public CoberturaContratadaBuilder ComTipoFormaContratacao(int tipo)
        {
            Instance.TipoFormaContratacaoId = tipo;
            return this;
        }

        public CoberturaContratadaBuilder ComProdutoId(int codigo)
        {
            Instance.ProdutoId = codigo;
            return this;
        }

        public CoberturaContratadaBuilder ComClasseRisco(int classeRisco)
        {
            Instance.ClasseRiscoId = classeRisco;
            return this;
        }

        public CoberturaContratadaBuilder ComTipoRenda(int tipoRenda)
        {
            Instance.TipoRendaId = tipoRenda;
            return this;
        }

        public CoberturaContratadaBuilder ComDataFimVigencia(DateTime data)
        {
            Instance.DataFimVigencia = data;
            return this;
        }

        public CoberturaContratadaBuilder ComDataUltimaCorrecaoMonetaria(DateTime data)
        {
            Instance.DataUltimaCorrecaoMonetaria = data;
            return this;
        }


        public CoberturaContratadaBuilder ComPrazo(int prazoPagamento, int prazoDecrescimo, int prazoCobertura)
        {
            Instance.PrazoPagamentoEmAnos = prazoPagamento;
            Instance.PrazoCoberturaEmAnos = prazoCobertura;
            Instance.PrazoDecrescimoEmAnos = prazoDecrescimo;
            return this;
        }

        public CoberturaContratadaBuilder ComStatus(StatusCoberturaEnum status)
        {
            Instance.Status = (int)status;
            return this;
        }

        public CoberturaContratadaBuilder ComRegimeFinanceiro(short regimeFinanceiro)
        {
            Instance.RegimeFinanceiroId = regimeFinanceiro;
            return this;
        }

        public CoberturaContratadaBuilder ComId(Guid id)
        {
            Instance.Id = id;
            return this;
        }
                
        public CoberturaContratadaBuilder ComTipoItemProdutoId(int tipoItemProdutoId)
        {
            Instance.TipoItemProdutoId = tipoItemProdutoId;
            return this;
        }

        public CoberturaContratadaBuilder ComTiposProvisao(params TipoProvisaoEnum[] tipoProvisoesPossiveis)
        {
            var provisoesPossiveis = 0;
            foreach (var item in tipoProvisoesPossiveis)
            {
                provisoesPossiveis = provisoesPossiveis | (int)item;
            }

            Instance.AdicionarTipoProvisoes(provisoesPossiveis);
            return this;
        }

        public CoberturaContratadaBuilder Padrao()
        {
            ComInscricaoCertificado(IdentificadoresPadrao.InscricaoId);
            ComMatricula(IdentificadoresPadrao.Matricula);
            ComSexo(IdentificadoresPadrao.Sexo);
            ComDataNascimento(IdentificadoresPadrao.DataNascimento);
            ComDataInicioVigencia(IdentificadoresPadrao.DataInicioVigencia);
            ComDataFimVigencia(IdentificadoresPadrao.DataFimVigencia);
            ComDataAssinatura(IdentificadoresPadrao.DataAssinatura);
            ComTipoFormaContratacao(IdentificadoresPadrao.TipoFormaContratacaoId);            
            ComItemProdutoId(IdentificadoresPadrao.ItemProdutoId);
            ComTipoItemProdutoId(IdentificadoresPadrao.TipoItemProdutoId);
            ComRegimeFinanceiro(IdentificadoresPadrao.RegimeFinanceiroId);
            ComTiposProvisao(TipoProvisaoEnum.PMBAC);
            ComClasseRisco(IdentificadoresPadrao.ClasseRiscoId);
            ComTipoRenda(IdentificadoresPadrao.TipoRendaId);
            ComDataUltimaCorrecaoMonetaria(IdentificadoresPadrao.DataUltimaAtualizacaoContribuicao);
            ComPrazo(IdentificadoresPadrao.PrazoPagamento, IdentificadoresPadrao.PrazoDecrescimo, IdentificadoresPadrao.PrazoCobertura);
            return this;
        }
    }
}