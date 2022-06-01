using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

namespace norming_planing_wpf_core
{
    internal class MaterialsViewModel : ObservableObject, IPageViewModel, IDisposable
    {
        private AcszmkdbContext db;
        private ICommand _openMaterialTypeExplorerCommand;
        public string Name => "Металлопрофиль";
        public ObservableCollection<MaterialType> MaterialTypes { get; set; }
        public MaterialType materialTypeSelectedItem { get; set; }
        public ICommand OpenMaterialTypeExplorerCommand
        {
            get => _openMaterialTypeExplorerCommand ??= new RelayCommand(async (p) =>
                                 {
                                     if (p == null)
                                     {
                                         p = new MaterialType();
                                     }
                                     MaterialTypeView materialTypeView = new()
                                     {
                                         DataContext = p as MaterialType
                                     };

                                     if ((bool)materialTypeView.ShowDialog())
                                     {
                                         if (((MaterialType)p).Id == MaterialType.DefaultId)
                                            await db.MaterialTypes.AddAsync((MaterialType)p);
                                         else
                                            db.MaterialTypes.Update((MaterialType)p);

                                         await db.SaveChangesAsync();
                                     }
                                 });
            set { _openMaterialTypeExplorerCommand = value; }
        }

        public MaterialsViewModel()
        {
            InitiateVM();
        }
        async public void InitiateVM()
        {
            db = new AcszmkdbContext();
            await db.MaterialTypes.Include(mt => mt.Materials).LoadAsync();
            MaterialTypes = db.MaterialTypes.Local.ToObservableCollection();

            RaisePropertyChanged(nameof(MaterialTypes));
        }
        
        public void Dispose()
        {
           db.Dispose();
        }
    }
}
