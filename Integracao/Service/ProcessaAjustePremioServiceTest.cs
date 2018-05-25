using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public class ProcessaAjustePremioServiceTest: EmissaoPremioBaseTest
    {
        private IParcelaAjustada _ajustePremio;

        private Premio _premioDto;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _ajustePremio = ObterContratoAjustepremio();

            GetInstance<AjustePremioService>().Execute(_ajustePremio).Wait();

            _premios = GetInstance<IPremios>();

            _premioDto = await _premios.ObterPorItemCertificado<Premio>(long.Parse(_emissaoPremio.Parcelas.First().ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Ajuste, 12);
        }

        [Test]
        public void CriouParcelaAjustada()
        {
            var premioAjustado = _ajustePremio.Parcelas.First().ParcelaId;

            Assert.That(_premioDto, Is.Not.Null, "Premio não cadastrado.");
            Assert.That(premioAjustado.IdentificadorExternoCobertura, Is.EqualTo(_premioDto.ItemCertificadoApoliceId.ToString()));
            Assert.That(premioAjustado.NumeroParcela, Is.EqualTo(_premioDto.Numero));
        }

        [Test]
        public void CriouParcelaAjusteVigencia()
        {
            var vigencia = _ajustePremio.Parcelas.First().Vigencia;
            Assert.That(vigencia.Competencia, Is.EqualTo(_premioDto.Competencia));
            Assert.That(vigencia.Inicio, Is.EqualTo(_premioDto.InicioVigencia));
            Assert.That(vigencia.Fim, Is.EqualTo(_premioDto.FimVigencia));
        }

        [Test]
        public void CriouParcelaAjusteValores()
        {
            var valores = _ajustePremio.Parcelas.First().Valores;
            Assert.That(valores.Contribuicao, Is.EqualTo(_premioDto.ValorPremio));
            Assert.That(valores.Beneficio, Is.EqualTo(_premioDto.ValorBeneficio));
            Assert.That(valores.Carregamento, Is.EqualTo(_premioDto.ValorCarregamento));
            Assert.That(valores.CapitalSegurado, Is.EqualTo(_premioDto.ValorCapitalSegurado));
        }

        [Test]
        public async Task CriouProvisaoDeUmAjuste()
        {
            var dto = await GetInstance<IProvisoes>().ObterProvisaoCobertura(_ajustePremio.Identificador);

            Assert.NotNull(dto);
        }

        [Test]
        public async Task CompensouEventoAjustePremio()
        {
            await GetInstance<AjustePremioService>().Compensate(_ajustePremio);

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_ajustePremio.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }

        private IParcelaAjustada ObterContratoAjustepremio()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();

            return ParcelaAjustadaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComDataExecucao(_proposta.DataExecucaoEvento)
                .Com(ParcelaBuilder.UmBuilder()
                    .Com(ParcelaIdBuilder.UmBuilder()
                        .ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno)
                        .ComNumeroParcela(IdentificadoresPadrao.NumeroParcela))
                    .Com(ValorBuilder.UmBuilder().Padrao())
                    .Com(VigenciaBuilder.UmBuilder().Padrao()))
                .Build();
        }
    }
}
