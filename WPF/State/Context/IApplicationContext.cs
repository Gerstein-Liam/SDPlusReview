using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WPF.State.Context
{
    public interface IApplicationContext<T> where T : class
    {
        public T ContextHolder { get; set; }
        public event Action<T> onEventChangingApplicationContext;
        public void RaiseEvent();
    }
}