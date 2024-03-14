using System.ComponentModel;

namespace GameOfLife.Model.Entities
{
    public enum BoardStatus
    {
        [Description("Initial state.")]
        Initial,

        [Description("Board cells are evolving.")]
        Progressing,

        [Description("Finished. No more states to progress.")]
        Finished,
    }
}
