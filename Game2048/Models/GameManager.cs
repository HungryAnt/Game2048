using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    class GameManager
    {
        private static readonly GameManager INSTANCE = new GameManager();

        public static GameManager Instance
        {
            get { return INSTANCE; }
        }

        public GameManager()
        {
            GameCore = new GameCore();
        }

        public GameCore GameCore { get; private set; }
    }
}
