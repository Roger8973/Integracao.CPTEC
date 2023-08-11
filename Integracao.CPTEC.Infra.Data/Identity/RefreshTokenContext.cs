using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Integracao.CPTEC.Infra.Data.Identity;

public class RefreshTokenContext : IdentityDbContext
{
    public RefreshTokenContext(DbContextOptions<RefreshTokenContext> options) : base(options) { }

}