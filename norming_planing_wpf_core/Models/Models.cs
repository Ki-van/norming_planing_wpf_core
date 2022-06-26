using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class QuantitativeEntity
    {
        public uint StraightCount { get; set; } = 1;
        public uint OppositeCount { get; set; } = 0;
       // public uint Weight { get; set; }
        [NotMapped]
        public uint TotalCount { get { return StraightCount + OppositeCount; } }
        //[NotMapped]
        //public uint TotalWeight { get { return TotalCount * Weight; } }
    }
}
