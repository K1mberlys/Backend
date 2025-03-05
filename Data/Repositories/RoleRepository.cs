using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;

namespace Data.Repositories;

public class RoleRepository(DataContext context) : BaseRepository<RoleEntity>(context), IRoleRepository
{
}
