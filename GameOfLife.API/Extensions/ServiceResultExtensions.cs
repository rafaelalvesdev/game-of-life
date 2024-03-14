using GameOfLife.Common.ServiceModels;

namespace GameOfLife.API.Extensions
{
    public static class ServiceResultExtensions
    {
        public static IResult AsResult(this ServiceResult serviceResult)
        {
            if (serviceResult == null)
                return Results.NoContent();

            if (serviceResult.StatusCode.HasValue)
            {
                return serviceResult.StatusCode switch
                {
                    400 => Results.BadRequest(serviceResult),
                    404 => Results.NotFound(serviceResult),
                    _ => Results.Ok(serviceResult),
                };
            }

            return serviceResult.IsValid
                ? Results.Ok(serviceResult)
                : Results.BadRequest(serviceResult);
        }
    }
}
