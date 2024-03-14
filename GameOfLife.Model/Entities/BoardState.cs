using GameOfLife.Common.Extensions;
namespace GameOfLife.Model.Entities
{
    /// <summary>
    /// Single state of a board.
    /// </summary>
    public class BoardState
    {
        public Cell[,] Cells { get; set; }

        public long AliveCells { get; set; }

        public bool IsAnyAlive => AliveCells > 0;

        public BoardState()
        {
            Cells = new Cell[0, 0];
        }

        public BoardState(Cell[,] cells)
            : this()
        {
            Cells = cells;
            AliveCells = cells.Flatten().Count(x => x.IsAlive);
        }
    }
}
