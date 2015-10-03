using GameFifteen.Models;
using GameFifteen.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen.Logic.Contracts
{
    public interface IGameInitializater
    {
        void Initialize(IGrid grid);
    }
}
