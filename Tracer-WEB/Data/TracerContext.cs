using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;

namespace Tracer_WEB.Data;

public class TracerContext : DbContext
{

    public TracerContext(DbContextOptions<TracerContext> options) : base(options)
    {
    }

    // Models
    //public DbSet<Customer> Customer { get; set; }

}
