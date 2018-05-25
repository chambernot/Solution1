using Mongeral.Provisao.V2.Testes.Infra.Builders;
using Moq;

namespace Mongeral.Provisao.V2.Testes.Infra.Builders
{
    public abstract class BaseBuilder<T> : IBuilder<T> where T : class
    {
        protected T Instance;

        public virtual T Build()
        {
            return Instance;
        }
    }
    public class MockBuilder<T> where T : class
    {
        protected Mock<T> Mock;

        protected MockBuilder() {
            Mock = new Mock<T>();
            Mock.SetupAllProperties();
        }

        public T Build()
        {
            return Mock.Object;
        }
    }

}
