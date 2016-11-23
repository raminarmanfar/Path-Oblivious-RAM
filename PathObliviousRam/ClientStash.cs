using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    static class ClientStash
    {
        static List<dbBlock> Blocks = new List<dbBlock>();
        public static void UpdateRecordInformation(int BlockID, int RecordIndex, string RecordID, BigInteger Key, RecordInfo recordInfo)
        {
            int BlockIndex = GetBlockIndexByID(BlockID);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedRecordID = ManageSecurity.Encrypt(RecordID);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedKey = ManageSecurity.Encrypt(Key);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedName = ManageSecurity.Encrypt(recordInfo.Name);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedFamily = ManageSecurity.Encrypt(recordInfo.Family);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedEmail = ManageSecurity.Encrypt(recordInfo.Email);
            Blocks[BlockIndex].GetRecord(RecordIndex).EncryptedCity = ManageSecurity.Encrypt(recordInfo.City);
        }
        public static void RemoveBlock(int BlockIndex)
        {
            Blocks.RemoveAt(BlockIndex);
        }
        public static void AddBlockSorted(dbBlock BlockToAdd)
        {
            int BlockID = BlockToAdd.BlockID;
            int left = 0, right = Blocks.Count - 1;

            int mid = 0;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (Blocks[mid].BlockID > BlockID) right = mid - 1;
                else if (Blocks[mid].BlockID < BlockID) left = mid + 1;
                else if (Blocks[mid].BlockID == BlockID) break;
            }
            if (Blocks.Count == 0) mid = 0;
            else if (Blocks[mid].BlockID < BlockID) mid++;
            Blocks.Insert(mid, BlockToAdd);
        }
        public static void AddBucketContentsSorted(List<dbBucket> BucketsToAdd)
        {
            for (int i = 0; i < BucketsToAdd.Count; i++)
            {
                for (int j = 0; j < ClientManager.NoOfBlocksPerBucket; j++)
                {
                    dbBlock BlockToAdd = BucketsToAdd[i].GetBlock(j);
                    int BlockID = BlockToAdd.BlockID;

                    int left = 0, right = Blocks.Count - 1, mid = 0;
                    while (left <= right)
                    {
                        mid = (left + right) / 2;
                        if (Blocks[mid].BlockID > BlockID) right = mid - 1;
                        else if (Blocks[mid].BlockID < BlockID) left = mid + 1;
                        else if (Blocks[mid].BlockID == BlockID) break;
                    }
                    if (Blocks.Count == 0) mid = 0;
                    else if (Blocks[mid].BlockID < BlockID) mid++;
                    Blocks.Insert(mid, BlockToAdd);
                }
            }
        }
        public static dbBlock GetBlock(int BlockIndex)
        {
            return Blocks[BlockIndex];
        }
        public static int GetBlockIndexByID(int BlockID)//--- test it by several record fetch
        {
            int left = 0, right = Blocks.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (Blocks[mid].BlockID > BlockID) right = mid - 1;
                else if (Blocks[mid].BlockID < BlockID) left = mid + 1;
                else if (Blocks[mid].BlockID == BlockID) return mid;
            }
            return -1;
        }
        public static dbBlock GetBlockByID(int BlockID)//--- test it by several record fetch
        {
            int left = 0, right = Blocks.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (Blocks[mid].BlockID > BlockID) right = mid - 1;
                else if (Blocks[mid].BlockID < BlockID) left = mid + 1;
                else if (Blocks[mid].BlockID == BlockID) return Blocks[mid];
            }
            return null;
        }
        public static List<dbBlock> GetAllBlocks()
        {
            return Blocks;
        }
        public static int GetBlocksCount()
        {
            return Blocks.Count;
        }
    }
}
