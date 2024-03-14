using GameOfLife.Model.Repositories;
using GameOfLife.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection builderServices)
        {
            builderServices.AddSingleton<IBoardRepository, BoardRepository>();
        }
    }
}
