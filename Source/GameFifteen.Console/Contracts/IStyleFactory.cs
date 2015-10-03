using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.Console.Contracts
{
    public interface IStyleFactory
    {
        IStyle Get(Enum type);
    }
}
