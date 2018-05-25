using Mongeral.Integrador.Contratos;
using Rhino.Mocks;
using System;

namespace Mongeral.Provisao.V2.Testes.TestHelpers.Builder.Contrato
{
    public abstract class EventoBaseBuilder<T> : BaseBuilder<T> where T : class, IEvento
    {
        protected EventoBaseBuilder()
        {
            Instance = MockRepository.GenerateMock<T>();
        }

        public EventoBaseBuilder<T> ComIdentificador(Guid identificador)
        {
            Instance.Expect(x => x.Identificador).Return(identificador);
            return this;
        }

        public EventoBaseBuilder<T> ComIdentificadorCorrelacao(string identificador)
        {
            Instance.Expect(x => x.IdentificadorCorrelacao).Return(identificador);
            return this;
        }

        public EventoBaseBuilder<T> ComIdentificadorNegocio(string identificador)
        {
            Instance.Expect(x => x.IdentificadorNegocio).Return(identificador);
            return this;
        }

        public EventoBaseBuilder<T> ComDataExecucao(DateTime data)
        {
            Instance.Expect(x => x.DataExecucaoEvento).Return(data);
            return this;
        }
    }
}
