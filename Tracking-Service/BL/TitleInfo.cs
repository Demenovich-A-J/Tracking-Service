using System;

namespace BL
{
    public class TitleInfo
    {
        private  readonly string _lastName;
        private readonly DateTime _date;

        public TitleInfo(string lastName, DateTime date)
        {
            _lastName = lastName;
            _date = date;
        }

        public string LastName => _lastName;

        public DateTime Date => _date;
    }
}