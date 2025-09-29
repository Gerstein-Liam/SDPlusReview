using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ViewModels
{
    public class ErrorViewModel: INotifyDataErrorInfo
    {

        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        public bool HasErrors => _propertyErrors.Any();


        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


        public void AddError(string propertyName, string errorMessage)
        {

            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            onErrorChanged(propertyName);
        }

        public void ClearError(string propertyName)
        {


            if (_propertyErrors.Remove(propertyName))
            {

                onErrorChanged(propertyName);
            }

        }

        private void onErrorChanged(string propertyName)
        {

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        }


        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }
    }
}
