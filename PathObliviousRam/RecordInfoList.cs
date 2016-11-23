using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    static class RecordInfoList
    {
        static Random rnd;
        static List<RecordInfo> recordsList;
        public static void init()
        {
            rnd = new Random(DateTime.Now.Millisecond);
            recordsList = new List<RecordInfo>();
        }
        public static void setDataList(List<RecordInfo> recordsList)
        {
            RecordInfoList.recordsList = new List<RecordInfo>(recordsList);
        }
        public static List<RecordInfo> getDataList()
        {
            return recordsList;
        }
        public static void addNewRecord(RecordInfo recordInfo)
        {
            recordsList.Add(recordInfo);
        }
        public static RecordInfo getRandomRecord()
        {
            int rndIndex = rnd.Next(ServerManager.NoOfRandRecName);
            string Name = recordsList[rndIndex].Name;

            rndIndex = rnd.Next(ServerManager.NoOfRandRecFamily);
            string Family = recordsList[rndIndex].Family;

            rndIndex = rnd.Next(ServerManager.NoOfRecordsToGenerate);
            string Email = recordsList[rndIndex].Email;

            rndIndex = rnd.Next(ServerManager.NoOfRandRecCity);
            string City = recordsList[rndIndex].City;

            return new RecordInfo(Name, Family, Email, City);
        }
        public static void removeAll()
        {
            recordsList.Clear();
        }
        public static bool removeRecordByIndex(int index)
        {
            if (index >= recordsList.Count) return false;
            recordsList.RemoveAt(index);
            return true;
        }
    }
}
