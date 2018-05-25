using System;
using System.CodeDom;
using System.ServiceModel;
using Mongeral.Infrastructure.Assertions;

namespace Mongeral.Provisao.V2.Adapters
{
    public static class WcfClient<T> where T : class
    {
        private static ChannelFactory<T> _channelFactory;
        private static IAddressResolver _addressResolver;
        private static ChannelFactory<T> ChannelFactory => _channelFactory ?? (_channelFactory = new ChannelFactory<T>(AddressResolver.Binding.binding, AddressResolver.Binding.address));
        private static IAddressResolver AddressResolver => _addressResolver ?? (_addressResolver = new AddressResolver<T>());

        public static void CallUsing(Action<T> action)
        {
            var client = (IClientChannel)ChannelFactory.CreateChannel();

            try
            {
                action((T)client);
                client.Close();
            }
            catch (CommunicationException)
            {
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch (Exception)
            {
                client.Abort();
                throw;
            }
        }

        public static TReturn CallUsing<TReturn>(Func<T, TReturn> func)
        {
            var client = (IClientChannel)ChannelFactory.CreateChannel();
            TReturn resultado;

            try
            {
                resultado = func((T)client);
            }
            finally
            {
                try
                {
                    client.Close();
                }
                catch (CommunicationException)
                {
                    client.Abort();
                }
                catch (TimeoutException)
                {
                    client.Abort();
                }
                catch (Exception)
                {
                    client.Abort();
                    throw;
                }
            }

            return resultado;
        }
    }
}