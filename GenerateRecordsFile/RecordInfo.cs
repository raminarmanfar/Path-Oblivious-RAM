using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRecordsFile
{
    public class RecordInfo
    {
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Email { get; private set; }
        public string City { get; private set; }
        public RecordInfo(string Name, string Family, string Email, string City)
        {
            this.Name = Name;
            this.Family = Family;
            this.Email = Email;
            this.City = City;
        }
    }
}
