using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.Infra.Persisters;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public class ProcessaAportePremioServiceTest: ApropriacaoPremioBaseTeste
    {
        private IAporteApropriado _aporteApropriado;
        private Premio _premioAporteDto;
        private PagamentoPremio _pagamentoPremioDto;
        private IAporte _aporte;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _aporteApropriado = ObterAportePremio();

            _aporte = _aporteApropriado.Aportes.First();

            GetInstance<CoberturaContratadaHelper>().AtualizarCoberturaRegimeFinanceiro(
                long.Parse(_aporte.ParcelaId.IdentificadorExternoCobertura), TipoRegimeFinanceiroEnum.FundoAcumulacao).Wait();

            GetInstance<AportePremioService>().Execute(_aporteApropriado).Wait();

            _premioAporteDto = await _premios.ObterPorItemCertificado<Premio>(long.Parse(_aporte.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Aporte, 0);

            _pagamentoPremioDto = await _premios.ObterPorItemCertificado<PagamentoPremio>(long.Parse(_aporte.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Aporte, 0);
        }

        [Test]
        public void CriouDadosAporte()
        {            
            Assert.That(_premioAporteDto.Numero, Is.EqualTo(0));
            Assert.That(_premioAporteDto.Competencia, Is.EqualTo(_aporte.Vigencia.Competencia));
            Assert.That(_premioAporteDto.InicioVigencia, Is.EqualTo(_aporte.Vigencia.Inicio));
            Assert.That(_premioAporteDto.FimVigencia, Is.EqualTo(_aporte.Vigencia.Fim));
            Assert.That(_premioAporteDto.ValorPremio, Is.EqualTo(_aporte.Valores.Contribuicao));
            Assert.That(_premioAporteDto.ValorBeneficio, Is.EqualTo(_aporte.Valores.Beneficio));
        }

        [Test]
        public void CriouDadosPagamento()
        {
            Assert.That(_pagamentoPremioDto.DataPagamento, Is.EqualTo(_aporte.Pagamento.DataPagamento));
            Assert.That(_pagamentoPremioDto.DataApropriacao, Is.EqualTo(_aporte.Pagamento.DataApropriacao));
            Assert.That(_pagamentoPremioDto.ValorPago, Is.EqualTo(_aporte.Pagamento.ValorPago));
            Assert.That(_pagamentoPremioDto.Desconto, Is.EqualTo(_aporte.Pagamento.Desconto));
            Assert.That(_pagamentoPremioDto.Multa, Is.EqualTo(_aporte.Pagamento.Multa));
            Assert.That(_pagamentoPremioDto.IOFARecolher, Is.EqualTo(_aporte.Pagamento.IOFARecolher));
            Assert.That(_pagamentoPremioDto.IOFRetido, Is.EqualTo(_aporte.Pagamento.IOFRetido));
            Assert.That(_pagamentoPremioDto.IdentificadorCredito, Is.EqualTo(_aporte.Pagamento.IdentificadorCredito));
        }

        [Test]
        public async Task NaoGeraMovimento()
        {
            var dto = await GetInstance<IProvisoes>().ObterProvisaoCobertura(_aporteApropriado.Identificador);

            Assert.IsTrue(!dto.Any());
        }

        [Test]
        public async Task CompensouEventoAporteApropriacao()
        {
            await GetInstance<AportePremioService>().Compensate(_aporteApropriado);

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_aporteApropriado.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }

        private IAporteApropriado ObterAportePremio()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();

            var aporte = AporteBuilder.UmBuilder()
                .Com(PagamentoBuilder.UmBuilder().Padrao())
                .ComValorBuilder(ValorBuilder.UmBuilder().Padrao())
                .ComVigenciaBuilder(VigenciaBuilder.UmBuilder().Padrao())
                .ComParcelaBuilder(ParcelaIdBuilder.UmBuilder()
                    .ComNumeroParcela(0).ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno));

            return AporteApropriadoBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(DateTime.Now)
                .Com((AporteBuilder)aporte)
                .Build();
        }
    }
}
