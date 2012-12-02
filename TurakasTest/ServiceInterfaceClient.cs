using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurakasServiceLibrary;
using System.ServiceModel;
using System.Diagnostics;

namespace TurakasTest
{
    /// <summary>
    /// Service client. 
    /// </summary>
    public class ServiceInterfaceClient
    {
        // Callback handler.
        private IServiceCallback callback = null;

        // Connection for this client.
        private IServiceInterface connection = null;

        // Create new client instace.
        public ServiceInterfaceClient(
                    IServiceCallback callback)
        {
            this.callback = callback;
        }

        // Connect to the service
        public void connect()
        {
            var binding = new NetTcpBinding();
            var endpoint = new EndpointAddress("net.tcp://localhost:9000/IGameService");
            var channelFactory = new DuplexChannelFactory<IServiceInterface>(callback, binding, endpoint);
            try
            {
                connection = channelFactory.CreateChannel();
                getCommObj().Open();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception on connecting to service: " + ex);
            }
        }

        /// <summary>
        /// Close the connection in proper way (unsbscribe etc)
        /// </summary>
        public void disconnect()
        {
            if (connection != null)
            {
                getCommObj().Close();
            }            
        }

        /// <summary>
        /// Get service interface for this client.
        /// </summary>
        /// <returns></returns>
        public IServiceInterface getServiceInterface()
        {
            if (connection == null)
            {
                throw new FaultException("Not connected", new FaultCode("NO-CONNECTION"));
            }
            if (getCommObj().State != CommunicationState.Opened)
            {
                throw new FaultException("Connection closed!", new FaultCode("NO-CONNECTION"));
            }
            return connection;
        }

        /// <summary>
        /// Subscribe  to service. 
        /// </summary>
        /// <param name="nickname">Nickname for subscription.</param>
        /// <exception cref="FaultException">Throws a fault exception on errors. </exception>
        public long Subscribe(string nickname)
        {
            return getServiceInterface().Subscribe(nickname);
        }

        /// <summary>
        /// Cast connection to communication object. 
        /// </summary>
        /// <returns>Communication object for the client</returns>
        private ICommunicationObject getCommObj()
        {
            return (ICommunicationObject)connection;
        }
    }
}
