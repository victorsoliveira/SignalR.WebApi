using System;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.WindowsAzure.ServiceRuntime;
using Owin;

[assembly: OwinStartup(typeof(SignalR.WebApi.Startup))]
namespace SignalR.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (GetConfiguration("UseBackplane") == "true" && !String.IsNullOrEmpty(GetConfiguration("Microsoft.WindowsAzure.ServiceBus.ConnectionString")))
            {
                var scaleoutConfig = new ServiceBusScaleoutConfiguration(GetConfiguration("Microsoft.WindowsAzure.ServiceBus.ConnectionString"), "sharedtodo")
                {
                    TopicCount = 1,
                };
                GlobalHost.DependencyResolver.UseServiceBus(scaleoutConfig);
            }
            app.MapSignalR();
        }

        private string GetConfiguration(string configurationName, bool throwIfNotExists = false)
        {
            bool inAzure;

            try
            {
                inAzure = RoleEnvironment.IsAvailable;
            }
            catch (Exception)
            {
                inAzure = false;
            }

            if (inAzure)
            {
                var value = RoleEnvironment.GetConfigurationSettingValue(configurationName);

                if (throwIfNotExists == false && string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("Configuração '" + configurationName + "' não foi definida.");
                }

                return value;
            }
            else
            {
                var value = ConfigurationManager.AppSettings.Get(configurationName);

                if (throwIfNotExists == false && string.IsNullOrEmpty(value))
                {
                    throw new ConfigurationErrorsException("Configuração '" + configurationName + "' não foi definida.");
                }

                return value;
            }
        }
    }
}
