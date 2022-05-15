using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace norming_planing_wpf_core
{
    internal class PlanningViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Планирование производства";
    }
}
