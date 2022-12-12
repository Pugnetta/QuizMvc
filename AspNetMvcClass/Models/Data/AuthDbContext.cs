using AspNetMvcClass.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcClass.Models.Data;

public class AuthDbContext: IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {

    }
    public DbSet<Domanda> Domande { get; set; }
    public DbSet<CdTimer> Timers { get; set; }
    public DbSet<GameSession> GameSessions { get; set; }
}
