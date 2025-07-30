using Domain.Entities;
using Domain.Entities.TypedIds;

namespace Application.Common.Interfaces.Repositories
{
    public interface IJwtRepository : IRepository<Jwt, JwtId> { }
}
