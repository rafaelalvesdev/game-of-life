using FluentAssertions;
using GameOfLife.Application.Services;
using GameOfLife.Model.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameOfLife.Application.Tests
{
    public class BoardStateLogicServiceTest
    {
        private readonly BoardStateLogicService _service;

        public BoardStateLogicServiceTest()
        {
            _service = new BoardStateLogicService(Mock.Of<ILogger<BoardStateLogicService>>());
        }

        [Fact]
        public void CalculateOverpopulationNextState()
        {
            // Arrange 
            var board = new BoardState(new Cell[,]
            {
                {true, true, true, true, true},
                {true, true, true, true, true},
                {true, true, true, true, true},
                {true, true, true, true, true},
                {true, true, true, true, true},
            });

            // Act
            var nextState = _service.CalculateNextState(board);

            // Assert
            nextState.IsAnyAlive.Should().BeFalse();
        }

        [Fact]
        public void CalculateSolitudeNextState()
        {
            // Arrange 
            var board = new BoardState(new Cell[,]
            {
                {false, false, false, false, false},
                {false, false, false, false, false},
                {false, false, true, false, false},
                {false, false, false, false, false},
                {false, false, false, false, false},
            });

            // Act
            var nextState = _service.CalculateNextState(board);

            // Assert
            nextState.IsAnyAlive.Should().BeFalse();
        }

        [Fact]
        public void CalculateNextState()
        {
            // Arrange 
            var board = new BoardState(new Cell[,]
            {
                {false, false, false, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, true, false, false},
                {false, false, false, false, false},
            });

            // Act
            var list = new List<BoardState>();
            for (var i = 0; i < 5; i++)
            {
                var lastState = list.LastOrDefault() ?? board;
                list.Add(_service.CalculateNextState(new BoardState(lastState.Cells)));
            }

            // Assert
            list.Select(x => x.AliveCells).ToList().ForEach(x => x.Should().Be(3));

        }
    }
}
