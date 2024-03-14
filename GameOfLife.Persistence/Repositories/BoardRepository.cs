using GameOfLife.Model.Entities;
using GameOfLife.Model.Repositories;

namespace GameOfLife.Persistence.Repositories
{
    public class BoardRepository : BaseJsonFileRepository<Board, Guid>, IBoardRepository
    {
    }
}
