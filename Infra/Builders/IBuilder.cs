namespace Mongeral.Provisao.V2.Testes.Infra.Builders
{
    public interface IBuilder<T> where T : class
    {
        T Build();
    }
}