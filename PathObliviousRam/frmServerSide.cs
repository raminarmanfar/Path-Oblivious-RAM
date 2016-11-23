using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;

namespace PathObliviousRam
{
    public partial class frmServerSide : Form
    {
        ServerTreeNode serverRootNode;
        public frmServerSide()
        {
            InitializeComponent();

            serverRootNode = null;
            RecordInfoList.init();
            ServerManager.Initialize(5, 2, 4, 5, 1024, KeysValueAssignType.Randomly, ManagerCalculation.GetTotalRecords(5, 2, 4, 5) / 2, 0, 0, 0);

            cbOrientaion.SelectedIndex = 0;
            ResetControls();
            cbSizeUnit.SelectedIndex = 0;
        }
        private void SetDatabaseSizeInfo(DataUnit dataUnit, bool AppendInLog)
        {
            lblBucketSizeInBits.Text = ManagerCalculation.GetBucketSize(dataUnit, ServerManager.NoOfRecordsPerBlock, ServerManager.KeySize, ServerManager.NoOfBlocksPerBucket).ToString();
            lblBlockSizeInBits.Text = ManagerCalculation.GetBlockSize(dataUnit,ServerManager.NoOfRecordsPerBlock,ServerManager.KeySize).ToString();
            lblRecordSizeInBits.Text = ManagerCalculation.GetRecordSize(dataUnit,ServerManager.KeySize).ToString();

            if (AppendInLog)
            {
                AppendLog("Record Size: " + ManagerCalculation.GetRecordSize(dataUnit,ServerManager.KeySize) + " " + dataUnit.ToString(), false);
                AppendLog("Block Size: " + ManagerCalculation.GetBlockSize(dataUnit,ServerManager.NoOfRecordsPerBlock,ServerManager.KeySize) + " " + dataUnit.ToString(), false);
                AppendLog("Bucket Size: " + ManagerCalculation.GetBucketSize(dataUnit,ServerManager.NoOfRecordsPerBlock,ServerManager.KeySize,ServerManager.NoOfBlocksPerBucket) + " " + dataUnit.ToString(), false);
                AppendLog("Database Size: " + ManagerCalculation.GetTreeSize(dataUnit,ServerManager.TreeHeight,ServerManager.NoOfChildsPerNode,ServerManager.NoOfRecordsPerBlock,ServerManager.KeySize,ServerManager.NoOfBlocksPerBucket) + " " + dataUnit.ToString(), true);
            }
        }
        private void SetDataUnitSize(bool AppendInLog)
        {
            switch (cbSizeUnit.SelectedIndex)
            {
                case 0: SetDatabaseSizeInfo(DataUnit.Bit, AppendInLog); break;
                case 1: SetDatabaseSizeInfo(DataUnit.Byte, AppendInLog); break;
                case 2: SetDatabaseSizeInfo(DataUnit.KBytes, AppendInLog); break;
                case 3: SetDatabaseSizeInfo(DataUnit.MBytes, AppendInLog); break;
                case 4: SetDatabaseSizeInfo(DataUnit.GBytes, AppendInLog); break;
            }
        }
        private void ResetControls()
        {
            btnGenerateDB.Enabled = true;
            btnStartClient.Enabled = false;
            btnGenerateDB.Enabled = false;

            cbDBExpandLevel.Items.Clear();

            tvDB.Nodes.Clear();
            tvDBContent.Nodes.Clear();

            lblTotalNodes.Text = "0";
            lblTotalBlocks.Text = "0";
            lblTotalRecords.Text = "0";

            lblSelectedNode.Text = "-";
            lblSelectedBlock.Text = "-";
            lblSelectedRecordID.Text = "-";

            lblTreeHeight.Text = ServerManager.TreeHeight.ToString();
            lblChildsPerNode.Text = ServerManager.NoOfChildsPerNode.ToString();
            lblBlocksPerBucket.Text = ServerManager.NoOfBlocksPerBucket.ToString();
            lblRecordsPerBlock.Text = ServerManager.NoOfRecordsPerBlock.ToString();

            SetDataUnitSize(false);
        }
        public void AppendLog(string Message,bool putLine)
        {
            txtLog.Text += ">>> " + Message + "\r\n";
            if (putLine)
                txtLog.Text += "--------------------------------------------------------------------------------------------------------------\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
        private void GenerateTreeView(ServerTreeNode DBTreeNode, TreeNode tvNode, long CurrentLevel)
        {
            for (int ch = 0; ch < ServerManager.NoOfChildsPerNode; ch++)
            {
                string label = DBTreeNode.NodeAddress + "." + ch.ToString();
                if (DBTreeNode.Count > 0)
                {
                    tvNode.Nodes.Add(label, label);
                    GenerateTreeView(DBTreeNode.GetChild(label), tvNode.Nodes[label], CurrentLevel + 1);
                }
            }
        }
        private void DBTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvDBContent.Nodes.Clear();
            int LeafNodeIndex = Convert.ToInt16(e.Node.Tag);
            dbBucket Bucket;
            Bucket = ServerDBTree.GetBucket(LeafNodeIndex, e.Node.Text);

            lblSelectedNode.Text = e.Node.Text;
            lblSelectedBlock.Text = "-";
            lblSelectedRecordID.Text = "-";

            for (int i = 0; i < ServerManager.NoOfBlocksPerBucket; i++)
            {
                dbBlock block = Bucket.GetBlock(i);
                tvDBContent.Nodes.Add("Block ID: " + Bucket.GetBlock(i).BlockID.ToString());
                for (int j = 0; j < ServerManager.NoOfRecordsPerBlock; j++)
                {
                    string RecordID = ManageSecurity.DecryptToString(block.GetRecord(j).EncryptedRecordID);
                    tvDBContent.Nodes[i].Nodes.Add("Record ID: " + RecordID);
                }
            }

            if (chkDBContentExpandAll.Checked) tvDBContent.ExpandAll();
            else tvDBContent.CollapseAll();
        }

        private void chkDBContentExpandAll_CheckedChanged(object sender, EventArgs e)
        {
            if (tvDBContent.Nodes.Count > 0)
            {
                if (chkDBContentExpandAll.Checked) tvDBContent.ExpandAll();
                else tvDBContent.CollapseAll();
            }
        }
        private void btnGenerateDB_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ClientManager.Initialize(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode, ServerManager.NoOfBlocksPerBucket, ServerManager.NoOfRecordsPerBlock);
            lblSelectedNode.Text = "-";
            lblSelectedBlock.Text = "-";
            lblSelectedRecordID.Text = "-";

            Stopwatch sw = new Stopwatch();
            sw.Start();

            serverRootNode = ServerDBTree.dbTreeNode = new ServerTreeNode("Root");
            ServerDBTree.GenerateTree(ServerDBTree.dbTreeNode, 1);

            sw.Stop();
            TimeSpan DBGenerationTime = sw.Elapsed;
            string DatabaseGenerationTime = DBGenerationTime.Hours.ToString() + ":" + DBGenerationTime.Minutes.ToString() + ":" + DBGenerationTime.Seconds.ToString() + ":" + DBGenerationTime.Milliseconds.ToString();
            AppendLog("Database Generation Time...", false);
            AppendLog("Days: " + DBGenerationTime.Days.ToString(), false);
            AppendLog("Time(Hour:Min:Sec:MSec): " + DatabaseGenerationTime, false);

            tvDB.Nodes.Add("Root", "Root");
            GenerateTreeView(ServerDBTree.dbTreeNode, tvDB.Nodes["Root"], 1);

            cbDBExpandLevel.Items.Clear();
            cbDBExpandLevel.Items.Add("Root");
            for (int i = 1; i <= ServerManager.TreeHeight; i++)
            {
                cbDBExpandLevel.Items.Add("Level " + i.ToString());
            }
            if (ServerManager.TreeHeight >= 5) cbDBExpandLevel.SelectedIndex = 5;
            else cbDBExpandLevel.SelectedIndex = ServerManager.TreeHeight;

            AppendLog("Database tree has been generated successfully...", false);
            AppendLog("Keys have been generated generated successfully...", true);
            AppendLog("Server Information...", false);

            SetDataUnitSize(true);

            lblTotalNodes.Text = ManagerCalculation.GetTreeNodeCounts(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode).ToString();
            lblTotalBlocks.Text = ManagerCalculation.GetTotalBlocks(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode, ServerManager.NoOfBlocksPerBucket).ToString();
            lblTotalRecords.Text = ManagerCalculation.GetTotalRecords(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode, ServerManager.NoOfBlocksPerBucket, ServerManager.NoOfRecordsPerBlock).ToString();

            AppendLog("Tree Height (L): " + ServerManager.TreeHeight.ToString(), false);
            AppendLog("Number of Childs Per Node: " + ServerManager.NoOfChildsPerNode.ToString(), false);
            AppendLog("Number of nodes in tree: " + lblTotalNodes.Text, false);
            AppendLog("Number blocks per bucket (Z): " + ServerManager.NoOfBlocksPerBucket, false);
            AppendLog("Number record per block (R): " + ServerManager.NoOfRecordsPerBlock, false);
            AppendLog("Number record per buket: " + ServerManager.NoOfRecordsPerBlock * ServerManager.NoOfBlocksPerBucket, false);
            AppendLog("Total number of records in the tree: " + ManagerCalculation.GetTotalRecords(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode, ServerManager.NoOfBlocksPerBucket, ServerManager.NoOfBlocksPerBucket), true);

            toolStripProgressBar1.Value = 100;
            Cursor = Cursors.Arrow;
            MessageBox.Show("Database tree has been generated successfully...\r\nTime taken for database generation(H:M:S:MS): " + DatabaseGenerationTime, "Database tree generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripProgressBar1.Value = 0;
            btnGenerateDB.Enabled = false;
            btnStartClient.Enabled = true;
        }
        private void btnExitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DBContentTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    lblSelectedBlock.Text = e.Node.Text.Substring(6);
                    lblSelectedRecordID.Text = "-";
                    break;
                case 1:
                    lblSelectedBlock.Text = e.Node.Parent.Text.Substring(e.Node.Parent.Text.IndexOf(':') + 1);
                    lblSelectedRecordID.Text = e.Node.Text.Substring(e.Node.Text.IndexOf(':') + 1);
                    break;
            }
        }
        private void btnResetServer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All database contents will be erased and will not be retrived.\r\nAre you sure to reset server ?", "Database server reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ServerManager.Initialize(5, 2, 4, 5, 1024, KeysValueAssignType.Randomly, ManagerCalculation.GetTotalRecords(5, 2, 3, 5) / 2, 0, 0, 0);

                ResetControls();
                AppendLog("Database server has been completely erased successfully...", true);
                toolStripProgressBar1.Value = 100;
                MessageBox.Show("Database server has been completely erased successfully...", "Database server reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripProgressBar1.Value = 0;
            }

        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
            frmClientSide frmClientSide = new frmClientSide(this, ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode);
            frmClientSide.Show();
        }

        private void cbSizeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDataUnitSize(false);
        }

        private void cbOrientaion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbOrientaion.SelectedIndex)
            {
                case 0:
                    splitContainer1.Orientation = Orientation.Vertical;
                    splitContainer1.SplitterDistance = splitContainer1.Width / 2;
                    break;
                case 1:
                    splitContainer1.Orientation = Orientation.Horizontal;
                    splitContainer1.SplitterDistance = splitContainer1.Height / 2;
                    break;
            }
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            frmAboutUs frm = new frmAboutUs();
            frm.ShowDialog(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmServerSettings frm = new frmServerSettings(this);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                ResetControls();

                AppendLog("Database server settings have been set successfully and database has been reset...", false);
                AppendLog("Tree Height (L): " + ServerManager.TreeHeight.ToString(), false);
                AppendLog("Number of Childs Per Node: " + ServerManager.NoOfChildsPerNode.ToString(), false);
                AppendLog("Number of nodes in tree: " + ManagerCalculation.GetTreeNodeCounts(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode).ToString(), false);
                AppendLog("Number blocks per bucket (Z): " + ServerManager.NoOfBlocksPerBucket, false);
                AppendLog("Number record per block (R): " + ServerManager.NoOfRecordsPerBlock, false);
                AppendLog("Number record per buket: " + ServerManager.NoOfRecordsPerBlock * ServerManager.NoOfBlocksPerBucket, false);
                AppendLog("Total number of records in the tree: " + ManagerCalculation.GetTotalRecords(ServerManager.TreeHeight, ServerManager.NoOfChildsPerNode, ServerManager.NoOfBlocksPerBucket, ServerManager.NoOfBlocksPerBucket), true);

                btnGenerateDB.Enabled = true;
                toolStripProgressBar1.Value = 100;
                MessageBox.Show("Database server settings have been set successfully...\r\nPlease generate new database with new settings to make it functional.", "Database server settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripProgressBar1.Value = 0;
            }
            else
            {
                AppendLog("Database server settings has been cancelled...", true);
            }
        }
        private void DBContentTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //---Clipboard.SetText(e.Node.Text);
            if (e.Node.Level == 1)
            {
                dbRecord selectedRecord = ServerManager.GetRecordByNodeAddress(serverRootNode, tvDB.SelectedNode.Text, e.Node.Parent.Index, e.Node.Index);
                frmServerRecordContents frm = new frmServerRecordContents(tvDB.SelectedNode.Text, e.Node.Parent.Text.Substring(e.Node.Parent.Text.IndexOf(':') + 1), selectedRecord);
                frm.ShowDialog(this);
            }
        }
        private void ExpandTreeUpToLevel(TreeNode treeNode,int level,int upToLevel)
        {
            if (level < upToLevel)
            {
                for (int ch = 0; ch < ServerManager.NoOfChildsPerNode; ch++)
                {
                    treeNode.Nodes[ch].Expand();
                    ExpandTreeUpToLevel(treeNode.Nodes[ch], level + 1, upToLevel);
                }
            }
        }
        private void cbDBExpandLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvDB.CollapseAll();
            if (cbDBExpandLevel.SelectedIndex > 0)
            {
                tvDB.Nodes[0].Expand();
                ExpandTreeUpToLevel(tvDB.Nodes[0], 0, cbDBExpandLevel.SelectedIndex - 1);
            }
            tvDBContent.Nodes.Clear();
            lblSelectedNode.Text = "-";
            lblSelectedBlock.Text = "-";
            lblSelectedRecordID.Text = "-";
        }

        private void lblSelectedRecordID_MouseUp(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(lblSelectedRecordID.Text);
        }
    }
}
