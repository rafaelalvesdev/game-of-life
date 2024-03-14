using GameOfLife.Model.Entities;

namespace GameOfLife.Model.Repositories
{
    public interface IBoardRepository
    {
        Task<Board> SaveAsync(Guid id, Board generateBoard);
        Task<Board> GetAsync(Guid id);
    }
}
