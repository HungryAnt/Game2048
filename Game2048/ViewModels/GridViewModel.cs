using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2048.Models;
using Gods.Foundation;

namespace Game2048.ViewModels
{
    public class GridViewModel
    {
        public object Value { get; set; }

        public int Level { get; set; }

        public int FromRow { get; set; }

        public int FromCol { get; set; }

        public int ToRow { get; set; }

        public int ToCol { get; set; }

        public GridStates GridStates { get; set; }

        public GridMoveInfo MoveInfo { get; set; }

//        private object _value;
//
//        public object Value
//        {
//            get { return _value; }
//            set
//            {
//                _value = value;
//                RaisePropertyChanged("Value");
//            }
//        }
//
//        private int _row;
//
//        public int Row
//        {
//            get { return _row; }
//            set
//            {
//                _row = value;
//                RaisePropertyChanged("Row");
//            }
//        }
//
//        private int _col;
//
//        public int Col
//        {
//            get { return _col; }
//            set
//            {
//                _col = value;
//                RaisePropertyChanged("Col");
//            }
//        }


    }
}
