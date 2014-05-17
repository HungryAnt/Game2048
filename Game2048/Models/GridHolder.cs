using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Models
{
    public class GridHolder
    {
        // 在游戏方格中的位置
        public int Row { get; set; }
        public int Col { get; set; }

        /// <summary>
        /// 可以承载一个GridEntity
        /// </summary>
        public GridEntity GridEntity { get; private set; }

        public GridHolder(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// 是否是空的单元格
        /// </summary>
        public bool IsNull
        {
            get { return GridEntity == null; }
        }

        public void SetGridEntity(GridEntity gridEntity)
        {
            GridEntity = gridEntity;

            if (gridEntity != null)
            {
                gridEntity.Owner = this;
            }
        }

        public bool IsTempNull
        {
            get { return TempEntity == null; }
        }

        public GridEntity TempEntity { get; set; }
    }
}
