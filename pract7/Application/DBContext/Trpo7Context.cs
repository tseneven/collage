using Microsoft.EntityFrameworkCore;
using pract7.Application.Models;
using Task = pract7.Application.Models.Task;

namespace pract7.Application.DBContext;

public partial class Trpo7Context : DbContext
{
    public Trpo7Context()
    {
    }

    public Trpo7Context(DbContextOptions<Trpo7Context> options)
        : base(options)
    {
    }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<RevokedToken> RevokedTokens { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        // => optionsBuilder.UseSqlServer("Server=sql.ects;Database=kuznetsovTaskDb;User Id=student_00;Password=student_00;TrustServerCertificate=True;");
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=trpo7;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TokenHash)
                .HasMaxLength(64)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("RefreshTokens_User_Id_fk");
        });

        modelBuilder.Entity<RevokedToken>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Jti).HasMaxLength(64);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Task_pk");

            entity.ToTable("Task");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.OwnerLogin).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.OwnerLoginNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.OwnerLogin)
                .HasConstraintName("Task_User_Login_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("User_pk");

            entity.ToTable("User");

            entity.HasIndex(e => e.Id, "UQ_User_Id").IsUnique();

            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
