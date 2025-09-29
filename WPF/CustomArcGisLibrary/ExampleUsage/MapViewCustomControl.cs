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
using WPF.CustomArcGisLibrary.Lib;

namespace WPF.CustomArcGisLibrary.ExampleUsage
{

    public class MapViewCustomControl : Control
    {
        public ConcreteMapViewModel ViewModel
        {
            get { return (ConcreteMapViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ConcreteMapViewModel), typeof(MapViewCustomControl), new PropertyMetadata(null));
        static MapViewCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapViewCustomControl), new FrameworkPropertyMetadata(typeof(MapViewCustomControl)));
        }
        private DrawableMapViewBaseControl DrawableMapView;
        public Button _DebugBt { get; set; }
        public Button _StartDrawButton { get; set; }
        public Button _SaveDrawButton { get; set; }
        public Button? _editExploitationBt { get; set; }

        public Label? _editStatusLabel{ get; set; }
        public Button? _deleteExploitationBt { get; set; }
        public TextBox _NewExploitationNameTxtBox { get; set; }
        private bool EditEnable = false;
        public MapViewCustomControl()
        {
            this.Loaded += OnFullyLoaded;
        }
        public override void OnApplyTemplate()
        {
            DrawableMapView = GetTemplateChild("DrawableMapView") as DrawableMapViewBaseControl;
            base.OnApplyTemplate();
            _DebugBt = GetTemplateChild("DebugBt") as Button;
            _DebugBt.Click += _DebugBt_Click;
            _StartDrawButton = GetTemplateChild("DrawNewExploitationBt") as Button;
            _StartDrawButton.Click += _StartDrawButton_Click;
            _SaveDrawButton = GetTemplateChild("SaveNewExploitationBt") as Button;
            _SaveDrawButton!.Click += _SaveDrawButton_Click;
            _editExploitationBt = GetTemplateChild("EditExploitationBt") as Button;
            _NewExploitationNameTxtBox = GetTemplateChild("NewExploitationName") as TextBox;
            _deleteExploitationBt = GetTemplateChild("DeleteExploitationBt") as Button;
            _deleteExploitationBt!.Click += DeleteExploitationClickHandler;
            _editExploitationBt = GetTemplateChild("EditExploitationBt") as Button;
            _editExploitationBt!.Click += _editExploitationBt_Click;
         
            _editStatusLabel = GetTemplateChild("editStatusLabel") as Label;
            _editStatusLabel.Content = $"Edit:OFF";

            this.KeyDown += SDPlusMap_KeyDown;
        }
        private void _editExploitationBt_Click(object sender, RoutedEventArgs e)
        {
            ChangeEditMode();
        }
        private void ChangeEditMode()
        {
            EditEnable = !EditEnable;
            DrawableMapView.SetEditMode(EditEnable);
    

            _editStatusLabel.Content = !EditEnable?  $"Edit:OFF": $"Edit:ON ";
        }
        private void DeleteExploitationClickHandler(object sender, RoutedEventArgs e)
        {
            DrawableMapView.Delete();
        }
        private void _DebugBt_Click(object sender, RoutedEventArgs e)
        {
            DrawableMapView.DebugContext();
        }
        private void OnFullyLoaded(object sender, RoutedEventArgs e)
        {
            DrawableMapView.SetContext(ViewModel, OnEditionsRulesChanges);
        }
        private void _StartDrawButton_Click(object sender, RoutedEventArgs e)
        {
            DrawableMapView.DrawNewItem();
        }
        private void _SaveDrawButton_Click(object sender, RoutedEventArgs e)
        {
            DrawableMapView.SaveDraw(_NewExploitationNameTxtBox.Text);
        }
        private void OnEditionsRulesChanges(MapInteractionContext Rules)
        {
            _StartDrawButton.IsEnabled = Rules.AllowAdding;
            _SaveDrawButton.IsEnabled = Rules.AllowAdding;
            _NewExploitationNameTxtBox.Text = Rules.DrawName;
        }
        private void SDPlusMap_KeyDown(object sender, KeyEventArgs e)
        {
            //AddNewItem
            if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                DrawableMapView.DrawNewItem();
            }
            //ToggleEditMode
            if (e.Key == Key.E && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ChangeEditMode();
            }
            //Delete Graphic or Vertices
            if (e.Key == Key.X && Keyboard.Modifiers == ModifierKeys.Control)
            {
                DrawableMapView.Delete();
            }
            //Save new Item or change on existing
            if ((e.Key == Key.S) && Keyboard.Modifiers == ModifierKeys.Control)
            {
               // HierarchicalPrint.Hierarchical_Print(HierarchicalPrint.CustomControl, "CC", "SDPlusMap_KeyDown", "Escap");
                DrawableMapView.SaveDraw(_NewExploitationNameTxtBox.Text);
            }
        }
    }
}
