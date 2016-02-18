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
            get
            {
                return this.categoryEntryId;
            }
            set
            {
                this.categoryEntryId = value;
                RaisePropertyChanged("CategoryEntryId");
            }
        }
        private CategoryEntry categoryentry;
        public CategoryEntry CategoryEntry
        {
            get
            {
                return this.categoryentry;
            }
            set
            {
                this.categoryentry = value;
                RaisePropertyChanged("CategoryEntry");
            }
        }

        public int EmployeeId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override bool Equals(object obj)
        {
            Schedule test = obj as Schedule;
            if (test == null)
            {
                return false;
            }
            return Id == test.Id &&
                Day.Date == test.Day.Date &&
                CategoryEntryId == test.CategoryEntryId &&
                EmployeeId == test.EmployeeId;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}