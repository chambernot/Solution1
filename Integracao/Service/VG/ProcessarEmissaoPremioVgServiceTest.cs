using Mongeral.Integrador.Contratos.VG.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using NUnit.Framework;
using System;
using Mongeral.Provisao.V2.Domain.Extensions;
using System.Linq;
using System.Threading.Tasks;
using ParcelaFaturaEmitidaBuilder = Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.VG.ParcelaFaturaEmitidaBuilder;
using Mongeral.Provisao.V2.Service.Premio.Eventos;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service.VG
{
    public class ProcessarEmissaoPremioVgServiceTest: IntegrationTestBase
    {
        [Test]
        public async Task AoProcessarEmissaoPremioDeveRetornarListaDeProvisoes()
        {
            var faturaParcela = ObterContratoEmissaoPremio();
            
            var retorno = await GetInstance<EmissaoPremioVgService>().Execute(faturaParcela);

            var parcela = faturaParcela.Parcelas.First();
            var numeroProvisoes = parcela.Vigencia.Fim.CalcularMeses(parcela.Vigencia.Inicio);

            Assert.That(retorno.Parcelas.First().Provisoes.Count(), Is.EqualTo(numeroProvisoes));
        }

        public IParcelaFaturaEmitida ObterContratoEmissaoPremio()
        {
            var parcela = ParcelaFaturaEmitidaBuilder.UmBuilder()
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
