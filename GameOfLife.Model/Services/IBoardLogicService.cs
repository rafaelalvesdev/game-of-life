using GameOfLife.Model.Entities;

namespace GameOfLife.Model.Services
{
    public interface IBoardLogicService
    {
        (long StateNumber, BoardState State) GetLastState(Board board);
        Board GenerateBoard(bool[][]? cells);
    }
}
