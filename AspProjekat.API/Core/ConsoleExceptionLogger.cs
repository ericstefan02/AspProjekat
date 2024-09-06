using AspProjekat.Application;
using AspProjekat.DataAccess;
using AspProjekat.Domain;

namespace AspProjekat.API.Core
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex, IApplicationActor actor)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(ex.Message + " ID: " + id);

            return id;
        }
    }

    public class DbExceptionLogger : IExceptionLogger
    {
        private readonly AspContext _aspContext;

        public DbExceptionLogger(AspContext aspContext)
        {
            _aspContext = aspContext;
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();

            ErrorLog log = new()
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };



            _aspContext.ErrorLogs.Add(log);

            _aspContext.SaveChanges();

            return id;
        }
    }
}
