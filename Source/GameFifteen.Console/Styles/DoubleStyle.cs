using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFifteen.Console.Contracts;

namespace GameFifteen.Console.Styles
{
    public class DoubleStyle: GridBorderStyle
    {
        private readonly char symbol = '║';
        private readonly char line = '═';
        private readonly int length = 14;

        public override string Top
        {
            get
            {
                return "╔" + new string(this.line, this.length) + "╗";
            }
        }

        public override string Bottom
        {
            get
            {
                return "╚" + new string(this.line, this.length) + "╝";
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
