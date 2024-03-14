namespace GameOfLife.Model.Entities;

/// <summary>
/// Current state of a cell.
/// </summary>
public class Cell
{
    /// <summary>
    /// Marks if cell is dead or alive.
    /// </summary>
    public bool IsAlive { get; set; }

    public Cell(bool isAlive)
    {
        IsAlive = isAlive;
    }

    public static implicit operator bool(Cell c) => c.IsAlive;
    public static implicit operator Cell(bool b) => new(b);
}