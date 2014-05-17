using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public interface IGridActionCommand
    {
        void Do(GameCore gameCore);

        void Undo(GameCore gameCore);
    }
}
