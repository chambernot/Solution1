using Mongeral.Infrastructure.UnitOfWork.Session;
using System;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Testes.Infra.Fake
{
    public class TransactionalMethodFake : ITransactionalMethod
    {
        public void Execute(Action method)
        {
            method();
        }

        public T Execute<T>(Func<T> method)
        {
            return method();
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> method)
        {
            return await method();
        }

        public async Task ExecuteAsync(Func<Task> method)
        {
            await method();
        }
    }
}
