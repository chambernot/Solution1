using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;
using System;
using System.Linq;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Service.Premio.Transformacao;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Service.Extensions
{
    [TestFixture]
    public class EmissaoPremioExtensionTest : UnitTesBase
    {
        private IParcelaEmitida _emissaoPremio;
        private EventoEmissaoPremio _evento;
        

        [Test]
        public void DadoUmPremioComIdentificadorInvalidoDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder().ComIdentificador(default(Guid)).Build();
            
            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("O Identificador não pode ser nulo."));
        }

        [Test]
        public void DadoUmPremioComDataExecucaoInvalidaDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder().ComDataExecucaoEvento(DateTime.MinValue).Build();

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("A Data de Execução não pode ser nula."));
        }

        [Test]
        public void DadoUmPremioComItemCertificadoApoliceInvalidoDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                    .ComIdentificador(IdentificadoresPadrao.Identificador)
                    .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)                        
                    .Com(ParcelaBuilder.UmBuilder()
                        .Com(ParcelaIdBuilder.UmBuilder().ComIdentificadorExternoCobertura(string.Empty))
                        .Com(ValorBuilder.UmBuilder().Padrao())
                        ).Build();

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("O ItemCerficadoApolice não foi informado"));
        }

        [Test]
        public void DadoUmPremioComNumeroParcelaInvalidoDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                        .ComIdentificador(IdentificadoresPadrao.Identificador)
                        .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)
                        .Com(ParcelaBuilder.UmBuilder()
                            .Com(VigenciaBuilder.UmBuilder().Padrao())
                            .Com(ValorBuilder.UmBuilder().Padrao())
                            .Com(ParcelaIdBuilder.UmBuilder()
                                .ComNumeroParcela(int.MinValue)))
                        .Build();
            
            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("O número da Parcela não foi informado."));
        }

        [Test]
        public void DadoUmPremioSemValoresDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                        .ComIdentificador(IdentificadoresPadrao.Identificador)
                        .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)
                        .Com(ParcelaBuilder.UmBuilder()
                            .Com(ValorBuilder.UmBuilder())
                            .Com(VigenciaBuilder.UmBuilder().Padrao())
                            .Com(ParcelaIdBuilder.UmBuilder()
                                .ComNumeroParcela(int.MinValue)))
                        .Build();

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("O valor de contribuição não foi informado."));
            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("O valor de Benfício não foi informado."));           
        }

        [Test]
        public void DadoUmPremioComDataCompetenciaMenorQueADataDeInicioDaVigenciaDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                .ComIdentificador(IdentificadoresPadrao.Identificador)
                .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)
                .Com(ParcelaBuilder.UmBuilder()
                    .Com(ParcelaIdBuilder.UmBuilder())
                    .Com(ValorBuilder.UmBuilder().Padrao())
                    .Com(VigenciaBuilder.UmBuilder()
                        .ComCompetencia(new DateTime(2017, 03, 01))
                        .ComDataInicio(new DateTime(2017, 06, 01))
                        .ComDataFim(new DateTime(2017, 12, 01))))
                .Build();            

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("A data de competência inválida, a data não pode estar fora do período de Vigência."));
        }

        [Test]
        public void DadoUmPremioComDataCompetenciaMaiorQueADataDeFimDaVigenciaDeveSerGeradoUmErro()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                .ComIdentificador(IdentificadoresPadrao.Identificador)
                .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)
                .Com(ParcelaBuilder.UmBuilder()
                    .Com(ParcelaIdBuilder.UmBuilder())
                    .Com(ValorBuilder.UmBuilder().Padrao())
                    .Com(VigenciaBuilder.UmBuilder()
                        .ComCompetencia(new DateTime(2017, 12, 01))
                        .ComDataInicio(new DateTime(2017, 06, 01))
                        .ComDataFim(new DateTime(2017, 10, 01))))
                .Build();            

            Assert.That(() => GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio), GeraErro("A data de competência inválida, a data não pode estar fora do período de Vigência."));
        }

        [Test]
        public void DadoUmaEmissaoPremioOMesmoDeveSerMapeado()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder().Padrao().Build();

            var premio = GetInstance<EventoPremioFactory>().Fabricar(_emissaoPremio).Premios.First();
                        
            Assert.That(IdentificadoresPadrao.Competencia, Is.EqualTo(premio.Competencia));
            Assert.That(IdentificadoresPadrao.DataInicioVigencia, Is.EqualTo(premio.InicioVigencia));
            Assert.That(IdentificadoresPadrao.DataFimVigencia, Is.EqualTo(premio.FimVigencia));
            Assert.That(IdentificadoresPadrao.ValorContribuicao, Is.EqualTo(premio.ValorPremio));
            Assert.That(IdentificadoresPadrao.ValorCarregamento, Is.EqualTo(premio.ValorCarregamento));
            Assert.That(IdentificadoresPadrao.ValorBeneficio, Is.EqualTo(premio.ValorBeneficio));
            Assert.That(IdentificadoresPadrao.ValorCapitalSegurado, Is.EqualTo(premio.ValorCapitalSegurado));                        
        }

        [Test]
        public void DadoUmaEmissaoPremioDeveAdicionarListaProvisoes()
        {
            _emissaoPremio = ParcelaEmitidaBuilder.UmBuilder()
                .ComIdentificador(IdentificadoresPadrao.Identificador)
                .ComIdentificadorNegocio(IdentificadoresPadrao.IdentificadorNegocio)
                .ComDataExecucaoEvento(IdentificadoresPadrao.DataExecucaoEvento)
                .Com(ParcelaBuilder.UmBuilder().Padrao())
                .Build();

            MovimentoProvisaoBuilder[] provisoes = { MovimentoProvisaoBuilder.UmBuilder().Padrao(), MovimentoProvisaoBuilder.UmBuilder().Padrao() };
                        
            _evento = EventoParcelaEmitidaBuilder.UmEvento()
                .Com(PremioBuilder.Um().Padrao()
                    .Com(provisoes)).Build();

            _emissaoPremio.Parcelas.ForEach(p => p.MovimentoToProvisao(_evento));

            var parcela = _emissaoPremio.Parcelas.First();

            Assert.That(provisoes.Count(), Is.EqualTo(_evento.Premios.First().MovimentosProvisao.Count()));
            Assert.That(provisoes.Count(), Is.EqualTo(parcela.Provisoes.Count()));
        }
    }   
}
