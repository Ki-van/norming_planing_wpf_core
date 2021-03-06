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
    /// Interaction logic for UserCollectionView.xaml
    /// </summary>
    public partial class UserCollectionView : UserControl
    {
        public UserCollectionView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UserCollectionViewModel).LoadModelsCommand.Execute(this);
        }
    }
}
