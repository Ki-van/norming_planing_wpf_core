using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for MaterialsView.xaml
    /// </summary>
    public partial class MaterialsView : UserControl
    {
        public MaterialsView()
        {
            InitializeComponent();
        }

        private void materialTypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1 && e.AddedItems[0] is MaterialType)
            {
                materialsDataGrid.Columns.Clear();
                materialsDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Наименование",
                    Binding = new Binding("Name")
                });

                foreach (StructureItem item in ((MaterialType)e.AddedItems[0]).StructureItems)
                {
                    materialsDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = item.Name,
                        Binding = new Binding(String.Format("ScalarItems[{0}].Val", ((MaterialType)e.AddedItems[0]).StructureItems.IndexOf(item)))
                        {
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        },
                        IsReadOnly = item.Func != null
                    });
                }
            }
        }

        private void materialTypesDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MaterialsViewModel)DataContext).OpenMaterialTypeExplorerCommand.Execute(materialTypesDataGrid.SelectedItem);
        }

        private void materialsDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            ObservableCollection<ScalarItem> si = new();
            foreach (StructureItem item in ((MaterialType)materialTypesDataGrid.SelectedItem).StructureItems)
                si.Add(new ScalarItem(item.Var));

            e.NewItem = new Material
            {
                ScalarItems = si,
                Type = (MaterialType)materialTypesDataGrid.SelectedItem
            };
        }

        private void materialsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(((Binding)((DataGridTextColumn)e.Column).Binding).Path.Path.StartsWith("ScalarItems"))
                ((MaterialsViewModel)DataContext).RecomputeMaterialDependenciesCommand.Execute(materialsDataGrid.SelectedItem);
            ((DataGrid)sender).ItemsSource = null;
            ((DataGrid)sender).ItemsSource = ((MaterialType)materialTypesDataGrid.SelectedItem).Materials;
        }
    }
}
