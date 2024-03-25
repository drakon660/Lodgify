using Ardalis.Result;

namespace ApiApplication.Infrastructure;

public static class ResultBuilder
{
    public static Result<IEnumerable<T>> SuccessOrNotFound<T>(IEnumerable<T> items)
    {
        if (items is not null && !items.Any())
            return Result.NotFound();

        return Result.Success(items);
    }
} 