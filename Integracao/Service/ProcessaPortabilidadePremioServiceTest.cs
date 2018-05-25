using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.Infra.Persisters;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{

    public class ProcessaPortabilidadePremioServiceTest : ApropriacaoPremioBaseTeste
    {
        private IPortabilidadeApropriada _portabilidadeApropriado;
        private Premio _premioPortabilidadeDto;
        private PagamentoPremio _pagamentoPremioDto;
        private IPortabilidade _portabilidade;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _portabilidadeApropriado = ObterPortabilidadeApropriada();

            _portabilidade = _portabilidadeApropriado.Portabilidades.First();

            GetInstance<CoberturaContratadaHelper>().AtualizarCoberturaRegimeFinanceiro(
                long.Parse(_portabilidade.ParcelaId.IdentificadorExternoCobertura), TipoRegimeFinanceiroEnum.FundoAcumulacao).Wait();

            GetInstance<PortabilidadePremioService>().Execute(_portabilidadeApropriado).Wait();

            _premioPortabilidadeDto = await _premios.ObterPorItemCertificado<Premio>(long.Parse(_portabilidade.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Portabilidade, 0);

            _pagamentoPremioDto = await _premios.ObterPorItemCertificado<PagamentoPremio>(long.Parse(_portabilidade.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Portabilidade, 0);
        }

        [Test]
        public void CriouDadosPortabilidade()
        {
            Assert.That(_premioPortabilidadeDto.TipoMovimentoId, Is.EqualTo((int)TipoMovimentoEnum.Portabilidade));
            Assert.That(_premioPortabilidadeDto.Numero, Is.EqualTo(0));
            Assert.That(_premioPortabilidadeDto.Competencia, Is.EqualTo(_portabilidade.Vigencia.Competencia));
            Assert.That(_premioPortabilidadeDto.InicioVigencia, Is.EqualTo(_portabilidade.Vigencia.Inicio));
            Assert.That(_premioPortabilidadeDto.FimVigencia, Is.EqualTo(_portabilidade.Vigencia.Fim));
            Assert.That(_premioPortabilidadeDto.ValorPremio, Is.EqualTo(_portabilidade.Valores.Contribuicao));
            Assert.That(_premioPortabilidadeDto.ValorBeneficio, Is.EqualTo(_portabilidade.Valores.Beneficio));
            Assert.That(_premioPortabilidadeDto.CodigoSusep, Is.EqualTo(_portabilidade.CodigoSusep));
        }

        [Test]
        public void CriouDadosPagamento()
        {
            Assert.That(_pagamentoPremioDto.DataPagamento, Is.EqualTo(_portabilidade.Pagamento.DataPagamento));
            Assert.That(_pagamentoPremioDto.DataApropriacao, Is.EqualTo(_portabilidade.Pagamento.DataApropriacao));
            Assert.That(_pagamentoPremioDto.ValorPago, Is.EqualTo(_portabilidade.Pagamento.ValorPago));
            Assert.That(_pagamentoPremioDto.Desconto, Is.EqualTo(_portabilidade.Pagamento.Desconto));
            Assert.That(_pagamentoPremioDto.Multa, Is.EqualTo(_portabilidade.Pagamento.Multa));
            Assert.That(_pagamentoPremioDto.IOFARecolher, Is.EqualTo(_portabilidade.Pagamento.IOFARecolher));
            Assert.That(_pagamentoPremioDto.IOFRetido, Is.EqualTo(_portabilidade.Pagamento.IOFRetido));
            Assert.That(_pagamentoPremioDto.IdentificadorCredito, Is.EqualTo(_portabilidade.Pagamento.IdentificadorCredito));
        }

        [Test]
        public async Task NaoGeraMovimento()
        {
            var dto = await GetInstance<IProvisoes>().ObterProvisaoCobertura(_portabilidadeApropriado.Identificador);

            Assert.IsTrue(!dto.Any());
        }

        [Test]
        public async Task CompensouEventoPortabilidadeApropriacao()
        {
            await GetInstance<PortabilidadePremioService>().Compensate(_portabilidadeApropriado);

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_portabilidadeApropriado.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }

        private IPortabilidadeApropriada ObterPortabilidadeApropriada()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();

            var aporte = PortabilidadeBuilder.UmBuilder()
                .ComCodigoSusep(IdentificadoresPadrao.CodigoSusep)
                .Com(PagamentoBuilder.UmBuilder().Padrao())
                .ComValorBuilder(ValorBuilder.UmBuilder().Padrao())
                .ComVigenciaBuilder(VigenciaBuilder.UmBuilder().Padrao())
                .ComParcelaBuilder(ParcelaIdBuilder.UmBuilder()
                    .ComNumeroParcela(0).ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno));

            return PortabilidadeApropriadaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(DateTime.Now)
                .Com((PortabilidadeBuilder)aporte)
                .Build();
        }
    }
}
