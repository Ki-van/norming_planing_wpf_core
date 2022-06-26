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
using System.ComponentModel;

namespace norming_planing_wpf_core
{
    internal class TOTypesViewModel : ObservableObject, IPageViewModel, IDisposable
    {
        private AcszmkdbContext db;
        private TOType toType;

        public string Name => "Типы ТО";
        public ObservableCollection<TOType> TOTypes { get; set; }
        public ObservableCollection<EntityParamType> ParamTypes { get; set; } = new();
        public TOType SelectedItemTreeViewSs
        {
            get
            {
                return toType;
            }
            set
            {
                toType = value;
                RaisePropertyChanged(nameof(SelectedItemTreeViewSs));
            }
        }
        #region Commands
        
        #endregion
        public TOTypesViewModel()
        {
            
            InitiateVM();
        }
        async public void InitiateVM()
        {
            db = new AcszmkdbContext();
            ParamTypes.Add(new EntityParamType("Число", typeof(Double).Name, 0));
            await db.UserCollections.ForEachAsync(uc => ParamTypes.Add(new EntityParamType(uc.Name, uc.GetType().Name, uc.Id)));
            
            await db.TOTypes.Include(tot => tot.Instruments).LoadAsync();
            TOTypes = db.TOTypes.Local.ToObservableCollection();
            if (TOTypes.Any())
                SelectedItemTreeViewSs = TOTypes.First();
            else
            {
                //TODO: create new type, add to collection 
            }
            //RaisePropertyChanged(nameof(TOTypes));
        }
        
        public void Dispose()
        {
           db?.Dispose();
        }
    }
}
