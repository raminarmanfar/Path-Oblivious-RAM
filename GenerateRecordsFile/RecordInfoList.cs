using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRecordsFile
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
            string Name, Family, Email, City;
            Name = Family = Email = City = string.Empty;
            for(int i = 0; i < 4; i++)
            {
                int rndIndex = rnd.Next(recordsList.Count);
                switch (i)
                {
                    case 0: Name = recordsList[rndIndex].Name; break;
                    case 1: Family = recordsList[rndIndex].Family; break;
                    case 2: Email = recordsList[rndIndex].Email; break;
                    case 3: City = recordsList[rndIndex].City; break;
                }

            }
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
