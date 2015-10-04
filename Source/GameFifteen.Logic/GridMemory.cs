namespace GameFifteen.Logic
{
    using GameFifteen.Models;

    /// <summary>
    /// Holds the memento.
    /// Memento design pattern
    /// The 'Caretaker' class.
    /// </summary>
    public class GridMemory
    {
        /// <summary>
        /// Gets, sets memento.
        /// </summary>
        /// <value>Memento that stores game states.</value>
        public Memento Memento { get; set; }
    }
}
