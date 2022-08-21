using DivinitySoftworks.Apps.Core.Configuration.Managers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DivinitySoftworks.Apps.TravelExpenses.Core.Handlers {

    public class ConfigurationItem : IRequest<object> {
        public ConfigurationItem(string name, object? value) {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object? Value { get; }
    }

    internal class ConfigurationHandler : IRequestHandler<ConfigurationItem, object> {
        readonly IConfigurationManager _configurationManager;

        public ConfigurationHandler(IConfigurationManager configurationManager) {
            _configurationManager = configurationManager;
        }

        public async Task<object> Handle(ConfigurationItem request, CancellationToken cancellationToken) {
            try {
                await _configurationManager.SetUserSettingAsync(request.Name, request.Value);

                return new object();
            }
            catch (Exception exception) {
                throw exception;
            }
        }
    }
}
