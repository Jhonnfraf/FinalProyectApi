using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinalProyectApi.Models;

public partial class CalendarioDbContext : DbContext
{
    public CalendarioDbContext()
    {
    }

    public CalendarioDbContext(DbContextOptions<CalendarioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<ReminderDay> ReminderDays { get; set; }

    public virtual DbSet<Routine> Routines { get; set; }

    public virtual DbSet<RoutineDay> RoutineDays { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CalendarioDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.CalendarId).HasName("PK__Calendar__53CFDBADF5730503");

            entity.Property(e => e.CalendarId).HasColumnName("CalendarID");
            entity.Property(e => e.CalendarName).HasMaxLength(100);
            entity.Property(e => e.CalendarPasswordHash).HasMaxLength(256);
            entity.Property(e => e.CalendarPasswordSalt).HasMaxLength(256);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Calendars)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calendars__UserI__3C69FB99");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Events__7944C8701600392B");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.CalendarId).HasColumnName("CalendarID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Calendar).WithMany(p => p.Events)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Events__Calendar__403A8C7D");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Reminder__01A830A7716B4A95");

            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");
            entity.Property(e => e.CalendarId).HasColumnName("CalendarID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Calendar).WithMany(p => p.Reminders)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reminders__Calen__44FF419A");
        });

        modelBuilder.Entity<ReminderDay>(entity =>
        {
            entity.HasKey(e => e.ReminderDayId).HasName("PK__Reminder__4E9E0471F6383E84");

            entity.Property(e => e.ReminderDayId).HasColumnName("ReminderDayID");
            entity.Property(e => e.ReminderId).HasColumnName("ReminderID");

            entity.HasOne(d => d.Reminder).WithMany(p => p.ReminderDays)
                .HasForeignKey(d => d.ReminderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReminderD__Remin__4D94879B");
        });

        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.RoutineId).HasName("PK__Routines__A6E3E51ABD9CB45F");

            entity.Property(e => e.RoutineId).HasColumnName("RoutineID");
            entity.Property(e => e.CalendarId).HasColumnName("CalendarID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Calendar).WithMany(p => p.Routines)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Routines__Calend__49C3F6B7");
        });

        modelBuilder.Entity<RoutineDay>(entity =>
        {
            entity.HasKey(e => e.RoutineDayId).HasName("PK__RoutineD__FB545847A28BAAF1");

            entity.Property(e => e.RoutineDayId).HasColumnName("RoutineDayID");
            entity.Property(e => e.RoutineId).HasColumnName("RoutineID");

            entity.HasOne(d => d.Routine).WithMany(p => p.RoutineDays)
                .HasForeignKey(d => d.RoutineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoutineDa__Routi__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8D657706");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FCC48930").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341F3D9309").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PasswordSalt).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
