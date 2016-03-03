using System;
using System.ComponentModel;

namespace TimeView.data
{
    public class Schedule : INotifyPropertyChanged
    {
        private CategoryEntry categoryentry;

        private int categoryEntryId;


        private DateTime day;
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public DateTime Day
        {
            get { return day; }
            set
            {
                day = value;
                RaisePropertyChanged("Day");
            }
        }

        public int CategoryEntryId
        {
            get { return categoryEntryId; }
            set
            {
                categoryEntryId = value;
                RaisePropertyChanged("CategoryEntryId");
            }
        }

        public CategoryEntry CategoryEntry
        {
            get { return categoryentry; }
            set
            {
                categoryentry = value;
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
            var test = obj as Schedule;
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