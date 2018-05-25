using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service.Coberturas
{
    public abstract class ProcessarCoberturaBaseTest : ImplantacaoBaseTest
    {
        protected IParcelaEmitida _emissaoPremio;
        protected IParcelaApropriada _apropriacaoPremio;
        protected IPremios _premios;
        public ICobertura cobertura;

        [OneTimeSetUp]
        protected new void FixtureSetUp()
        {
            ProcessarImplatacao(true, true);

            _premios = GetInstance<IPremios>();
            cobertura = _proposta.Produtos.First().Coberturas.First();

        }


        public void ProcessarEmissao()
        {
            _emissaoPremio = ObterContratoEmissaoPremio();
            GetInstance<EmissaoPremioService>().Execute(_emissaoPremio).Wait();
        }

        public void ProcessarApropriacao()
        {
            _apropriacaoPremio = ObterContratoParcelaApropriada();

            GetInstance<ApropriacaoPremioService>().Execute(_apropriacaoPremio).Wait();
        }


        private IParcelaEmitida ObterContratoEmissaoPremio()
        {
            

            return ParcelaEmitidaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(_proposta.DataExecucaoEvento)
                .Com(ParcelaBuilder.UmBuilder()
                    .Com(ParcelaIdBuilder.UmBuilder()
                        .ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno)
                        .ComNumeroParcela(IdentificadoresPadrao.NumeroParcela))
                    .Com(ValorBuilder.UmBuilder().Padrao())
                    .Com(VigenciaBuilder.UmBuilder().Padrao()))
                .Build();
        }

        private IParcelaApropriada ObterContratoParcelaApropriada()
        {
            

            var apropriacao = ApropriacaoBuilder.UmBuilder()
                .Com(PagamentoBuilder.UmBuilder().Padrao())
                .ComValorBuilder(ValorBuilder.UmBuilder().Padrao())
                .ComVigenciaBuilder(VigenciaBuilder.UmBuilder().Padrao())
                .ComParcelaBuilder(ParcelaIdBuilder.UmBuilder()
                    .ComNumeroParcela(12).ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno));

            return ParcelaApropriadaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(DateTime.Now)
                .Com((ApropriacaoBuilder)apropriacao)
                .Build();
        }

    }
}
