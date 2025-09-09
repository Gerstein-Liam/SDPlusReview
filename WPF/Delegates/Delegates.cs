using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.ViewModels;

namespace WPF.Delegates
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
}
