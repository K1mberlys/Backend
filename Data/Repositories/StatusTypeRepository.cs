using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : BaseRepository<StatusTypeEntity>(context), IStatusTypeRepository
{
}
