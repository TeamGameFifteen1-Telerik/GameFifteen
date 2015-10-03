using GameFifteen.Console.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen.Console.Styles
{
    public class NormalStyle : GridBorderStyle
    {
        private readonly char symbol = '-';
        private readonly char sideSymbol = '|';
        private readonly int length = 11;

        public override string Top
        {
            get
            {
                return "  " + new string(this.symbol, this.length);
            }
        }

        public override string Bottom
        {
            get
            {
                return "  " + new string(this.symbol, this.length);
            }
        }

        public override string Left
        {
            get
            {
                return this.sideSymbol + " ";
            }
        }

        public override string Right
        {
            get
            {
                return " " + this.sideSymbol;
            }
        }
    }
}
