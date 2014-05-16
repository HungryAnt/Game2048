using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.ViewModels
{
    class GameViewModel
    {
        public GameViewModel()
        {
        }

        void Init()
        {
            GridViewModel gridViewModel = new GridViewModel()
                {
                    Value = "2048"
                };
        }

        //public void GetGridViewModels
    }
}
