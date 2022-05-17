using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace norming_planing_wpf_core
{
    public class Employee
    {
        [Key]
        public string Passport { get; set; }
        public string Fullname { get; set; }
        public EmployeePosition Position { get; set; }
        public uint Qualification { get; set; }
        public ICollection<TaskParticipation> Participations { get; set; } = new List<TaskParticipation>();
        public ICollection<ShiftTask> ShiftTasks { get; set; } = new List<ShiftTask>();
       /* public ICollection<ShiftTask>? IssuingShiftTasks { get; set; }*/
    }

    public class TaskParticipation
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ShiftTaskId { get; set; }
        public ShiftTask ShiftTask { get; set; }

        public double? ParticipationPercentage { get; set; }
    }

    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public ICollection<TOType> TOTypes { get; set; }
    }
    
}
