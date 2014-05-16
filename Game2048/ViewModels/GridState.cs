using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.ViewModels
{
    public enum GridState
    {
        /// <summary>
        /// 新增的
        /// </summary>
        NewCreated,

        /// <summary>
        /// 原有的
        /// </summary>
        Original,

        /// <summary>
        /// 有移动的
        /// </summary>
        Moved,
    }
}
