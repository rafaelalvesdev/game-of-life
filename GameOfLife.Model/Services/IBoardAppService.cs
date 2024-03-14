using GameOfLife.Common.ServiceModels;
using GameOfLife.Model.DTOs;

namespace GameOfLife.Model.Services
{
    public interface IBoardAppService
    {
        Task<ServiceResult<BoardResponseDto>> CreateBoardAsync(BoardRequestDto boardRequestDto);
        Task<ServiceResult<BoardResponseDto>> GetNextBoardStateAsync(Guid id);
        Task<ServiceResult<BoardResponseDto>> GetBoardStateAsync(Guid id, long stateNumber);
        Task<ServiceResult<BoardResponseDto>> GetBoardFinalStateAsync(Guid id);
    }
}
