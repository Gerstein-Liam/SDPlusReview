using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Windows.ApplicationModel.Background;
using WPF.Commands.Generics;
using WPF.Models;

namespace WPF.ViewModels.DashboardItems
{
  
    //https://www.youtube.com/watch?v=JZth6r2UXLU
    
    public class OwnerEditViewModel : ViewModelBase, INotifyDataErrorInfo
    {



        


        private bool _showMe;
        public bool ShowMe
        {
            get
            {
                return _showMe;
            }
            set
            {
                _showMe = value;
                OnPropertyChanged(nameof(ShowMe));
            }
        }


        private string _ownerName;
        public string OwnerName
        {
            get
            {
                return _ownerName;
            }
            set
            {
                _ownerName = value;


                _errorViewModel?.ClearError(nameof(OwnerName));
                if (_ownerName == string.Empty) {
                    _errorViewModel?.AddError(nameof(OwnerName), "Le nom ne peut pas être vide");
                }

                
                OnPropertyChanged(nameof(OwnerName));
                ValidedBt.RaiseCanExecuteChanged();
            }
        }

        private readonly ErrorViewModel _errorViewModel;
        public bool Context = false;        //False:add   True: Edit

        public ActionCommand ValidedBt { get; }


        private Action<Owner> editedAddedOwner;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;


        public bool HasErrors=> _errorViewModel.HasErrors;

        public OwnerEditViewModel(Action<Owner> editedAddedOwner)
        {
            this.editedAddedOwner = editedAddedOwner;



            ValidedBt = new ActionCommand(() =>
            {


                Owner owner = new Owner() { OwnerName = OwnerName, ID = Guid.NewGuid().ToString() };
                editedAddedOwner(owner);
            },
            ButtonCanExecute
            
            
            );
            OwnerName = "Name...";
            // OwnerName = string.Empty;

            _errorViewModel = new ErrorViewModel();
            _errorViewModel.ErrorsChanged += _errorViewModel_ErrorsChanged;
        }

        private void _errorViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(e.PropertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorViewModel.GetErrors(propertyName);
        }

  

        private bool ButtonCanExecute(object p) {

            return !_errorViewModel.HasErrors;
        
        }
    }
}
