using GameFifteen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.Logic.Contracts
{
    public interface IGameInitializater
    {
        void Initialize(Grid grid);
    }
}
