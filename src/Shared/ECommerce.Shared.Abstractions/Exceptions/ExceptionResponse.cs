using System.Net;

namespace ECommerce.Shared.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);