using Mongeral.Provisao.V2.Domain.Enum;
using Mongeral.Provisao.V2.Testes.Infra.Builders.Entitdades;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongeral.Provisao.V2.Service.Premio.Factory;
using Mongeral.Provisao.V2.Service.Premio.Validacao;
using Ninject;
using Mongeral.Provisao.V2.Domain.Extensions;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain
{
    [TestFixture]
    public class PremioTest: UnitTesBase
    {
        [Test]
        public void PremioAnteriorInvalido()
        {
            var premioAtual = PremioBuilder.Um().Padrao().ComTipoMovimento((short)TipoMovimentoEnum.Desapropriacao).Build();
            premioAtual.InformaEvento(EventoDesParcelaApropriadaBuilder.UmBuilder().Padrao().Build());

            var premioAnterior = PremioBuilder.Um().Padrao().ComTipoMovimento((short)TipoMovimentoEnum.Emissao).Build();
            
            Assert.That(() => premioAtual.ValidaPremioAnterior(premioAnterior), GeraErro($"Impossivel validar movimento de {TipoMovimentoEnum.Desapropriacao} precedido de {TipoMovimentoEnum.Emissao}"));
        }
    }
}
