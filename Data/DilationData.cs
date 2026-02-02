using Microsoft.EntityFrameworkCore;
using Smart_Dilation_Management.Class;
using Smart_Dilation_Management.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Smart_Dilation_Management.Data
{
    public class DilationData : DbContext
    {
        public DilationData(DbContextOptions<DilationData> options)
          : base(options) { }

        public DbSet<DilationOrder> DilationOrder { get; set; }
        public DbSet<DoseLog> DoseLog { get; set; }
        public DbSet<EyeDrop> EyeDrop { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DilationOrder>()
                .HasOne(o => o.Doctor)
                .WithMany()
                .HasForeignKey(o => o.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoseLog>()
                .HasOne(d => d.Staff)
                .WithMany()
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.NoAction);
        }





    }
}
