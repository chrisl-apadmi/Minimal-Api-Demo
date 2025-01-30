using System.Runtime.Serialization;
using MinimalApi.Clean.Application.Common.Entity;

namespace MinimalApi.Clean.Application.Common.Exception;

[Serializable]
public class NotFoundException: System.Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, System.Exception innerException) :
        base(message, innerException)
    {
    }

    public static void ThrowIfNull(object? argument, EntityType entity)
    {
        if (argument is null)
        {
            throw new NotFoundException($"Supplied Id does not have a corresponding {entity} ");
        }
    }
}