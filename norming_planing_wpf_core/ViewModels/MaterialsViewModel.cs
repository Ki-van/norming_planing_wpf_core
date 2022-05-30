using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace norming_planing_wpf_core
{
    internal class MaterialsViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Металлопрофиль";
        public ObservableCollection<MaterialType> MaterialTypes;
        public MaterialsViewModel()
        {

        }
    }
}
