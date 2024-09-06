using AspProjekat.Application;

namespace AspProjekat.API.Core
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex, IApplicationActor actor);

    }
}
