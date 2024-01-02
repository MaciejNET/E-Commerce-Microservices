using ECommerce.Shared.Abstractions.Exceptions;

namespace ECommerce.Shared.Infrastructure.Exceptions;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}