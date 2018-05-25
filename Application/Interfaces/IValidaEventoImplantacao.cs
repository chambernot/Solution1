using System.Threading.Tasks;
using Mongeral.Provisao.V2.Domain;

namespace Mongeral.Provisao.V2.Application.Interfaces
{
    public interface IValidaEventoImplantacao
    {
        Task Validar(CoberturaContratada cobertura);
    }
}