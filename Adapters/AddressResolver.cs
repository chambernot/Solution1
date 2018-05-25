using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using Mongeral.Infrastructure.Assertions;

namespace Mongeral.Provisao.V2.Adapters
{
    public class AddressResolver<T> : IAddressResolver
    {
        public (Binding binding, EndpointAddress address) Binding { get { return GetBinding(typeof(T)); } }

        private (Binding binding, EndpointAddress address)  GetBinding(Type type)
        {
            var clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            var client = clientSection?.Endpoints.Cast<ChannelEndpointElement>()
                .FirstOrDefault(x => x.Contract == type.FullName);

            Assertion.NotNull(client,$"Não foir encontrada a configiguração para o serviço {type.FullName}").Validate();

            var binding = client.Binding == "basicHttpBinding"
                ? (Binding) new BasicHttpBinding(client.BindingConfiguration)
                : (Binding) new WSHttpBinding(client.BindingConfiguration);

            var adress = new EndpointAddress(client.Address);

            return (binding, adress);
        }

    }
}