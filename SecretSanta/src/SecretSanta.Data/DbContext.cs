using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using DbContext = SecretSanta.Data.DbContext;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext() : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        {
            Database.Migrate();
        }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users => Set<User>();
        public Microsoft.EntityFrameworkCore.DbSet<Group> Groups => Set<Group>();
        public Microsoft.EntityFrameworkCore.DbSet<Gift> Gifts => Set<Gift>();
        public Microsoft.EntityFrameworkCore.DbSet<Assignment> Assignments => Set<Assignment>();

        private StreamWriter LogStream { get; } = new StreamWriter(
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "db.log"), append: true);

// Kept getting "Invalid token '=' in class, record, struct, or interface member declaration" on this.
// No idea why. Would have used it otherwise.
/*      Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "db.log"))
            .CreateLogger();

        public static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => { 
            builder
                .AddSerilog(logger: Log.Logger)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("SecretSanta.Data.DbContext", LogLevel.Debug)
                .AddEventLog(); 
        }); */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }
 
            optionsBuilder.LogTo(LogStream.WriteLine);
//            .UseLoggerFactory(DbLoggerFactory)
//            .UseSqlite("Data Source=main.db");
        }

        public override void Dispose()
        {
            base.Dispose();
            LogStream.Dispose();
            GC.SuppressFinalize(this);
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await LogStream.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

//            modelBuilder.Entity<User>()
//                .HasMany(user => user.Gifts)
//                .WithOne(gift => gift.Receiver);

//            modelBuilder.Entity<User>()
//                .HasMany(user => user.Groups)
//                .WithMany(group => group.Users);

//            modelBuilder.Entity<Group>()
//                .HasMany(group => group.Users)
//                .WithMany(user => user.Groups);

//            modelBuilder.Entity<Group>()
//                .HasMany(group => group.Assignments)
//                .WithOne(assignment => assignment.Group);

            modelBuilder.Entity<User>().HasAlternateKey(user => new { user.FirstName, user.LastName });
            modelBuilder.Entity<Group>().HasAlternateKey(group => new { group.Name });
            modelBuilder.Entity<Gift>().HasAlternateKey(gift => new { gift.Title });
            modelBuilder.Entity<Assignment>().HasAlternateKey(assignment => new { assignment.GiverName });

            // Just got to help EntityFramework figure out what it's looking at.
            modelBuilder.Entity<User>()
                .Property(user => user.Groups)
                .HasConversion(
                    groups => JsonSerializer.Serialize(groups, null),
                    groups => JsonSerializer.Deserialize<List<Group>>(groups, null)
                );

            modelBuilder.Entity<Group>()
                .Property(group => group.Users)
                .HasConversion(
                    users => JsonSerializer.Serialize(users, null),
                    users => JsonSerializer.Deserialize<List<User>>(users, null)
                );

            modelBuilder.Entity<Group>()
                .Property(group => group.Assignments)
                .HasConversion(
                    assignments => JsonSerializer.Serialize(assignments, null),
                    assignments => JsonSerializer.Deserialize<List<Assignment>>(assignments, null)
                );

            modelBuilder.Entity<Assignment>()
                .Property(assignment => assignment.Giver)
                .HasConversion(
                    giver => JsonSerializer.Serialize(giver, null),
                    giver => JsonSerializer.Deserialize<User>(giver, null)
                );

            modelBuilder.Entity<Assignment>()
                .Property(assignment => assignment.Receiver)
                .HasConversion(
                    receiver => JsonSerializer.Serialize(receiver, null),
                    receiver => JsonSerializer.Deserialize<User>(receiver, null)
                );
        }
    }
}
