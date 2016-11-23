using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    public static class ManagerCalculation
    {
        public static int GetDigits(int n)
        {
            int digits = 0;
            while (n > 0)
            {
                n /= 10;
                digits++;
            }
            return digits;
        }
        public static BigInteger GetTreeLeafNodeCounts(int TreeHeight, int NoOfChildsPerNode)
        {
            return BigInteger.Pow(NoOfChildsPerNode, TreeHeight);
        }
        public static int GetTreeNodeCounts(int TreeHeight, int NoOfChildsPerNode)
        {
            int value = 0;
            for (int i = 0; i <= TreeHeight; i++)
            {
                value += (int)Math.Pow(NoOfChildsPerNode, i);
            }
            return value;
        }
        public static int GetTotalBlocks(int TreeHeight, int NoOfChildsPerNode, int NoOfBlocksPerBucket)
        {
            return GetTreeNodeCounts(TreeHeight, NoOfChildsPerNode) * NoOfBlocksPerBucket;
        }
        public static int GetTotalRecords(int TreeHeight, int NoOfChildsPerNode,int NoOfBlocksPerBucket, int NoOfRecordsPerBlock)
        {
            return GetTotalBlocks(TreeHeight, NoOfChildsPerNode, NoOfBlocksPerBucket) * NoOfRecordsPerBlock;
        }
        public static double GetRecordSize(DataUnit dataUnit,int KeySize)
        {
            double value = KeySize;// + sizeof(long) * 8;
            switch (dataUnit)
            {
                case DataUnit.Byte: value /= 8; break;
                case DataUnit.KBytes: value /= 8 * 1024; break;
                case DataUnit.MBytes: value /= 8 * 1024 * 1024; break;
                case DataUnit.GBytes: value /= (double)8 * 1024 * 1024 * 1024; break;
            }
            return value;
        }
        public static double GetBlockSize(DataUnit dataUnit,int NoOfRecordsPerBlock,int KeySize)
        {
            return NoOfRecordsPerBlock * GetRecordSize(dataUnit, KeySize);
        }
        public static double GetBucketSize(DataUnit dataUnit, int NoOfRecordsPerBlock, int KeySize, int NoOfBlocksPerBucket)
        {
            return NoOfBlocksPerBucket * GetBlockSize(dataUnit, NoOfRecordsPerBlock, KeySize);
        }
        public static double GetTreeSize(DataUnit dataUnit, int TreeHeight, int NoOfChildsPerNode, int NoOfRecordsPerBlock, int KeySize, int NoOfBlocksPerBucket)
        {
            return (double)GetTreeNodeCounts(TreeHeight, NoOfChildsPerNode) * GetBucketSize(dataUnit, NoOfRecordsPerBlock, KeySize, NoOfBlocksPerBucket);
        }
    }
}
