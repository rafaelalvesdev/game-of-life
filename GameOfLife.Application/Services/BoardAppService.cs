using GameOfLife.Common.ServiceModels;
using GameOfLife.Model.Configuration;
using GameOfLife.Model.DTOs;
using GameOfLife.Model.Entities;
using GameOfLife.Model.Repositories;
using GameOfLife.Model.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameOfLife.Application.Services;

public class BoardAppService : BaseService, IBoardAppService
{
    private readonly IBoardLogicService _boardLogicService;
    private readonly IBoardStateLogicService _boardStateLogicService;
    private readonly IBoardRepository _boardRepository;
    private readonly GameSettings _settings;

    public BoardAppService(
        ILogger<BoardAppService> logger,
        IBoardLogicService boardLogicService,
        IBoardStateLogicService boardStateLogicService,
        IBoardRepository boardRepository,
        IOptions<GameSettings> settings)
        : base(logger)
    {
        _boardLogicService = boardLogicService;
        _boardStateLogicService = boardStateLogicService;
        _boardRepository = boardRepository;
        _settings = settings.Value;
    }

    public async Task<ServiceResult<BoardResponseDto>> CreateBoardAsync(BoardRequestDto boardRequestDto)
    {
        var board = _boardLogicService.GenerateBoard(boardRequestDto.Cells);
        board = await _boardRepository.SaveAsync(board.Id, board);

        return new ServiceResult<BoardResponseDto>(
            new BoardResponseDto() { Id = board.Id });
    }

    public async Task<ServiceResult<BoardResponseDto>> GetNextBoardStateAsync(Guid id)
    {
        var board = await _boardRepository.GetAsync(id);

        if (board is null)
            return new ServiceResult<BoardResponseDto>(null, 404, "Board not found.");

        if (BoardStatus.Finished.Equals(board.Status))
            return new ServiceResult<BoardResponseDto>(
                new BoardResponseDto(board.Id, board.FutureStateList.LastOrDefault().Key,
                    board.FutureStateList.LastOrDefault().Value.Cells),
                200, "Board is in finished status.");

        var lastState = _boardLogicService.GetLastState(board);

        var nextStateNumber = lastState.StateNumber + 1;
        var nextState = board.FutureStateList.GetOrAdd(nextStateNumber,
            _boardStateLogicService.CalculateNextState(lastState.State));

        board.Status = nextState.IsAnyAlive ? BoardStatus.Progressing : BoardStatus.Finished;
        board = await _boardRepository.SaveAsync(board.Id, board);

        return new ServiceResult<BoardResponseDto>(
            new BoardResponseDto(board.Id, nextStateNumber, nextState.Cells));
    }

    public async Task<ServiceResult<BoardResponseDto>> GetBoardStateAsync(Guid id, long stateNumber)
    {
        var board = await _boardRepository.GetAsync(id);

        if (board is null)
            return new ServiceResult<BoardResponseDto>(null, 404, "Board not found.");

        if (BoardStatus.Finished.Equals(board.Status))
            return new ServiceResult<BoardResponseDto>(
                new BoardResponseDto(board.Id, board.FutureStateList.LastOrDefault().Key,
                    board.FutureStateList.LastOrDefault().Value.Cells),
                200, "Board is in finished status.");

        if (stateNumber == 0)
            return new ServiceResult<BoardResponseDto>(
                new BoardResponseDto(board.Id, stateNumber, board.InitialBoardMatrix.Cells));

        if (board.FutureStateList.TryGetValue(stateNumber, out var value))
            return new ServiceResult<BoardResponseDto>(
                new BoardResponseDto(board.Id, stateNumber, value.Cells));

        var lastState = _boardLogicService.GetLastState(board);
        while (lastState.StateNumber < stateNumber)
        {
            var nextStateNumber = lastState.StateNumber + 1;
            var nextState = board.FutureStateList.GetOrAdd(nextStateNumber,
                _boardStateLogicService.CalculateNextState(lastState.State));
            lastState = (nextStateNumber, nextState);

            if (!lastState.State.IsAnyAlive)
                break;
        }

        board.Status = lastState.State.IsAnyAlive ? BoardStatus.Progressing : BoardStatus.Finished;
        board = await _boardRepository.SaveAsync(board.Id, board);

        return new ServiceResult<BoardResponseDto>(
            new BoardResponseDto(board.Id, lastState.StateNumber, lastState.State.Cells));
    }

    public async Task<ServiceResult<BoardResponseDto>> GetBoardFinalStateAsync(Guid id)
    {
        var board = await _boardRepository.GetAsync(id);

        if (board is null)
            return new ServiceResult<BoardResponseDto>(null, 404, "Board not found.");

        if (BoardStatus.Finished.Equals(board.Status))
            return new ServiceResult<BoardResponseDto>(
                new BoardResponseDto(board.Id, board.FutureStateList.LastOrDefault().Key,
                    board.FutureStateList.LastOrDefault().Value.Cells));

        var lastState = _boardLogicService.GetLastState(board);
        var statesCalculated = 0;
        while (statesCalculated++ < _settings.MaxStatesToConclusion)
        {
            var nextStateNumber = lastState.StateNumber + 1;
            var nextState = board.FutureStateList.GetOrAdd(nextStateNumber,
                _boardStateLogicService.CalculateNextState(lastState.State));
            lastState = (nextStateNumber, nextState);

            if (!lastState.State.IsAnyAlive)
                break;
        }

        board.Status = lastState.State.IsAnyAlive ? BoardStatus.Progressing : BoardStatus.Finished;
        board = await _boardRepository.SaveAsync(board.Id, board);

        return new ServiceResult<BoardResponseDto>(
            new BoardResponseDto(board.Id, lastState.StateNumber, lastState.State.Cells));
    }
}