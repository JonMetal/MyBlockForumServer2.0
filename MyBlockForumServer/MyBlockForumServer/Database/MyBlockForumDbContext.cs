using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer.Database;

public partial class MyBlockForumDbContext : DbContext
{
    public MyBlockForumDbContext()
    {
    }

    public MyBlockForumDbContext(DbContextOptions<MyBlockForumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Thread> Threads { get; set; }

    public virtual DbSet<ThreadTheme> ThreadThemes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyBlockForum;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83F551F40EA");

            entity.ToTable("message");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Text)
                .HasMaxLength(1024)
                .HasColumnName("text");
            entity.Property(e => e.ThreadId).HasColumnName("thread_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Thread).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__thread___40058253");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message__user_id__3F115E1A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F7CB62B7F");

            entity.ToTable("role");

            entity.HasIndex(e => e.Title, "UQ__role__E52A1BB376F95416").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__status__3213E83F54EAC6A2");

            entity.ToTable("status");

            entity.HasIndex(e => e.Title, "UQ__status__E52A1BB30A6D8A54").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Thread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__thread__3213E83F6C6B1891");

            entity.ToTable("thread");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.ThreadThemeId).HasColumnName("thread_theme_id");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .HasColumnName("title");
            entity.Property(e => e.UserCreatorId).HasColumnName("user_creator_id");

            entity.HasOne(d => d.ThreadTheme).WithMany(p => p.Threads)
                .HasForeignKey(d => d.ThreadThemeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__thread__thread_t__3B40CD36");

            entity.HasOne(d => d.UserCreator).WithMany(p => p.Threads)
                .HasForeignKey(d => d.UserCreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__thread__user_cre__3A4CA8FD");
        });

        modelBuilder.Entity<ThreadTheme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__thread_t__3213E83F3C755F13");

            entity.ToTable("thread_theme");

            entity.HasIndex(e => e.Title, "UQ__thread_t__E52A1BB3212EA6B9").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F70DCB036");

            entity.ToTable("user");

            entity.HasIndex(e => e.Login, "UQ__user__7838F272B0F206C3").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__user__AB6E6164845C9150").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(128)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(56)
                .HasColumnName("email");
            entity.Property(e => e.Karma)
                .HasDefaultValue(0)
                .HasColumnName("karma");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__role_id__367C1819");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__status_id__3587F3E0");

            entity.HasMany(d => d.FromUsers).WithMany(p => p.ToUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserUserKarma",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_user__from___46B27FE2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_user__to_us__47A6A41B"),
                    j =>
                    {
                        j.HasKey("FromUserId", "ToUserId").HasName("PK__user_use__C5B0E86FD04B3762");
                        j.ToTable("user_user_karma");
                        j.IndexerProperty<Guid>("FromUserId").HasColumnName("from_user_id");
                        j.IndexerProperty<Guid>("ToUserId").HasColumnName("to_user_id");
                    });

            entity.HasMany(d => d.ThreadsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserThread",
                    r => r.HasOne<Thread>().WithMany()
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_thre__threa__43D61337"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_thre__user___42E1EEFE"),
                    j =>
                    {
                        j.HasKey("UserId", "ThreadId").HasName("PK__user_thr__AEFF2920EF4B6D43");
                        j.ToTable("user_thread");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<Guid>("ThreadId").HasColumnName("thread_id");
                    });

            entity.HasMany(d => d.ToUsers).WithMany(p => p.FromUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserUserKarma",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_user__to_us__47A6A41B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__user_user__from___46B27FE2"),
                    j =>
                    {
                        j.HasKey("FromUserId", "ToUserId").HasName("PK__user_use__C5B0E86FD04B3762");
                        j.ToTable("user_user_karma");
                        j.IndexerProperty<Guid>("FromUserId").HasColumnName("from_user_id");
                        j.IndexerProperty<Guid>("ToUserId").HasColumnName("to_user_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
