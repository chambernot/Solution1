using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.DTO;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public abstract class ApropriacaoPremioBaseTeste: EmissaoPremioBaseTest
    {
        protected IParcelaApropriada _apropriacaoPremio;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            _apropriacaoPremio = ObterContratoParcelaApropriada();

            GetInstance<ApropriacaoPremioService>().Execute(_apropriacaoPremio).Wait();
        }

        private IParcelaApropriada ObterContratoParcelaApropriada()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();

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

    public class ProcessaApropriacaoPremioServiceTest: ApropriacaoPremioBaseTeste
    {
        private PagamentoPremio _premioApropriadoDto;
        private IApropriacao _apropriacao;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _apropriacao = _apropriacaoPremio.Parcelas.First();
            _premioApropriadoDto = await _premios.ObterPorItemCertificado<PagamentoPremio>(long.Parse(_apropriacao.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Apropriacao, 12);
        }

        [Test]
        public void CriouDataPagamento()
        {
            Assert.That(_premioApropriadoDto.DataPagamento, Is.EqualTo(_apropriacao.Pagamento.DataPagamento));
        }

        [Test]
        public void CriouDataApropriacao()
        {
            Assert.That(_premioApropriadoDto.DataApropriacao, Is.EqualTo(_apropriacao.Pagamento.DataApropriacao));
        }

        [Test]
        public void CriouValorPago()
        {
            Assert.That(_premioApropriadoDto.ValorPago, Is.EqualTo(_apropriacao.Pagamento.ValorPago));
        }

        [Test]
        public void CriouDesconto()
        {
            Assert.That(_premioApropriadoDto.Desconto, Is.EqualTo(_apropriacao.Pagamento.Desconto));
        }

        [Test]
        public void CriouMulta()
        {
            Assert.That(_premioApropriadoDto.Multa, Is.EqualTo(_apropriacao.Pagamento.Multa));
        }

        [Test]
        public void CriouIOFARecolher()
        {
            Assert.That(_premioApropriadoDto.IOFARecolher, Is.EqualTo(_apropriacao.Pagamento.IOFARecolher));
        }

        [Test]
        public void CriouIOFRetido()
        {
            Assert.That(_premioApropriadoDto.IOFRetido, Is.EqualTo(_apropriacao.Pagamento.IOFRetido));
        }

        [Test]
        public void CriouIdentificadorCredito()
        {
            Assert.That(_premioApropriadoDto.IdentificadorCredito, Is.EqualTo(_apropriacao.Pagamento.IdentificadorCredito));
        }

        [Test]
        public async Task CriouProvisaoDeUmaApropriacao()
        {
            var dto = await GetInstance<IProvisoes>().ObterProvisaoCobertura(_apropriacaoPremio.Identificador);

            Assert.NotNull(dto);
        }

        [Test]
        public async Task CompensouEventoApropriacaoPremio()
        {   
            await GetInstance<ApropriacaoPremioService>().Compensate(_apropriacaoPremio);

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_apropriacaoPremio.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }
    }
}
