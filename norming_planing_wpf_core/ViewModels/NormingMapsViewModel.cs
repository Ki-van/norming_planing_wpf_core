using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Gu.Wpf.DataGrid2D;

namespace norming_planing_wpf_core
{
    internal class NormingMapsViewModel : ObservableObject, IPageViewModel, IDisposable
    {
        private AcszmkdbContext db;
        private NormingMapsView view;
        public string Name => "Карты нормирования";
        public ObservableCollection<NormingMap> NormingMaps { get; set; }
        public ObservableCollection<TOType> TOTypes { get; set; }
        #region Commands
        private RelayCommand loadModelsCommand;
        public RelayCommand LoadModelsCommand
        {
            get
            {
                return loadModelsCommand ??= new RelayCommand(async (view) =>
                {
                    if (db == null)
                    {
                        this.view = view as NormingMapsView;
                        db = new AcszmkdbContext();
                        await db.NormingMaps.Include(nm => nm.TOType).LoadAsync();
                        await db.TOTypes.LoadAsync();

                        NormingMaps = db.NormingMaps.Local.ToObservableCollection();
                        TOTypes = db.TOTypes.Local.ToObservableCollection();

                        this.view.TOTyoesComboBox.ItemsSource = TOTypes;

                        this.view.NormingMapsListView.ItemsSource = NormingMaps;
                        CollectionView defaultView = (CollectionView)CollectionViewSource.GetDefaultView(this.view.NormingMapsListView.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("TOTypeName");
                        defaultView.GroupDescriptions.Add(groupDescription);
                    }
                });
            }
        }
        private RelayCommand changNormingMapCommand;
        public RelayCommand ChangNormingMapCommand
        {
            get
            {
                return changNormingMapCommand ??= new RelayCommand(async (p) =>
                {
                    NormingMap nm = p as NormingMap;
                    this.view.NormingMapDataGrid.SetArray2D(MDListToArray(nm.Norms));
                    this.view.NormingMapDataGrid.SetColumnHeadersSource(nm.FirstArg.Range);
                   
                    if (nm.SecondArg != null) {
                        this.view.NormingMapDataGrid.SetRowHeadersSource(nm.SecondArg.Range);
                        
                        
                       
                    }
                   
                });
            }
        }

        private double[,] MDListToArray(List<List<double>> list)
        {
            if (list == null || list.Count == 0)
                return new double[1, 1];
            double[,] arr = new double[list.Count, list[0].Count];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list[i].Count; j++)
                    arr[i, j] = list[i][j];

            return arr;
        }
        public void Dispose()
        {
            db?.Dispose();
        }
        #endregion
    }
}
