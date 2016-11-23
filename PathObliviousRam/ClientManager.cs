using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    public enum Operation { ReadData, WriteData, OmitData }
    public static class ClientManager
    {
        public static int TreeHeight { get; private set; }
        public static int NoOfChildsPerNode { get; private set; }
        public static int NoOfBlocksPerBucket { get; private set; }
        public static int NoOfRecordsPerBlock { get; private set; }
        public static List<int> VacantRecordsList;// <BlockID, # of vacant records in corresponding block>

        public static List<int> BlockToBuketPositionMapList;
        public static Dictionary<string, int> RecordIdToBlockMapList;

        public static ClientPositionMapList NameToBlockMapList;
        public static ClientPositionMapList FamilyToBlockMapList;
        public static ClientPositionMapList CityToBlockMapList;

        public static dbBlock RequestedDataBlock;
        static Random rnd;
        public static bool ChangeKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey oldKey, TKey newKey)
        {
            TValue value;
            if (!dict.TryGetValue(oldKey, out value)) return false;

            dict.Remove(oldKey);  // do not change order
            dict[newKey] = value;  // or dict.Add(newKey, value) depending on ur comfort
            return true;
        }
        public static dbRecord GetRequestedRecord(string RecordID)
        {
            string EncryptedRecordID = ManageSecurity.Encrypt(RecordID);
            int RequestedRecordIndex = RequestedDataBlock.GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
            return RequestedDataBlock.GetRecord(RequestedRecordIndex);
        }
        public static List<dbRecord> GetRequestedRecordsByField(string FieldName, string FieldValue)
        {
            string EncryptedFieldValue = ManageSecurity.Encrypt(FieldValue);
            List<dbRecord> RequestedRecordsList = RequestedDataBlock.GetRecordsByFieldName(FieldName, EncryptedFieldValue);
            return RequestedRecordsList;
        }
        public static void Initialize(int TreeHeight, int NoOfChildsPerNode, int NoOfBlocksPerBucket, int NoOfRecordsPerBlock)
        {
            BlockToBuketPositionMapList = new List<int>();
            RecordIdToBlockMapList = new Dictionary<string, int>();

            NameToBlockMapList = new ClientPositionMapList("Name");
            FamilyToBlockMapList = new ClientPositionMapList("Family");
            CityToBlockMapList = new ClientPositionMapList("City");

            VacantRecordsList = new List<int>();

            rnd = new Random(DateTime.Now.Millisecond);
            RequestedDataBlock = new dbBlock();
            ClientManager.TreeHeight = TreeHeight;
            ClientManager.NoOfChildsPerNode = NoOfChildsPerNode;

            ClientManager.NoOfBlocksPerBucket = NoOfBlocksPerBucket;
            ClientManager.NoOfRecordsPerBlock = NoOfRecordsPerBlock;
        }
        public static int FindVacantBlockID()
        {
            int VacantBlockID = -1;
            for (int i = 1; i <= ClientManager.NoOfRecordsPerBlock; i++)
            {
                VacantBlockID = VacantRecordsList.IndexOf(i);
                if (VacantBlockID > -1) break;
            }
            return VacantBlockID;
        }
        public static int AccessGet(int BlockID)
        {
            //--- remap block randomly ---
            int PreviousLeafNodeIndex = BlockToBuketPositionMapList[BlockID];
            do
            {
                BlockToBuketPositionMapList[BlockID] = rnd.Next((int)ManagerCalculation.GetTreeLeafNodeCounts(TreeHeight, NoOfChildsPerNode));
            } while (BlockToBuketPositionMapList[BlockID] == PreviousLeafNodeIndex);

            List<dbBucket> buckets = ServerManager.GetAllBucketsByLeafNodeIndex(PreviousLeafNodeIndex);
            ClientStash.AddBucketContentsSorted(buckets);

            int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(BlockID);
            RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
            return PreviousLeafNodeIndex;
        }
        public static void AccessPut(Operation operation, int BlockID, string RecordID, string ModifiedRecordID, int PreviousLeafNodeIndex, BigInteger Key, RecordInfo recordInfo)
        {
            //--- update block => executes only if operation is write ---
            string EncryptedRecId = ManageSecurity.Encrypt(RecordID);
            int RequestedRecordIndex = RequestedDataBlock.GetRecordIndexByEncryptedRecordID(EncryptedRecId);
            switch (operation)
            {
                case Operation.WriteData:
                    ClientStash.UpdateRecordInformation(BlockID, RequestedRecordIndex, ModifiedRecordID , Key, recordInfo);
                    break;
                case Operation.OmitData:
                    ClientStash.UpdateRecordInformation(BlockID, RequestedRecordIndex, "Dummy", BigInteger.Zero, new RecordInfo("Dummy", "Dummy", "Dummy", "Dummy"));
                    break;
            }

            //--- Rewrite back blocks from stash to the database server ---
            //BlockToBuketPositionMapList[BlockID] = 9;//--- temporary---------------------------------------
            dbBucket bucket = new dbBucket();
            for (int l = TreeHeight; l >= 0; l--)
            {
                //--- write other blocks by random except block a here
                for (int j = 0; j < NoOfBlocksPerBucket && ClientStash.GetBlocksCount() > 0; j++)
                {
                    int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(BlockID);
                    int SelectedBlockIndex;
                    bool IsPathSimilar = false;
                    if (BlockToBuketPositionMapList[BlockID] > -1)
                        IsPathSimilar = ServerManager.LeafsAresInSimilarPath(PreviousLeafNodeIndex, BlockToBuketPositionMapList[BlockID], l);
                    if (IsPathSimilar)
                    {
                        SelectedBlockIndex = rnd.Next(ClientStash.GetBlocksCount());
                    }
                    else
                    {
                        do
                        {
                            SelectedBlockIndex = rnd.Next(ClientStash.GetBlocksCount());
                        } while (SelectedBlockIndex == RequestedBlockIndexInStash);
                    }

                    dbBlock block = ClientStash.GetBlock(SelectedBlockIndex);
                    bucket.WriteBlockToBucket(block, j);
                    ClientStash.RemoveBlock(SelectedBlockIndex);
                    if (SelectedBlockIndex != RequestedBlockIndexInStash)
                        BlockToBuketPositionMapList[bucket.GetBlock(j).BlockID] = PreviousLeafNodeIndex;
                }
                ServerManager.SetBucket(PreviousLeafNodeIndex, l, bucket);
            }
            //--- set bukets position map id = -1 because there is no free space in the tree to store them ---
            int RemainedBlocksCount = ClientStash.GetBlocksCount();
            for (int i = 0; i < RemainedBlocksCount; i++)
            {
                BlockToBuketPositionMapList[ClientStash.GetBlock(i).BlockID] = -1;
            }
        }
        public static void AccessPut(int BlockID, int PreviousLeafNodeIndex)
        {
            //--- Rewrite back blocks from stash to the database server ---
            //BlockToBuketPositionMapList[BlockID] = 9;//--- temporary---------------------------------------
            dbBucket bucket = new dbBucket();
            for (int l = TreeHeight; l >= 0; l--)
            {
                //--- write other blocks by random except block a here
                for (int j = 0; j < NoOfBlocksPerBucket && ClientStash.GetBlocksCount() > 0; j++)
                {
                    int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(BlockID);
                    int SelectedBlockIndex;
                    bool IsPathSimilar = false;
                    if (BlockToBuketPositionMapList[BlockID] > -1)
                        IsPathSimilar = ServerManager.LeafsAresInSimilarPath(PreviousLeafNodeIndex, BlockToBuketPositionMapList[BlockID], l);
                    if (IsPathSimilar)
                    {
                        SelectedBlockIndex = rnd.Next(ClientStash.GetBlocksCount());
                    }
                    else
                    {
                        do
                        {
                            SelectedBlockIndex = rnd.Next(ClientStash.GetBlocksCount());
                        } while (SelectedBlockIndex == RequestedBlockIndexInStash);
                    }

                    dbBlock block = ClientStash.GetBlock(SelectedBlockIndex);
                    bucket.WriteBlockToBucket(block, j);
                    ClientStash.RemoveBlock(SelectedBlockIndex);
                    if (SelectedBlockIndex != RequestedBlockIndexInStash)
                        BlockToBuketPositionMapList[bucket.GetBlock(j).BlockID] = PreviousLeafNodeIndex;
                }
                ServerManager.SetBucket(PreviousLeafNodeIndex, l, bucket);
            }
            //--- set bukets position map id = -1 because there is no free space in the tree to store them ---
            int RemainedBlocksCount = ClientStash.GetBlocksCount();
            for (int i = 0; i < RemainedBlocksCount; i++)
            {
                BlockToBuketPositionMapList[ClientStash.GetBlock(i).BlockID] = -1;
            }
        }
        public static void Access(Operation operation, int BlockID, string RecordID, BigInteger Key,RecordInfo recordInfo)
        {
            int PreviousLeafNodeIndex = AccessGet(BlockID);
            AccessPut(operation, BlockID, RecordID, RecordID, PreviousLeafNodeIndex, Key, recordInfo);
        }
        public static void Access(int BlockID)
        {
            int PreviousLeafNodeIndex = AccessGet(BlockID);
            AccessPut(BlockID, PreviousLeafNodeIndex);
        }
    }
}
