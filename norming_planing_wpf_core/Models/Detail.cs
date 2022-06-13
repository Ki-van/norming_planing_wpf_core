using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using norming_planing_wpf_core.Attributes;

namespace norming_planing_wpf_core
{
    public class Detail : QuantitativeEntity
    {
        public Detail() { }
        public string Code { get; set; }
        public double Weight { get; set; }
        [NotMapped]
        public double TotalWeight { get { return TotalWeight * TotalCount; } }
        public double? MainLenght { get; set; }
        public string MarkCode { get; set; }
        public int MarkDraftId { get; set; }
        public Mark Mark { get; set; }
        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
        public int? SteelGradeId { get; set; }
        public SteelGrade? SteelGrade { get; set; }

        public ICollection<TO> TOs { get; set; }
    }
}
