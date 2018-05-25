using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Factories;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Provisao;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Mongeral.Provisao.V2.DTO;
using Mongeral.Provisao.V2.Testes.Infra.Builders;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain.Factories
{
    [TestFixture]
    public class CalculadorProvisaoPremioTest: UnitTesBase
    {
        private CalculadorProvisaoPremio _calculadorProvisao;
        private Mock<CalculadorProvisaoPremioNaoGanhoPremio> _calculadorPPNG;
        private Mock<CalculadorProvisaoMatematicaBeneficioAConceder> _calculadorPMBAC;
        private Mock<IProvisoesService> _provisao;
        private Mock<ICalculoFacade> _facade;
        private ProvisaoMatematicaBeneficioAConceder _resultado;        

        private EventoEmissaoPremio _evento;
        private Premio _premio;
        private ProvisaoDto _movimentoProvisaoDto;
        private MovimentoProvisao _movimentoProvisao;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            _movimentoProvisaoDto = ProvisaoDtoBuilder.UmBuilder().Padrao().Build();
            _movimentoProvisao = MovimentoProvisaoBuilder.UmBuilder().Padrao().Build();

            _calculadorPMBAC = new Mock<CalculadorProvisaoMatematicaBeneficioAConceder>(_facade);
            _calculadorPPNG = new Mock<CalculadorProvisaoPremioNaoGanhoPremio>();            
            _provisao = new Mock<IProvisoesService>();
        }

        [Test]
        public void DadoUmPremioCapitalizadoDeveCalcularPMBAC()
        {
            CriaEventoEmissaoPremio(TipoRegimeFinanceiroEnum.Capitalizacao, TipoProvisaoEnum.PMBAC);

            _premio = _evento.Premios.First();
            _premio.InformaEvento(_evento);
            _resultado = PmbacBuilder.UmBuilder().Padrao().Build();            

            _facade = new Mock<ICalculoFacade>();
            _facade.Setup(x => x.CalcularPMBAC(It.IsAny<CoberturaContratada>(), It.IsAny<DateTime>(), It.IsAny<decimal>())).Returns(_resultado);
            
            _provisao.Setup(x => x.ObterUltimoMovimentoProvisao(It.IsAny<Premio>(), TipoProvisaoEnum.PMBAC)).Returns(Task.FromResult(_movimentoProvisao));
            
            _calculadorPMBAC.Setup(x => x.CalculaProvisao(It.IsAny<Premio>(), It.IsAny<decimal>())).Returns(new List<ProvisaoDto>() { _resultado });
            
            _calculadorProvisao = new CalculadorProvisaoPremio(_calculadorPPNG.Object, _calculadorPMBAC.Object, _provisao.Object);

            _calculadorProvisao.CriarProvisao(_premio);

            _calculadorPMBAC.Verify(m => m.CalculaProvisao(It.IsAny<Premio>(), It.IsAny<decimal>()));
        }

        [Test]
        public void DadoUmPremioReparticaoSimplesDeveCalcularPPNG()
        {
            CriaEventoEmissaoPremio(TipoRegimeFinanceiroEnum.ReparticaoSimples, TipoProvisaoEnum.PPNG, TipoProvisaoEnum.PIC, TipoProvisaoEnum.POR);

            _premio = _evento.Premios.First();
            _premio.InformaEvento(_evento);
            _calculadorPPNG.Setup(x => x.CalcularProvisao(It.IsAny<Premio>()))
                .Returns(new List<ProvisaoDto>() { _movimentoProvisaoDto });

            _calculadorProvisao = new CalculadorProvisaoPremio(_calculadorPPNG.Object, _calculadorPMBAC.Object, _provisao.Object);

            _calculadorProvisao.CriarProvisao(_premio);

            _calculadorPPNG.Verify(m => m.CalcularProvisao(It.IsAny<Premio>()));
        }      

        private void CriaEventoEmissaoPremio(TipoRegimeFinanceiroEnum regimeFinanceiro, params TipoProvisaoEnum[] tiposProvisao)
        {
            _evento = EventoEmissaoPremioBuilder.UmEvento()
                .Padrao()
                .Com(PremioBuilder.Um()
                    .Com(CoberturaContratadaBuilder.Uma()
                        .ComRegimeFinanceiro((short)regimeFinanceiro)
                        .ComTiposProvisao(tiposProvisao)))
                .Build();
        }
    }
}
