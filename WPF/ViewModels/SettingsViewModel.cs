using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using WPF.State.Context;

namespace WPF.ViewModels
{
    public class SettingsViewModel :ViewModelBase
    {


		private bool enableDebuggingMode=true;
		public bool EnableDebuggingMode
		{
			get
			{
				return enableDebuggingMode;
			}
			set
			{
				enableDebuggingMode = value;
				OnPropertyChanged(nameof(EnableDebuggingMode));
                _settingsContexts.ContextHolder.EnableDebugging = enableDebuggingMode;
                _settingsContexts.RaiseEvent();
            }
		}

		private bool workWithoutConnection;
		public bool WorkWithoutConnection
		{
			get
			{
				return workWithoutConnection;
			}
			set
			{
				workWithoutConnection = value;
				OnPropertyChanged(nameof(WorkWithoutConnection));
                _settingsContexts.ContextHolder.WorkWithoutDBConnection = workWithoutConnection;
				//_logger.LogInformation($"WorkWithoutConnection{workWithoutConnection}");
                _settingsContexts.RaiseEvent();
			}
		}

		private readonly IApplicationContext<Settings> _settingsContexts;

		private readonly ILogger<SettingsViewModel> _logger;	
        public SettingsViewModel(IApplicationContext<Settings> settingsContext/*, ILogger<SettingsViewModel> logger*/)
        {
           
            _settingsContexts = settingsContext;
            //_logger= logger;


        }
    }
}
