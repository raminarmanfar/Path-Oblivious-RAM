using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    static class ServerDBTree
    {
        public static ServerTreeNode dbTreeNode;
        public static void GenerateTree(ServerTreeNode treeNode, long CurrentLevel)
        {
            if (CurrentLevel <= ServerManager.TreeHeight)
            {
                for (int i = 0; i < ServerManager.NoOfChildsPerNode; i++)
                {
                    string NodeAddress = treeNode.NodeAddress + "." + i.ToString();
                    treeNode.Add(new ServerTreeNode(NodeAddress));
                    GenerateTree(treeNode.GetChild(NodeAddress), CurrentLevel + 1);
                    if (CurrentLevel == ServerManager.TreeHeight)
                    {
                        ServerManager.LeafNodesList.Add(treeNode.GetChild(NodeAddress));
                    }
                }
            }
        }
        public static dbBucket GetBucket(int LeafNodeIndex, string NodeAddress)
        {
            if (NodeAddress == "Root") return dbTreeNode.Bucket;
            else
            {
                ServerTreeNode curTreeNode = dbTreeNode;
                NodeAddress = NodeAddress.Substring(5) + ".";
                while (NodeAddress.Length > 0)
                {
                    string ad = NodeAddress.Substring(0, NodeAddress.IndexOf('.'));
                    NodeAddress = NodeAddress.Substring(NodeAddress.IndexOf('.') + 1);
                    curTreeNode = curTreeNode.GetChild(curTreeNode.NodeAddress + "." + ad);
                }
                return curTreeNode.Bucket;
            }
        }
    }
}
