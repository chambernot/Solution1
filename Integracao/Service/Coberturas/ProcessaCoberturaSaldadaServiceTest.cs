using Mongeral.Integrador.Contratos;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Domain.Interfaces;
using Mongeral.Provisao.V2.Service.Premio.Eventos;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Implantacao;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Contratos.Premio;
using Mongeral.Provisao.V2.Testes.Infra.Persisters;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Integracao.Service.Coberturas
{
    
    public class ProcessaCoberturaSaldadaServiceTest : ProcessarCoberturaBaseTest
    {
        
        private ICoberturaSaldada _coberturaSaldada;
        long identificador;

        [OneTimeSetUp]
        public new void FixtureSetUp()
        {
            ProcessarEmissao();

            _coberturaSaldada = ObterCoberturaSaldada();
            identificador = Convert.ToInt64(_coberturaSaldada.IdentificadorExternoCobertura);
            GetInstance<CoberturaSaldadaService>().Execute(_coberturaSaldada).Wait();

            ProcessarApropriacao();
        }

        [Test]
        public async Task CriouEmissaoCriouEventoSaldamentoCriaApropriacao_ValoresIguaisProvisao()
        {
            
            
            var provisaoemissao = await GetInstance<ProvisaoCobertura>().ObterMovimentoProvisao(identificador, (short)TipoEventoEnum.EmissaoPremio);

            var provisaoapropriacao = await GetInstance<ProvisaoCobertura>().ObterMovimentoProvisao(identificador, (short)TipoEventoEnum.ApropriacaoPremio);

            Assert.AreEqual(provisaoemissao.ValorProvisao, provisaoapropriacao.ValorProvisao);
            
        }


        [Test]
        public async Task CriouMovimentoProvisoesPorCadaTipoProvisaoCoberturaSaldamento()
        {


            var dto = await GetInstance<ICoberturas>().ObterProvisaoPorCobertura(identificador);

            var provisoes = await GetInstance<ProvisaoCobertura>().ListarProvisoesCobertura(identificador, (short)TipoEventoEnum.Saldamento);

            Assert.That(dto.ProvisaoCobertura.Count, Is.EqualTo(provisoes));
        }


        [Test]
        public async Task CriouHistoricoCoberturaSaldamento()
        {
            

            var dto = await GetInstance<HistoricoCobertura>().ObterHistoricoCobertura(identificador, (short)TipoEventoEnum.Saldamento);


            Assert.IsNotNull(dto);

            Assert.AreEqual(dto.ValorBeneficio, _coberturaSaldada.ValorBeneficio);
            Assert.AreEqual(dto.ValorCapital, _coberturaSaldada.ValorCapitalSegurado);
        }


        private ICoberturaSaldada ObterCoberturaSaldada()
        {
            return CoberturaSaldadaBuilder.UmaCobertura().ComIdentificador(Guid.NewGuid())
                .ComIdentificadorNegocio(_proposta.IdentificadorNegocio)
                .ComDataExecucaoEvento(DateTime.Now).Padrao().ComIdentificadorExternoCobertura(cobertura.IdentificadorExterno).Build();
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
}
