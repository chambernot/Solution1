using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Enum;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Integrador.Contratos.VG.Eventos;
using System;
using System.Collections.Generic;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders.Dto
{
    public class ParcelaFaturaEmitidaDtoBuilder : BaseBuilder<ParcelaFaturaEmitidaDto>
    {
        private ParcelaFaturaEmitidaDtoBuilder() { Instance = new ParcelaFaturaEmitidaDto(); }

        public static ParcelaFaturaEmitidaDtoBuilder UmBuilder()
        {
            return new ParcelaFaturaEmitidaDtoBuilder();
        }

        public ParcelaFaturaEmitidaDtoBuilder Padrao()
        {
            Instance.Identificador = Guid.NewGuid();
            Instance.IdentificadorNegocio = IdentificadoresPadrao.IdentificadorNegocio;
            Instance.DataExecucaoEvento = IdentificadoresPadrao.Competencia;

            var parcela = new ParcelaDto();

            parcela.ParcelaId = new ParcelaIdDto()
            {
                IdentificadorExternoCobertura = IdentificadoresPadrao.ItemCertificadoApoliceId.ToString(),
                NumeroParcela = IdentificadoresPadrao.NumeroParcela
            };

            parcela.Valores = new ValoresDto()
            {
                Contribuicao = IdentificadoresPadrao.ValorContribuicao,
                IOF = IdentificadoresPadrao.ValorIOF,
                Carregamento = IdentificadoresPadrao.ValorCarregamento,
                ProLaboreDescontado = IdentificadoresPadrao.ValorProlaboreDescontado,
                Beneficio = IdentificadoresPadrao.ValorBeneficio,
                CapitalSegurado = IdentificadoresPadrao.ValorCapitalSegurado
            };

            parcela.Vigencia = new VigenciaDto()
            {
                Competencia = IdentificadoresPadrao.Competencia,
                Inicio = IdentificadoresPadrao.DataInicioVigencia,
                Fim = IdentificadoresPadrao.DataFimVigencia
            };

            Instance.Parcelas.Add(parcela);
            return this;
        }
    }

    public class ParcelaFaturaEmitidaDto : IParcelaFaturaEmitida
    {
        public List<IParcela> Parcelas => throw new NotImplementedException();

        public Guid Identificador { get; set; }
        public string IdentificadorCorrelacao { get; set; }
        public string IdentificadorNegocio { get; set; }
        public DateTime DataExecucaoEvento { get; set; }
        public int TotalItens { get; set; }
        public int ItemAtual { get; set; }
    }

    public class ParcelaDto : IParcela
    {
        public IParcelaId ParcelaId { get; set; }

        public IValores Valores { get; set; }

        public IVigencia Vigencia { get; set; }

        public IList<IResseguro> Resseguros { get; set; }
        public IList<IProvisao> Provisoes { get; set; }
        public IList<Integrador.Contratos.Premio.IComissao> Comissoes { get; set; }
    }

    public class ParcelaIdDto : IParcelaId
    {
        public string IdentificadorExternoCobertura { get; set; }

        public int NumeroParcela { get; set; }

        public int NumeroEndosso { get; set; }

        public long ParcelaId { get; set; }

        public long FaturaId { get; set; }

        public int NumeroFatura { get; set; }

        public short NumeroFaturaComplementar { get; set; }
    }

    public class ValoresDto : IValores
    {
        public decimal Contribuicao { get; set; }
        public decimal IOF { get; set; }
        public decimal Carregamento { get; set; }
        public decimal ProLaboreDescontado { get; set; }
        public decimal Beneficio { get; set; }
        public decimal CapitalSegurado { get; set; }
    }

    public class VigenciaDto : IVigencia
    {
        public DateTime Competencia { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Vencimento { get; set; }
    }

    public class ComissaoDto : Integrador.Contratos.IComissao
    {
        public int CodigoProdutor { get; set; }

        public TipoComissaoEnum TipoComissao { get; set; }

        public decimal ValorBase { get; set; }

        public decimal ValorComissao { get; set; }

        public decimal PercentualComissao { get; set; }
    }
}
