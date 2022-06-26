using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace norming_planing_wpf_core
{
    public class Mark : QuantitativeEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int DraftId { get; set; }
        public Draft Draft { get; set; }
        public ICollection<Detail> Details { get; set; } = new ObservableCollection<Detail>();
        public ICollection<TO>? TechProcesses { get; set; }
    
        [NotMapped]
        public double TotalDuration { 
            get
            {
                if (TechProcesses == null || TechProcesses.Count == 0)
                    return 0;
                else
                {
                    return TechProcesses.Select(to => to.NormTime*to.OperationCount*(to.GeneratedAssemblie != null?to.GeneratedAssemblie.TotalCount:2)).Sum();
                }
            } 
        }
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                if (TechProcesses == null || TechProcesses.Count == 0)
                    return 0;
                else
                {
                    return TechProcesses.Select(to => to.NormPrice*TotalDuration).Sum();
                }
            }
        }
    }
}
