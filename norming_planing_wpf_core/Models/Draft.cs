using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace norming_planing_wpf_core
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum DraftStatus
    {
        Defining,
        Planning,
        Finished,
        Rejected
    }
    public class Draft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; } = DateTime.UtcNow;
        public uint Priority { get; set; } = 0;
        public DraftStatus Status { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Mark> Marks { get; set; }

        [NotMapped]
        public double TotalDuration
        {
            get
            {
                if (Marks == null || Marks.Count == 0)
                    return 0;
                else
                {
                    return Marks.Select(m => m.TotalDuration).Sum();
                }
            }
        }
        [NotMapped]
        public double TotalPrice
        {
            get
            {
                if (Marks == null || Marks.Count == 0)
                    return 0;
                else
                {
                    return Marks.Select(m => m.TotalPrice).Sum();
                }
            }
        }
       
    }
    public class EmployReq
    {
        public string Sec { get; set; }
        public string Cval { get; set; }
        public int Count { get; set; }
    }
}
