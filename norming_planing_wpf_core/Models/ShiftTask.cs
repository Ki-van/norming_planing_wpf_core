using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace norming_planing_wpf_core
{
    public enum ShiftTaskStatus
    {
        Waiting,
        Performed,
        Complited,
        Defected
    }

    public class ShiftTask
    {
        public int Id { get; set; }
        public TO TO { get; set; }
        public ShiftTaskStatus Status { get; set; }
        public double? NormTime { get; set; }
        public double? NormPrice { get; set; }
        public DateTime? Issue { get; set; }
       /* public Employee IssuingEmployeeId { get; set; }
        public Employee IssuingEmployee { get; set; }*/
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<TaskParticipation> Participations { get; set; } = new List<TaskParticipation>(); 
    }

    class ShiftTaskConfiguration : IEntityTypeConfiguration<ShiftTask>
    {
        public void Configure(EntityTypeBuilder<ShiftTask> builder)
        {
            /*builder.HasOne(st => st.IssuingEmployee)
                .WithMany(e => e.IssuingShiftTasks)
                .HasForeignKey(st => st.IssuingEmployeeId);*/
        }
    }
}
