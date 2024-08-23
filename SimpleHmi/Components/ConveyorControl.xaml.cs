using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SimpleHmi.Components
{
    /// <summary>
    /// Interaction logic for ConveyorControl.xaml
    /// </summary>
    public partial class ConveyorControl : UserControl
    {

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ConveyorControl), new PropertyMetadata(String.Empty));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        /*
        public static readonly RoutedEvent RestartClickEvent = EventManager.RegisterRoutedEvent(nameof(RestartClick), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ConveyorControl));

        public event RoutedEventHandler RestartClick
        {
            add { AddHandler(RestartClickEvent, value); }
            remove { RemoveHandler(RestartClickEvent, value); }
        }

        public void OnRestartClick(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(RestartClickEvent));
        }
        */
        //public static readonly DependencyProperty RestartCommandProperty = DependencyProperty.Register("RestartCommand", typeof(ICommand), typeof(ConveyorControl), new PropertyMetadata(null));

        public static readonly DependencyProperty RestartCommandProperty = DependencyProperty.Register(nameof(RestartCommand), typeof(ICommand), typeof(ConveyorControl));

        public ICommand RestartCommand //NavigateToPageCommand
        {
            get => (ICommand)GetValue(RestartCommandProperty);
            set => SetValue(RestartCommandProperty, value);
            
        }


        public ConveyorControl()
        {
            InitializeComponent();
        }


    }
}
