using Microsoft.EntityFrameworkCore;
using System.Data;

namespace SuperHerosApi.Data;

public class DataAccessClass : DbContext
{
    public DataAccessClass(DbContextOptions<DataAccessClass> options) : base(options){ }

    public DbSet<Superhero> superheros { get; set; }

}
