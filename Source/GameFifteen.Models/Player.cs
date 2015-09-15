namespace GameFifteen.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Player : IComparable
    {
        private string name;
        private int moves;

        public Player(string name, int moves)
        {
            this.name = name;
            this.moves = moves;
        }

        public string Name
        {
            get 
            {
                return this.name;
            }
        }

        public int Moves
        {
            get
            {
                return this.moves; 
            }

            set
            {
                this.moves = value;
            }
        }

        public int CompareTo(object player)
        {
            Player currentPlayer = (Player)player;
            int result = this.moves.CompareTo(currentPlayer.Moves);
            return result;
        }
    }
}
