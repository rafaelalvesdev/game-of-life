using GameOfLife.Common.Extensions;
using GameOfLife.Model.Entities;

namespace GameOfLife.Model.DTOs;

public class BoardResponseDto
{
    public Guid Id { get; set; }

    public long? StateNumber { get; set; }

    public bool[][]? Cells { get; set; }

    public BoardResponseDto()
    { }

    public BoardResponseDto(Guid id, long stateNumber, bool[][] cells)
    {
        Id = id;
        StateNumber = stateNumber;
        Cells = cells;
    }

    public BoardResponseDto(Guid id, long stateNumber, Cell[,] cells)
        : this(id, stateNumber, cells.ToArrayOfArray().Select(x => x.Select(c => (bool)c).ToArray()).ToArray())
    { }
}