using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Mongeral.Provisao.V2.Adapters
{
    public interface IAddressResolver
    {
        (Binding binding, EndpointAddress address) Binding { get; }
    }
}