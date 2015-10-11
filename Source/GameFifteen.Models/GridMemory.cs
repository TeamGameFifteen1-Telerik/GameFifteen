// <copyright file="GridMemory.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    /// <summary>
    /// Holds the memento.
    /// Memento design pattern
    /// The 'Caretaker' class.
    /// </summary>
    public class GridMemory
    {
        /// <summary>
        /// Gets or sets memento.
        /// </summary>
        /// <value>Memento that stores game states when the player saves their game.</value>
        public Memento SavedMemento { get; set; }

        /// <summary>
        /// Gets or sets memento.
        /// </summary>
        /// <value>Memento that stores the current game state.</value>
        public Memento CurrentMemento { get; set; }
    }
}
