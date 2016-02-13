using System;
using System.ComponentModel;

namespace TimeView.data
{
    public class Schedule : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }


        private DateTime day;
        public DateTime Day
        {
            get
            {
                return this.day;
            }
            set
            {
                day = value;
                RaisePropertyChanged("Day");
            }
        }

        private int categoryEntryId;
        public int CategoryEntryId
        {
            get { return this.categoryEntryId; }
            set
            {
                this.categoryEntryId = value;
                RaisePropertyChanged("CategoryEntryId");
            }
        }
        public CategoryEntry CategoryEntry { get; set; }

        public int EmployeeId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}