using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.NumPad4)
            {


                propertiesListingView.Visibility = propertiesListingView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.NumPad2)
            {


                ContextualArea.Visibility = ContextualArea.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.NumPad6)
            {


                AdressListContainer.Visibility = AdressListContainer.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }


        }
    }
}
