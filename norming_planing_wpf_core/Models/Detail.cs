using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using norming_planing_wpf_core.Attributes;

namespace norming_planing_wpf_core
{
    public class Detail
    {
        public Detail() { }
        public string Code { get; set; }
        public uint StraightCount { get; set; } = 1;
        public uint OppositeCount { get; set; } = 0;
        public uint TotalCount { get; set; }
        public double Weight { get; set; }
        public double TotalWeight { get; set; }
        public double? MainLenght { get; set; }

        [MarkComponentParam("Количество отверстий")]
        public int? HolesCount { get; set; } = 0;

        [MarkComponentParam("Диаметр отверстий")]
        public int? HolesDiamtr { get; set; }

        public string MarkCode { get; set; }
        public int MarkDraftId { get; set; }
        public Mark Mark { get; set; }
        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
        public int? SteelGradeId { get; set; }
        public SteelGrade? SteelGrade { get; set; }
    }
    public class SteelGrade
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
