using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

<<<<<<< HEAD
public class MijnContext :IdentityDbContext<User>{
=======
public class MijnContext :IdentityDbContext<srcUser>{
>>>>>>> 8389422d02a7ce225d0e88b71766fa905e22621d

    public MijnContext(DbContextOptions<MijnContext> options) : base(options)
    {
    }
    public DbSet<Chat> Chat {get;set;}
    public DbSet<Message> Messages {get;set;}
    public DbSet<ChatUser> ChatUsers{get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ChatUser>()
                        .HasKey(x=>new{x.UserId, x.ChatId});
    }
}