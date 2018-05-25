using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.Domain.Interfaces
{
    public interface IPremioService
    {   
        Task<Premio> CriarPremio(Premio premio, EventoPremio evento);
    }
}
