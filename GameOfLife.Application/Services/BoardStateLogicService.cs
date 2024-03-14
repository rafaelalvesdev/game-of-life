using GameOfLife.Model.Entities;
using GameOfLife.Model.Services;
using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.Services;

public class BoardStateLogicService : BaseService, IBoardStateLogicService
{
    public BoardStateLogicService(ILogger<BoardStateLogicService> logger)
        : base(logger)
    {
    }

    public BoardState CalculateNextState(BoardState boardState)
    {
        if (!boardState.IsAnyAlive)
            return boardState;

        var cells = boardState.Cells;
        var columns = cells.GetLength(0);
        var rows = cells.GetLength(1);
        var newStateCells = new Cell[columns, rows];

        for (var x = 0; x < columns; x++)
            for (var y = 0; y < rows; y++)
            {
                var currentCellState = cells[x, y];
                var xLeft = (x > 0) ? x - 1 : columns - 1;
                var xRight = (x < columns - 1) ? x + 1 : 0;
                var yUp = (y > 0) ? y - 1 : rows - 1;
                var yD = (y < rows - 1) ? y + 1 : 0;

                var neighbors = new List<Cell>
                {
                    cells[xLeft, yUp],
                    cells[x, yUp],
                    cells[xRight, yUp],
                    cells[xLeft, y],
                    cells[xRight, y],
                    cells[xLeft, yD],
                    cells[x, yD],
                    cells[xRight, yD]
                };

                var neighborsAlive = neighbors.Count(n => n.IsAlive);

                newStateCells[x, y] = new Cell((neighborsAlive == 2 && currentCellState.IsAlive) || neighborsAlive == 3);
            }

        return new BoardState(newStateCells);
    }
}