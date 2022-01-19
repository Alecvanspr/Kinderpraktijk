using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class MijnContext : IdentityDbContext<srcUser>{

    public MijnContext(DbContextOptions<MijnContext> options) : base(options)
    {
    }
    public DbSet<Chat> Chat {get;set;}
    public DbSet<Message> Messages {get;set;}
    public DbSet<ChatUser> ChatUsers{get;set;}
    public DbSet<Melding> Meldingen{get;set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ChatUser>()
                        .HasKey(x=>new{x.UserId, x.ChatId});
        builder.Entity<srcUser>()
                    .HasMany(x=>x.Childeren)
                    .WithOne(x=>x.Parent);
        builder.Entity<srcUser>()
                    .HasMany(x=>x.Clients)
                    .WithOne(x=>x.Specialist);
    }
}