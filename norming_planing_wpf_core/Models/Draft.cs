using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public DraftStatus Status { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}
