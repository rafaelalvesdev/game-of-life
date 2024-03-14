using System.Collections.Concurrent;

namespace GameOfLife.Model.Entities;

/// <summary>
/// Board of Game of Life with initial state and allocation for future states.
/// </summary>
public class Board
{
    public Guid Id { get; set; }

    /// <summary>
    /// Origination state.
    /// </summary>
    public BoardState InitialBoardMatrix { get; set; }

    /// <summary>
    /// List of calculated states.
    /// </summary>
    public ConcurrentDictionary<long, BoardState> FutureStateList { get; set; }

    /// <summary>
    /// Current status.
    /// </summary>
    public BoardStatus Status { get; set; }

    /// <summary>
    /// Board size.
    /// </summary>
    public (int, int) Size { get; set; }

    public Board()
    {
        InitialBoardMatrix = new BoardState();
        FutureStateList = new ConcurrentDictionary<long, BoardState>();
        Status = BoardStatus.Initial;
    }

    public Board(Cell[,] boardMatrix)
        : this()
    {
        InitialBoardMatrix = new BoardState(boardMatrix);
        Size = (boardMatrix.GetLength(0), boardMatrix.GetLength(1));
    }

    public Board(Guid id, Cell[,] boardMatrix)
        : this(boardMatrix)
    {
        Id = id;
    }
}