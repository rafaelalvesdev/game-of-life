using GameOfLife.Application.Services;
using GameOfLife.Model.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Application;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection builderServices)
    {
        builderServices.AddSingleton<IBoardStateLogicService, BoardStateLogicService>();
        builderServices.AddSingleton<IBoardLogicService, BoardLogicService>();
        builderServices.AddSingleton<IBoardAppService, BoardAppService>();
    }
}