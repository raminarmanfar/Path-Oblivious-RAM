using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateRecordsFile
{
    class Program
    {
        private static List<RecordInfo> generateStringCombination(int RecordsCount)
        {
            int strLen = 0;
            List<RecordInfo> lstResult = new List<RecordInfo>();
            while (Math.Pow(26, strLen) < RecordsCount) strLen++;
            StringBuilder str = new StringBuilder(string.Empty);
            str.Append('A', strLen);
            int i = strLen - 1;
            while (true)
            {
                if (i == strLen - 1)
                {
                    while (str[i] < 'Z' && lstResult.Count < RecordsCount)
                    {
                        lstResult.Add(new RecordInfo("N" + str, "F" + str, "E" + str, "C" + str));
                        str[i]++;
                    }
                    while (i >= 0 && str[i] < 'Z') i++;
                    if (i < 0) break;
                    else
                    {
                        str[i]++;
                        while (i < strLen - 1) str[++i] = 'A';
                    }
                }
            }
            return lstResult;
        }
        static void Main(string[] args)
        {
            Console.Write("Enter file name: ");
            string fileName = Console.ReadLine();
            Console.Write("Enter number of records to generate: ");
            int recordsCount = Convert.ToInt16(Console.ReadLine());
            RecordInfoList.setDataList(generateStringCombination(recordsCount));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (RecordInfo record in RecordInfoList.getDataList())
                {
                    string recordStr = record.Name + "," + record.Family + "," + record.Email + "," + record.City + ",";
                    file.WriteLine(recordStr);
                }
            }
        }
    }
}