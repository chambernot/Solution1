using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Domain.Extensions;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.VG;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service.VG
{
    public class ProcessarReemissaoPremioVgServiceTest : IntegrationTestBase
    {
        [Test]
        public async Task AoProcessarReemissaoPremioDeveRetornarListaDeProvisoes()
        {
            var faturaParcela = ObterContratoEmissaoPremio();

            var retorno = await GetInstance<ReemissaoPremioVgService>().Execute(faturaParcela);

            var parcela = faturaParcela.Parcelas.First();
            var numeroProvisoes = parcela.Vigencia.Fim.CalcularMeses(parcela.Vigencia.Inicio);

            Assert.That(retorno.Parcelas.First().Provisoes.Count(), Is.EqualTo(numeroProvisoes));
        }

        public IParcelaFaturaReemitida ObterContratoEmissaoPremio()
        {
            var parcela = ParcelaFaturaReemitidaBuilder.UmBuilder()
                .ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio)
                .ComDataExecucaoEvento(IdentificadoresPadrao.Competencia)
                .Com(ParcelaBuilder.UmBuilder()
                    .Com(ParcelaIdBuilder.UmBuilder()
                        .ComIdentificadorExternoCobertura(IdentificadoresPadrao.ItemCertificadoApoliceId.ToString())
                        .ComNumeroParcela(IdentificadoresPadrao.NumeroParcela))
                    .Com(ValorBuilder.UmBuilder().Padrao())
                    .Com(VigenciaBuilder.UmBuilder().Padrao()))
                .Build();

            return parcela;
        }
    }
}
