using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PathObliviousRam
{
    public class dbBucket
    {
        List<dbBlock> Blocks;
        public dbBucket()
        {
            Blocks = new List<dbBlock>(ServerManager.NoOfBlocksPerBucket);
            for (int i = 0; i < ServerManager.NoOfBlocksPerBucket; i++)
            {
                Blocks.Add(new dbBlock());
            }

        }
        public dbBucket(int LeafNodeIndex)
        {
            //--- generating blocks for current node using leaf address and block id ---
            Blocks = new List<dbBlock>(ServerManager.NoOfBlocksPerBucket);
            for (int i = 0; i < ServerManager.NoOfBlocksPerBucket; i++)
            {
                ClientManager.BlockToBuketPositionMapList.Add(LeafNodeIndex);
                ClientManager.VacantRecordsList.Add(ClientManager.NoOfRecordsPerBlock);

                Blocks.Add(new dbBlock(ServerManager.BlockIDCounter++));
            }
        }

        public dbBlock GetBlock(int BlockIndex)
        {
            return Blocks[BlockIndex];
        }
        public List<dbBlock> GetWoleBlocks()
        {
            return Blocks;
        }
        public dbBucket CopyBucketContents()
        {
            dbBucket DestBucket = new dbBucket();
            for (int i = 0; i < ServerManager.NoOfBlocksPerBucket; i++)
            {
                DestBucket.Blocks[i] = Blocks[i].CopyBlockContents();
            }
            return DestBucket;
        }
        public void WriteBlockToBucket(dbBlock BlockToWrite,int BlockIndexToWrite)
        {
            Blocks[BlockIndexToWrite] = BlockToWrite;
        }
    }
}
