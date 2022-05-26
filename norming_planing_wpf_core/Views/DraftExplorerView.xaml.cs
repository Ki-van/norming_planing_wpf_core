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
using System.Windows.Shapes;

namespace norming_planing_wpf_core
{
    /// <summary>
    /// Interaction logic for DraftExplorerView.xaml
    /// </summary>
    public partial class DraftExplorerView : Window
    {
        public DraftExplorerView(Draft? draft)
        {
            InitializeComponent();
            this.DataContext = new DraftExplorerViewModel(draft);
        }
    }
}
