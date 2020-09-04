using CSK.PersonalBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Interface
{
    public interface ILoggerService : IGenericService<Log>
    {
        //NLog Insert
        Task LogError(string message);
    }
}
