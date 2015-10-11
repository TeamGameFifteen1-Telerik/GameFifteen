// <copyright file="Memento.cs" company="Telerik Academy">All rights reserved.</copyright>
// <author>Team GameFifteen-1</author>
namespace GameFifteen.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Memento design pattern
    /// The 'Memento' class.
    /// </summary>
    public class Memento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Memento" /> class.
        /// </summary>
        /// <param name="tiles">List of tiles to be saved in the memento.</param>
        public Memento(List<Tile> tiles)
        {
            this.Tiles = tiles;
        }

        /// <summary>
        /// Gets or sets a list of tiles saved in the memento.
        /// </summary>
        /// <value>The Tiles property gets or sets a list of tiles saved in the memento.</value>
        public List<Tile> Tiles { get; set; }
    }
}
