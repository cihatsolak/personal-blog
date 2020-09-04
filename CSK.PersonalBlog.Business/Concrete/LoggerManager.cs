using CSK.PersonalBlog.Business.Interface;
using CSK.PersonalBlog.DataAccess.Interfaces;
using CSK.PersonalBlog.Entities.Concrete;
using NLog;
using System.Threading.Tasks;

namespace CSK.PersonalBlog.Business.Concrete
{
    public class LoggerManager : GenericManager<Log>, ILoggerService
    {
        public LoggerManager(IGenericDal<Log> genericDal) : base(genericDal)
        {
        }

        public async Task LogError(string message)
        {
            await Task.Run(() =>
            {
                var logger = LogManager.GetLogger("fileLogger");
                logger.Log(LogLevel.Error, message);
            });
        }
    }
}
