using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WPF.State.Context
{
    public class Settings
    {
        public Settings(bool workWithoutDBConnection, bool enableDebugging)
        {
            WorkWithoutDBConnection = workWithoutDBConnection;
            EnableDebugging = enableDebugging;
        }
        public Settings()
        {
        }
        public bool WorkWithoutDBConnection { get; set; }
        public bool EnableDebugging { get; set; }
    }
    public class SettingsContext : IApplicationContext<Settings>
    {
        private Settings _settings;
        public Settings ContextHolder { get => _settings; set => _settings = value; }
        public event Action<Settings>? onEventChangingApplicationContext;
        public void RaiseEvent()
        {
            onEventChangingApplicationContext?.Invoke(_settings);
        }
        public SettingsContext()
        {
            _settings = new();
        }
    }
}