using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.ViewModels
{
    [Flags]
    public enum GridStates
    {
        Null = 0,

        /// <summary>
        /// 新增的
        /// </summary>
        NewCreated = 1,

        /// <summary>
        /// 原有的
        /// </summary>
        Original = 2,

        /// <summary>
        /// 有移动的
        /// </summary>
        Moved = 4,
    }
}
