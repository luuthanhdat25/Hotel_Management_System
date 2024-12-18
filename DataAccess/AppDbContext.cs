﻿using Microsoft.EntityFrameworkCore;
using BusinessObject;
using BusinessObjects;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<BookingDetail> BookingDetails { get; set; }
        
        public DbSet<BookingReservation> BookingReservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.Account)
                .WithOne()
                .HasForeignKey<Customer>(customer => customer.AccountId) 
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType
                {
                    RoomTypeId = 1,
                    RoomTypeName = "Standard",
                    TypeDescription = "Basic room with essential amenities",
                    TypeNote = "Includes free breakfast"
                },
                new RoomType
                {
                    RoomTypeId = 2,
                    RoomTypeName = "Deluxe",
                    TypeDescription = "Spacious room with additional features",
                    TypeNote = "Sea view, includes free breakfast and gym access"
                }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    RoomId = 1,
                    RoomName = "101",
                    RoomDescription = "Standard room with queen bed",
                    RoomMaxCapacity = 2,
                    RoomStatus = RoomStatus.Active,
                    RoomPricePerDate = 100m,
                    RoomTypeId = 1
                },
                new Room
                {
                    RoomId = 2,
                    RoomName = "202",
                    RoomDescription = "Deluxe room with king bed and balcony",
                    RoomMaxCapacity = 3,
                    RoomStatus = RoomStatus.Active,
                    RoomPricePerDate = 200m,
                    RoomTypeId = 2
                }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    EmailAddress = "admin@gmail.com",
                    Password = "123",
                    AccountType = AccountType.Admin,
                    AccountStatus = AccountStatus.Active
                },
                new Account
                {
                    AccountId = 2,
                    EmailAddress = "nguyenvana@gmail.com",
                    Password = "123",
                    AccountType = AccountType.Customer,
                    AccountStatus = AccountStatus.Active
                },
                new Account
                {
                    AccountId = 3,
                    EmailAddress = "tranthib@gmail.com",
                    Password = "123",
                    AccountType = AccountType.Admin,
                    AccountStatus = AccountStatus.Active
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerFullName = "Nguyen Van A",
                    Telephone = "0123456789",
                    CustomerBirthday = new DateOnly(1990, 1, 1),
                    AccountId = 2,
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerFullName = "Tran Thi B",
                    Telephone = "0987654321",
                    CustomerBirthday = new DateOnly(1985, 5, 15),
                    AccountId = 3,
                }
            );
        }
    }
}
