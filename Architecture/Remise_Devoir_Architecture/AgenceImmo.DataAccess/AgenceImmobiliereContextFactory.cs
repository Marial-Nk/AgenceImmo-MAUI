using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AgenceImmo.DataAccess;

public class AgenceImmobiliereContextFactory : IDesignTimeDbContextFactory<AgenceImmobiliereContext>
{
    public AgenceImmobiliereContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AgenceImmobiliereContext>()
            .UseSqlServer("Server=.;Database=AgenceImmobiliere;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        return new AgenceImmobiliereContext(options);
    }
}
