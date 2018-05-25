using System;
using System.Linq;
using System.Threading.Tasks;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service
{
    public class ProcessaDesapropriacaoPremioServiceTest: ApropriacaoPremioBaseTeste
    {
        private IParcelaDesapropriada _desapropriacaoPremio;
        private PagamentoPremio _premioDto;
        private IApropriacao _apropriacao;

        [OneTimeSetUp]
        public new async Task FixtureSetUp()
        {
            _desapropriacaoPremio = ObterParcelaDesapropriada();

            GetInstance<DesapropriacaoPremioService>().Execute(_desapropriacaoPremio).Wait();

            _premios = GetInstance<IPremios>();

            _apropriacao = _desapropriacaoPremio.Parcelas.First();

            _premioDto = await _premios.ObterPorItemCertificado<PagamentoPremio>(long.Parse(_apropriacao.ParcelaId.IdentificadorExternoCobertura), (short)TipoMovimentoEnum.Desapropriacao, 12);
        }

        [Test]
        public void CriouDesaproriacaoDataPagamento()
        {
            Assert.That(_premioDto.DataPagamento, Is.EqualTo(_apropriacao.Pagamento.DataPagamento));
        }

        [Test]
        public void CriouDesaproriacaoDataApropriacao()
        {
            Assert.That(_premioDto.DataApropriacao, Is.EqualTo(_apropriacao.Pagamento.DataApropriacao));
        }

        [Test]
        public void CriouDesaproriacaoValorPago()
        {
            Assert.That(_premioDto.ValorPago, Is.EqualTo(_apropriacao.Pagamento.ValorPago));
        }

        [Test]
        public void CriouDesaproriacaoDesconto()
        {
            Assert.That(_premioDto.Desconto, Is.EqualTo(_apropriacao.Pagamento.Desconto));
        }

        [Test]
        public void CriouDesaproriacaoMulta()
        {
            Assert.That(_premioDto.Multa, Is.EqualTo(_apropriacao.Pagamento.Multa));
        }

        [Test]
        public void CriouDesaproriacaoIOFARecolher()
        {
            Assert.That(_premioDto.IOFARecolher, Is.EqualTo(_apropriacao.Pagamento.IOFARecolher));
        }

        [Test]
        public void CriouDesaproriacaoIOFRetido()
        {
            Assert.That(_premioDto.IOFRetido, Is.EqualTo(_apropriacao.Pagamento.IOFRetido));
        }

        [Test]
        public void CriouDesaproriacaoIdentidficadorCredito()
        {
            Assert.That(_premioDto.IdentificadorCredito, Is.EqualTo(_apropriacao.Pagamento.IdentificadorCredito));
        }

        [Test]
        public async Task CriouProvisaoDeUmaDesapropriacao()
        {
            var dto = await GetInstance<IProvisoes>().ObterProvisaoCobertura(_apropriacaoPremio.Identificador);

            Assert.NotNull(dto);
        }

        [Test]
        public async Task CompensouEventoDesapropriacaoPremio()
        {
            await GetInstance<DesapropriacaoPremioService>().Compensate(_desapropriacaoPremio);

            var existeEvento = await GetInstance<IEventosBase<EventoPremio>>().ExisteEvento(_desapropriacaoPremio.Identificador);

            Assert.That(existeEvento, Is.EqualTo(false));
        }
        
        private IParcelaDesapropriada ObterParcelaDesapropriada()
        {
            var cobertura = _proposta.Produtos.First().Coberturas.First();
            
            var apropriacao = ApropriacaoBuilder.UmBuilder()
                .Com(PagamentoBuilder.UmBuilder().Padrao())
                .ComValorBuilder(ValorBuilder.UmBuilder().Padrao())
                .ComVigenciaBuilder(VigenciaBuilder.UmBuilder().Padrao())
                .ComParcelaBuilder(ParcelaIdBuilder.UmBuilder()
                    .ComNumeroParcela(12).ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno));

            return ParcelaDesapropriadaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(DateTime.Now)
                .Com((ApropriacaoBuilder)apropriacao)
                .Build();
        }
    }
}
