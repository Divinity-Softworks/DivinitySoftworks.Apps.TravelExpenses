using DivinitySoftworks.Apps.Core.Configuration.Managers;
using DivinitySoftworks.Apps.TravelExpenses.Services;
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
        readonly ILogService _logService;

        public ConfigurationHandler(IConfigurationManager configurationManager, ILogService logService) {
            _configurationManager = configurationManager;
            _logService = logService;
        }

        public async Task<object> Handle(ConfigurationItem request, CancellationToken cancellationToken) {
            try {
                await _configurationManager.SetUserSettingAsync(request.Name, request.Value);
            }
            catch (Exception exception) {
                _logService.LogException(exception, "An error occurred while trying to save the configuration.");
            }
            return new object();
        }
    }
}
