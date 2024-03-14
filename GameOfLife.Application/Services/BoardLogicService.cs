using GameOfLife.Common.Extensions;
using GameOfLife.Model.Entities;
using GameOfLife.Model.Services;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.Services
{
    public class BoardLogicService : BaseService, IBoardLogicService
    {
        public BoardLogicService(ILogger<BoardLogicService> logger)
            : base(logger)
        {
        }

        public (long StateNumber, BoardState State) GetLastState(Board board)
        {
            var lastState = board.FutureStateList.LastOrDefault();
            if (board.FutureStateList.IsEmpty)
                lastState = new KeyValuePair<long, BoardState>(0, board.InitialBoardMatrix);

            return (lastState.Key, lastState.Value);
        }

        public Board GenerateBoard(bool[][]? cells)
        {
            if (cells is null) throw new ArgumentNullException(nameof(cells));

            return new Board(
                Guid.NewGuid(),
                cells.Select(x => x.Select(b => (Cell)b).ToArray()).ToArray().ToMatrix());
        }
    }
}