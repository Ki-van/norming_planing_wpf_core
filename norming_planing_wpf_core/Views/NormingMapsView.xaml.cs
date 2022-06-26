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

namespace norming_planing_wpf_core
{
    /// <summary>
    /// Interaction logic for NormingMapsView.xaml
    /// </summary>
    public partial class NormingMapsView : UserControl
    {
        public NormingMapsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as NormingMapsViewModel).LoadModelsCommand.Execute(this);
        }

        private void NormingMapsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
                (DataContext as NormingMapsViewModel).ChangNormingMapCommand.Execute(e.AddedItems[0]);
        }
    }
}
