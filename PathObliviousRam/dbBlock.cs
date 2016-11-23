using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    public class dbBlock
    {
        public int BlockID { get; private set; }
        List<dbRecord> Records;
        public dbBlock()//--- not for join to the tree. just temporary use
        {
            BlockID = -1;
            Records = new List<dbRecord>(ServerManager.NoOfRecordsPerBlock);
            String Dummy = ManageSecurity.Encrypt("Dummy");
            for (int i = 0; i < ServerManager.NoOfRecordsPerBlock; i++)
            {
                Records.Add(new dbRecord(Dummy, ManageSecurity.Encrypt(BigInteger.Zero), Dummy, Dummy, Dummy, Dummy));
            }
        }
        public dbBlock(int BlockID)
        {
            this.BlockID = BlockID;
            Records = new List<dbRecord>(ServerManager.NoOfRecordsPerBlock);
            string Dummy = ManageSecurity.Encrypt("Dummy");
            for (int i = 0; i < ServerManager.NoOfRecordsPerBlock; i++)
            {
                if (ClientManager.RecordIdToBlockMapList.Count < ServerManager.NoOfRecordsToGenerate)
                {
                    string EncryptedRecID = ManageSecurity.Encrypt(ClientManager.RecordIdToBlockMapList.Count.ToString());
                    RecordInfo rndInfo = RecordInfoList.getRandomRecord();
                    string nameEncrypted = ManageSecurity.Encrypt(rndInfo.Name);
                    string familyEncrypted = ManageSecurity.Encrypt(rndInfo.Family);
                    string emailEncrypted = ManageSecurity.Encrypt(rndInfo.Email);
                    string cityEncrypted = ManageSecurity.Encrypt(rndInfo.City);

                    Records.Add(new dbRecord(EncryptedRecID, nameEncrypted, familyEncrypted, emailEncrypted, cityEncrypted));
                    ClientManager.RecordIdToBlockMapList.Add(ClientManager.RecordIdToBlockMapList.Count.ToString(), BlockID);

                    ClientManager.NameToBlockMapList.addNewBlockIDByFieldValue(rndInfo.Name, BlockID);
                    ClientManager.FamilyToBlockMapList.addNewBlockIDByFieldValue(rndInfo.Family, BlockID);
                    ClientManager.CityToBlockMapList.addNewBlockIDByFieldValue(rndInfo.City, BlockID);
                    ClientManager.VacantRecordsList[BlockID]--;
                }
                else
                {
                    Records.Add(new dbRecord(Dummy, ManageSecurity.Encrypt(BigInteger.Zero), Dummy, Dummy, Dummy, Dummy));
                }
            }
        }
        public dbRecord GetRecord(int RecordIndex)
        {
            return Records[RecordIndex];
        }
        public List<dbRecord> ReadWholeRecords()
        {
            return Records;
        }
        public int GetRecordIndexByEncryptedRecordID(string EncryptedRecordID)
        {
            for (int i = 0; i < ServerManager.NoOfRecordsPerBlock; i++)
            {
                if (GetRecord(i).EncryptedRecordID == EncryptedRecordID) return i;
            }
            return -1;
        }
        public List<dbRecord> GetRecordsByFieldName(string FieldName, string EncryptedFieldValue)
        {
            List<dbRecord> recordList = new List<dbRecord>();
            for (int i = 0; i < ServerManager.NoOfRecordsPerBlock; i++)
            {
                switch (FieldName)
                {
                    case "RECORDID":
                        if (GetRecord(i).EncryptedRecordID == EncryptedFieldValue) recordList.Add(Records[i]);
                        break;
                    case "NAME":
                        if (GetRecord(i).EncryptedName == EncryptedFieldValue) recordList.Add(Records[i]);
                        break;
                    case "FAMILY":
                        if (GetRecord(i).EncryptedFamily == EncryptedFieldValue) recordList.Add(Records[i]);
                        break;
                    case "EMAIL":
                        if (GetRecord(i).EncryptedEmail == EncryptedFieldValue) recordList.Add(Records[i]);
                        break;
                    case "CITY":
                        if (GetRecord(i).EncryptedCity == EncryptedFieldValue) recordList.Add(Records[i]);
                        break;
                }
            }
            return recordList;
        }
        public dbBlock CopyBlockContents()
        {
            dbBlock DestBlock = new dbBlock();
            DestBlock.BlockID = BlockID;
            for (int i = 0; i < ServerManager.NoOfRecordsPerBlock; i++)
            {
                DestBlock.Records[i].SetRecordInfo(GetRecord(i).EncryptedRecordID, GetRecord(i).EncryptedKey, GetRecord(i).EncryptedName, GetRecord(i).EncryptedFamily, GetRecord(i).EncryptedEmail, GetRecord(i).EncryptedCity);
            }
            return DestBlock;
        }
    }
}
