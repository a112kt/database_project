using System;
using System.Collections.Generic;
using HotelSystem.Models;
using HotelSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<VgetDataOfRoom> VgetDataOfRooms { get; set; }

    public virtual DbSet<WorkOn> WorkOns { get; set; }

    public DbSet<GuestResViewModel> GuestResViewModels { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.GuId).HasName("PK__Guest__7CEC7E93B5BE57E0");

            entity.ToTable("Guest");

            entity.HasIndex(e => e.Email, "UQ__Guest__A9D105348CE49196").IsUnique();

            entity.Property(e => e.GuId).HasColumnName("GU_Id");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(40);
            entity.Property(e => e.Fname).HasMaxLength(25);
            entity.Property(e => e.Lname).HasMaxLength(25);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__DA6C7FC1C6F4ECD1");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
            entity.Property(e => e.Method)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnName("Payment_Date");
            entity.Property(e => e.ResId).HasColumnName("Res_Id");

            entity.HasOne(d => d.Res).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ResId)
                .HasConstraintName("FK__Payment__Res_Id__403A8C7D");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ResId).HasName("PK__Reservat__11B934A59C9C1BAF");

            entity.ToTable("Reservation");

            entity.Property(e => e.ResId).HasColumnName("Res_Id");
            entity.Property(e => e.CheckInDate).HasColumnName("CheckIn_Date");
            entity.Property(e => e.CheckoutDate).HasColumnName("Checkout_Date");
            entity.Property(e => e.GuId).HasColumnName("Gu_Id");
            entity.Property(e => e.ReservationDate).HasColumnName("Reservation_Date");

            entity.HasOne(d => d.Gu).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.GuId)
                .HasConstraintName("FK__Reservati__Gu_Id__3D5E1FD2");

            entity.HasMany(d => d.Rooms).WithMany(p => p.Res)
                .UsingEntity<Dictionary<string, object>>(
                    "RoomReservation",
                    r => r.HasOne<Room>().WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Room_Rese__Room___440B1D61"),
                    l => l.HasOne<Reservation>().WithMany()
                        .HasForeignKey("ResId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Room_Rese__Res_I__4316F928"),
                    j =>
                    {
                        j.HasKey("ResId", "RoomId").HasName("PK__Room_Res__3027D204292A7112");
                        j.ToTable("Room_Reservation", tb => tb.HasTrigger("trg_afterReservationDelete"));
                        j.IndexerProperty<int>("ResId").HasColumnName("Res_Id");
                        j.IndexerProperty<int>("RoomId").HasColumnName("Room_Id");
                    });
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__19EE6A13D819D574");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).HasColumnName("Room_Id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Available");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC07E5CD8287");

            entity.ToTable("Service");

            entity.Property(e => e.Description).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasMany(d => d.Gus).WithMany(p => p.Servs)
                .UsingEntity<Dictionary<string, object>>(
                    "GuestService",
                    r => r.HasOne<Guest>().WithMany()
                        .HasForeignKey("GuId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Guest_Ser__Gu_Id__49C3F6B7"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("ServId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Guest_Ser__Serv___48CFD27E"),
                    j =>
                    {
                        j.HasKey("ServId", "GuId").HasName("PK__Guest_Se__2654ECBB950730B8");
                        j.ToTable("Guest_Service");
                        j.IndexerProperty<int>("ServId").HasColumnName("Serv_Id");
                        j.IndexerProperty<int>("GuId").HasColumnName("Gu_Id");
                    });
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07C51F68EC");

            entity.Property(e => e.Fname).HasMaxLength(20);
            entity.Property(e => e.Lname).HasMaxLength(20);
            entity.Property(e => e.MngrId).HasColumnName("Mngr_Id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.Mngr).WithMany(p => p.InverseMngr)
                .HasForeignKey(d => d.MngrId)
                .HasConstraintName("FK__Staff__Mngr_Id__4CA06362");
        });

        modelBuilder.Entity<VgetDataOfRoom>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VGetDataOfRoom");

            entity.Property(e => e.RoomId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkOn>(entity =>
        {
            entity.HasKey(e => new { e.StaffId, e.RoomId }).HasName("PK__WorkOn__134F128205A9657C");

            entity.ToTable("WorkOn");

            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
            entity.Property(e => e.RoomId).HasColumnName("Room_Id");
            entity.Property(e => e.AssignDate).HasColumnName("Assign_Date");
            entity.Property(e => e.DueDate).HasColumnName("Due_Date");
            entity.Property(e => e.Status)
                .HasMaxLength(25)
                .HasColumnName("status");
            entity.Property(e => e.Task).HasMaxLength(40);

            entity.HasOne(d => d.Room).WithMany(p => p.WorkOns)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOn__Room_Id__5070F446");

            entity.HasOne(d => d.Staff).WithMany(p => p.WorkOns)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOn__Staff_Id__4F7CD00D");
        });

        modelBuilder.Entity<GuestResViewModel>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
