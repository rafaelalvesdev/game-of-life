using GameOfLife.Model.Entities;

namespace GameOfLife.Model.Services
{
    public interface IBoardStateLogicService
    {
        BoardState CalculateNextState(BoardState board);
    }
}
