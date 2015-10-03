using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFifteen.Models.Contracts
{
    public abstract class GridBorderStyle
    {
        public abstract string Top { get; }

        public abstract string Bottom { get; }

        public abstract string Left { get; }

        public abstract string Right { get; }
    }
}
