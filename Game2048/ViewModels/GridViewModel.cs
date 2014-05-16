using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gods.Foundation;

namespace Game2048.ViewModels
{
    public class GridViewModel : NotificationObject
    {
        private object _value;

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        private int _row;

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                RaisePropertyChanged("Row");
            }
        }

        private int _col;

        public int Col
        {
            get { return _col; }
            set
            {
                _col = value;
                RaisePropertyChanged("Col");
            }
        }


    }
}
