using Domain.Entities;
using Domain.Entities.Post;
using Domain.Entities.User;
using Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Gender>();
        modelBuilder.HasPostgresEnum<Active>();
        modelBuilder.Entity<FollowingRelationShip>()
            .HasOne<ApplicationUser>(u => u.ApplicationUser)
            .WithMany(f => f.FollowingRelationShips)
            .HasForeignKey(u => u.ApplicationUserId);
        modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();
        modelBuilder.Entity<UserProfile>()
            .HasIndex(u => u.ApplicationUserId)
            .IsUnique();
        modelBuilder.Entity<Category>()
            .HasIndex(u => u.CategoryName)
            .IsUnique();
        
        modelBuilder.Entity<UserSearchHistory>()
            .HasOne<ApplicationUser>(u => u.ApplicationUserSearch)
            .WithMany(s => s.UserSearchHistories)
            .HasForeignKey(x => x.ApplicationUserSearchId);

        modelBuilder.Entity<PostLike>()
            .ToTable(p => p.HasCheckConstraint("PostLikes", @" ""LikeCount"" >= 0"));

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Story> Stories { get; set; } = null!;
    public DbSet<StoryLike> StoryLikes { get; set; } = null!;
    public DbSet<StoryUser> StoryUsers { get; set; } = null!;
    public DbSet<StoryView> StoryViews { get; set; } = null!;
    public DbSet<StoryStat> StoryStats { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostCategory> PostCategories { get; set; } = null!;
    public DbSet<PostComment> PostComments { get; set; } = null!;
    public DbSet<PostFavorite> PostFavorites { get; set; } = null!;
    public DbSet<PostLike> PostLikes { get; set; } = null!;
    public DbSet<ExternalAccount> ExternalAccounts { get; set; } = null!;
    public DbSet<FollowingRelationShip> FollowingRelationShips { get; set; } = null!;
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public DbSet<UserSetting> UserSettings { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<PostUserLike> PostUserLikes { get; set; } = null!;
    public DbSet<PostView> PostViews { get; set; } = null!;
    public DbSet<PostViewUser> PostViewUsers { get; set; } = null!;
    public DbSet<PostCommentLike> PostCommentLikes { get; set; } = null!;
    public DbSet<ListOfUserCommentLike> ListOfUserCommentLikes { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<PostFavoriteUser> PostFavoriteUsers { get; set; } = null!;
    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;
    public DbSet<UserSearchHistory> UserSearchHistories { get; set; } = null!;
}