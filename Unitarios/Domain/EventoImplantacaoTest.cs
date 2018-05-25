using System;
using Mongeral.Provisao.V2.Domain;
using NUnit.Framework;

namespace Mongeral.Provisao.V2.Testes.Unitarios.Domain
{
    [TestFixture]
    public class EventoImplantacaoTest
    {
        private EventoImplantacao _evento;
        private Guid _identificador = Guid.NewGuid();

        [Test]
        public void Ao_Criar_Evento_o_identificador_deve_ser_preenchido()
        {
            _evento = new EventoImplantacao(_identificador,"idCor","idNeg",DateTime.Now);

            Assert.That(_evento.Identificador, Is.EqualTo(_identificador));
            Assert.That(_evento.IdentificadorNegocio, Is.EqualTo("idNeg"));
            Assert.That(_evento.IdentificadorCorrelacao, Is.EqualTo("idCor"));
        }
    }    
}
