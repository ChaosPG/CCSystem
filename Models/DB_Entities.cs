using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CCSystem_New.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("CCS_DB") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<MachineList> MachineLists { get; set; }
        public DbSet<MaterialList> MaterailLists { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Machine_Asphalt> Machine_Asphalts { get; set; }
        public DbSet<Machine_Bulldozer> Machine_Bulldozers { get; set; }
        public DbSet<Machine_Crane> Machine_Cranes { get; set; }
        public DbSet<Machine_Excavator> Machine_Excavators { get; set; }
        public DbSet<Machine_Folklift> Machine_Folklifts { get; set; }
        public DbSet<Machine_Roller> Machine_Rollers { get; set; }
        public DbSet<Machine_WheelLoader> Machine_WheelLoaders { get; set; }
        public DbSet<Floor_Material_01> Floor_Material_01 { get; set; }
        public DbSet<Floor_Material_02> Floor_Material_02 { get; set; }
        public DbSet<General_Material_01> General_Material_01 { get; set; }
        public DbSet<General_Material_02> General_Material_02 { get; set; }
        public DbSet<Roof_Material_01> Roof_Material_01 { get; set; }
        public DbSet<Roof_Material_02> Roof_Material_02 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<MachineList>().ToTable("MachineList");
            modelBuilder.Entity<MaterialList>().ToTable("MaterialList");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Machine_Asphalt>().ToTable("Machine_Asphalt");
            modelBuilder.Entity<Machine_Bulldozer>().ToTable("Machine_Bulldozer");
            modelBuilder.Entity<Machine_Crane>().ToTable("Machine_Crane");
            modelBuilder.Entity<Machine_Excavator>().ToTable("Machine_Excavator");
            modelBuilder.Entity<Machine_Folklift>().ToTable("Machine_Folklift");
            modelBuilder.Entity<Machine_Roller>().ToTable("Machine_Roller");
            modelBuilder.Entity<Machine_WheelLoader>().ToTable("Machine_WheelLoader");
            modelBuilder.Entity<Floor_Material_01>().ToTable("Floor_Material_01");
            modelBuilder.Entity<Floor_Material_02>().ToTable("Floor_Material_02");
            modelBuilder.Entity<General_Material_01>().ToTable("General_Material_01");
            modelBuilder.Entity<General_Material_02>().ToTable("General_Material_02");
            modelBuilder.Entity<Roof_Material_01>().ToTable("Roof_Material_01");
            modelBuilder.Entity<Roof_Material_02>().ToTable("Roof_Material_02");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
