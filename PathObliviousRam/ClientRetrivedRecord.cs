using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    class ClientRetrivedRecord
    {
        public int LeafNodeIndex { get; private set; }
        public int BlockID { get; private set; }
        public string RecordID { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Email { get; private set; }
        public string City { get; private set; }
        public string Key { get; private set; }
        public ClientRetrivedRecord(int LeafNodeIndex, int BlockID, string RecordID, string Name, string Family, string Email, string City, string Key)
        {
            this.LeafNodeIndex = LeafNodeIndex;
            this.BlockID = BlockID;
            this.RecordID = RecordID;
            this.Name = Name;
            this.Family = Family;
            this.Email = Email;
            this.City = City;
            this.Key = Key;
        }
    }
}
