using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFifteen.Console.Contracts;
using GameFifteen.Common;

namespace GameFifteen.Console.Styles
{
    public class AsteriskStyle: GridBorderStyle
    {
        private readonly char symbol = '*';
        private readonly int length = GlobalConstants.GridSize * 4;

        public override string Top
        {
            get
            {
                return new string(this.symbol, length); 
            }
        }

        public override string Bottom
        {
            get
            {
                return new string(this.symbol, length);
            }
        }

        public override string Left
        {
            get
            {
                return this.symbol + " ";
            }
        }

        public override string Right
        {
            get
            {
                return " " + this.symbol; 
            }
        }
    }
}
