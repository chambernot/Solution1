using System;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public abstract class EmissaoPremioBaseTest : ImplantacaoBaseTest
    {
        protected IParcelaEmitida _emissaoPremio;
        protected IPremios _premios;

        [OneTimeSetUp]
        protected new void FixtureSetUp()
        {
            ProcessarImplatacao(true, true);

            _premios = GetInstance<IPremios>();
            _emissaoPremio = ObterContratoEmissaoPremio();

            GetInstance<EmissaoPremioService>().Execute(_emissaoPremio).Wait();
        }

        private IParcelaEmitida ObterContratoEmissaoPremio()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();

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
    }

    public class ProcessarEmissaoPremioServiceTest: EmissaoPremioBaseTest
    {
        private IParcela _parcelaEmitida;
        private Premio _premioDto;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _parcelaEmitida = _emissaoPremio.Parcelas.First();

            _premioDto = await _premios.ObterPorItemCertificado<Premio>
                (long.Parse(_parcelaEmitida.ParcelaId.IdentificadorExternoCobertura), 
                (short)TipoMovimentoEnum.Emissao, 12);
        }

        [Test]
        public void CriouParcela()
        {
            Assert.That(_premioDto, Is.Not.Null, "Premio não cadastrado.");
            Assert.That(_parcelaEmitida.ParcelaId.IdentificadorExternoCobertura, Is.EqualTo(_premioDto.ItemCertificadoApoliceId.ToString()));
            Assert.That(_parcelaEmitida.ParcelaId.NumeroParcela, Is.EqualTo(_premioDto.Numero));
        }

        [Test]
        public void CriouParcelaVigencia()
        {
            Assert.That(_parcelaEmitida.Vigencia.Competencia, Is.EqualTo(_premioDto.Competencia));
            Assert.That(_parcelaEmitida.Vigencia.Inicio, Is.EqualTo(_premioDto.InicioVigencia));
            Assert.That(_parcelaEmitida.Vigencia.Fim, Is.EqualTo(_premioDto.FimVigencia));
        }

        [Test]
        public void CriouParcelaValores()
        {
            Assert.That(_parcelaEmitida.Valores.Contribuicao, Is.EqualTo(_premioDto.ValorPremio));
            Assert.That(_parcelaEmitida.Valores.Beneficio, Is.EqualTo(_premioDto.ValorBeneficio));
            Assert.That(_parcelaEmitida.Valores.Carregamento, Is.EqualTo(_premioDto.ValorCarregamento));
            Assert.That(_parcelaEmitida.Valores.CapitalSegurado, Is.EqualTo(_premioDto.ValorCapitalSegurado));
        }

        [Test]
        public async Task CompensouEventoEmissaoPremio()
        {
            GetInstance<EmissaoPremioService>().Compensate(_emissaoPremio).Wait();

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_emissaoPremio.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }
    }
}
