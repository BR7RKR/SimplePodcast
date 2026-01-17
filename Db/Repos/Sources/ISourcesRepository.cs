using Db.Models;

namespace Db;

public interface ISourcesRepository : ICrud<int, Source>, IPagination<Source>
{
    
}