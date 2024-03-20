using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class Persistence : DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<Destination> destinations { get; set; }
        public DbSet<Comfort> comforts { get; set; }
        public Persistence(DbContextOptions<Persistence> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class Destination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public Int32 Price { get; set; }
    }
    public class Comfort
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public double Multiplier { get; set; }
    }
}