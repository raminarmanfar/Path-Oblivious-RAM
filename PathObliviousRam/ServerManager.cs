using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    public enum KeysValueAssignType { Randomly, Zero, One }
    public enum DataUnit { Bit, Byte, KBytes, MBytes, GBytes }
    public static class ServerManager
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        public static KeysValueAssignType keysValueAssignType { get; private set; }
        public static int TreeHeight { get; private set; }
        public static int NoOfChildsPerNode { get; private set; }
        public static int NoOfBlocksPerBucket { get; private set; }
        public static int NoOfRecordsPerBlock { get; private set; }
        public static int KeySize { get; private set; }//--- key size unit is bits
        public static BigInteger RecIDCounter { get; set; }
        public static int BlockIDCounter { get; set; }
        public static List<ServerTreeNode> LeafNodesList;
        public static int NoOfRecordsToGenerate { get; private set; }
        public static int NoOfRandRecName { get; private set; }
        public static int NoOfRandRecFamily { get; private set; }
        public static int NoOfRandRecCity { get; private set; }
        public static void Initialize(int TreeHeight, int NoOfChildsPerNode, int NoOfBlocksPerBucket, int NoOfRecordsPerBlock, int KeySize, KeysValueAssignType keysValueAssignType, int NoOfRecordsToGenerate, int NoOfRandRecName, int NoOfRandRecFamily, int NoOfRandRecCity)
        {
            RecIDCounter = 0;
            BlockIDCounter = 0;
            ServerManager.TreeHeight = TreeHeight;
            ServerManager.NoOfChildsPerNode = NoOfChildsPerNode;
            ServerManager.NoOfBlocksPerBucket = NoOfBlocksPerBucket;
            ServerManager.NoOfRecordsPerBlock = NoOfRecordsPerBlock;
            ServerManager.KeySize = KeySize;
            ServerManager.keysValueAssignType = keysValueAssignType;
            ServerManager.NoOfRecordsToGenerate = NoOfRecordsToGenerate;

            LeafNodesList = new List<ServerTreeNode>();
            ManageSecurity.Initialize("r@m!n", "$@1tV@1ue", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);//--- it should be in client side

            ServerManager.NoOfRandRecName = NoOfRandRecName;
            ServerManager.NoOfRandRecFamily = NoOfRandRecFamily;
            ServerManager.NoOfRandRecCity = NoOfRandRecCity;
        }
        //--- Bucket Operations ---
        public static List<dbBucket> GetAllBucketsByLeafNodeIndex(int leafNodeIndex)
        {
            List<dbBucket> result = new List<dbBucket>();
            ServerTreeNode curTreeNode = ServerManager.LeafNodesList[leafNodeIndex];
            while (curTreeNode != null)
            {
                result.Add(curTreeNode.Bucket);
                curTreeNode = curTreeNode.Parent;
            }
            return result;
        }

        public static dbRecord GetRecordByNodeAddress(ServerTreeNode rootNode,string NodeAddress, int BlockIndex, int RecordIndex)
        {
            ServerTreeNode treeNode = rootNode;
            string curAddress = NodeAddress.Substring(0, 4);
            if (NodeAddress != "Root")
            {
                NodeAddress = NodeAddress.Substring(5) + ".";
                while (NodeAddress.Length > 0)
                {
                    curAddress += "." + NodeAddress.Substring(0, NodeAddress.IndexOf('.'));
                    NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf('.') + 1);
                    treeNode = treeNode.GetChild(curAddress);
                }
            }
            return treeNode.Bucket.GetBlock(BlockIndex).GetRecord(RecordIndex);
        }
        public static bool LeafsAresInSimilarPath(int leafNodeIndexA, int leafNodeIndexB, int TreeLevel)
        {
            ServerTreeNode curTreeNodeA = LeafNodesList[leafNodeIndexA];
            ServerTreeNode curTreeNodeB = ServerManager.LeafNodesList[leafNodeIndexB];
            while (TreeLevel<=TreeHeight)
            {
                if (curTreeNodeA == curTreeNodeB) return true;
                curTreeNodeA = curTreeNodeA.Parent;
                curTreeNodeB = curTreeNodeB.Parent;
                TreeLevel++;
            }
            return false;
        }
        public static void SetBucket(int LeafNodeIndex, int TreeLevel, dbBucket BucketToSet)
        {
            ServerTreeNode curTreeNode = LeafNodesList[LeafNodeIndex];
            for (int i = TreeHeight; i > TreeLevel; i--)
            {
                curTreeNode = curTreeNode.Parent;
            }
            curTreeNode.Bucket = BucketToSet.CopyBucketContents();
        }
    }
}
