using  Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;


namespace NetKubernetes.Data;

public  class  AppDbContext:IdentityDbContext<Usuario>
{


    public AppDbContext(DbContextOptions<AppDbContext>Opt):base(Opt)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<Inmueble>? Inmuebles{get;set;}

}

