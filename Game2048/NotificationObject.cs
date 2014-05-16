using System;
using System.ComponentModel;

namespace Gods.Foundation
{
    public class NotificationObject : INotifyPropertyChanged
    {
        //public bool IsNotificationDisabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            //if (!IsNotificationDisabled)
            //{
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            //}
        }
    }


}
