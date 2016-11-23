using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    public class ServerTreeNode : IEnumerable<ServerTreeNode>
    {
        private readonly Dictionary<string, ServerTreeNode> _children = new Dictionary<string, ServerTreeNode>();

        public int BelongToLeafNodeIndex;
        public readonly string NodeAddress;
        public dbBucket Bucket;
        public ServerTreeNode Parent { get; private set; }
        public ServerTreeNode(string NodeAddress)
        {
            BelongToLeafNodeIndex = ServerManager.LeafNodesList.Count;
            this.NodeAddress = NodeAddress;
            Bucket = new dbBucket(ServerManager.LeafNodesList.Count);
        }
        public ServerTreeNode GetChild(string NodeAddress)
        {
            return _children[NodeAddress];
        }
        public void Add(ServerTreeNode item)
        {
            if (item.Parent != null)
            {
                item.Parent._children.Remove(item.NodeAddress);
            }

            item.Parent = this;
            this._children.Add(item.NodeAddress, item);
        }
        public IEnumerator<ServerTreeNode> GetEnumerator()
        {
            return this._children.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /*
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        */
        public int Count
        {
            get { return this._children.Count; }
        }
    }
}
