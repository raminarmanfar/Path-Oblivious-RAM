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
    public enum UserOperations { GetRecord, ModifyRecord, AddRecord, DeleteRecord };
    public partial class frmClientSide : Form
    {
        frmServerSide frmServer;
        public frmClientSide(frmServerSide frmServer, int TreeHeight, int NoOfChildsPerNode)
        {
            InitializeComponent();
            this.frmServer = frmServer;

            //ClassClient.Initialize(TreeHeight, NoOfChildsPerNode, ClassServer.NoOfBlocksPerBucket, ClassServer.NoOfRecordsPerBlock);

            lblTreeHeight.Text = TreeHeight.ToString();
            lblTotalBuketsInDB.Text = ManagerCalculation.GetTreeNodeCounts(TreeHeight, NoOfChildsPerNode).ToString();
            lblTotalBlocksInDB.Text = ClientManager.BlockToBuketPositionMapList.Count.ToString();
            lblTotalRecordsInDB.Text = ClientManager.RecordIdToBlockMapList.Count.ToString();

            lblTotalBlocksInStash.Text = "0";

            AppendLog("Client has been successfully established and ready to operations...", true);
            cbOrientaion.SelectedIndex = 0;

            lblContainedBlockID.Text = "-";
            lblTargetLeafNode.Text = "-";
            btnModifyRecord.Enabled = false;
            btnReadData.Enabled = false;
            btnDeleteRecord.Enabled = false;
            UpdateStashTreeView();
            //---cbOrientaion.SelectedIndex = 1;
            UpdateStashTreeView();
        }
        RecordInfo enterdRecordInfo;
        public void SetRecordInfo(string RecordID, BigInteger Key,RecordInfo recordInfo)
        {
            txtRecordID.Text = RecordID;
            txtObtainedRecKey.Text = Key.ToString();
            enterdRecordInfo = recordInfo;

        }
        public void AppendLog(string Message, bool putLine)
        {
            txtLog.Text += ">>> " + Message + "\r\n";
            if (putLine)
                txtLog.Text += "--------------------------------------------------------------------------------------------------------------\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("All revious logs will be deleted.\r\nAre you sure ?","Clear logs history", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
            {
                txtLog.Clear();
            }
        }
        private void lblContainedBlockID_TextChanged(object sender, EventArgs e)
        {
            if (lblContainedBlockID.Text != "-")
            {
                int blockNo = Convert.ToInt16(lblContainedBlockID.Text);
                int LeafIndex = ClientManager.BlockToBuketPositionMapList[blockNo];
                lblTargetLeafNode.Text = LeafIndex.ToString();
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnReadData_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int blockID = ClientManager.RecordIdToBlockMapList[txtRecordID.Text];
            //--- check whether requested blockID is in stash or not ---
            int BlockIndexInStash = ClientStash.GetBlockIndexByID(blockID);
            if (BlockIndexInStash == -1)//--- block is NOT in the stash ---
            {
                ClientManager.Access(Operation.ReadData, blockID, txtRecordID.Text, BigInteger.Parse(txtObtainedRecKey.Text), null);
                sw.Stop();
                long AccessTime = sw.ElapsedMilliseconds;

                sw.Restart();
                int CurrentLeafNodeIndex = ClientManager.BlockToBuketPositionMapList[blockID];
                string PreviousLeafNodeIndex = lblTargetLeafNode.Text;
                lblTargetLeafNode.Text = CurrentLeafNodeIndex.ToString();
                txtObtainedRecKey.Text = ManageSecurity.Decrypt(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedKey).ToString();
                string Name = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedName);
                string Family = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedFamily);
                string Email = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedEmail);
                string City = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedCity);
                sw.Stop();
                long DecryptionTime = sw.ElapsedMilliseconds;

                AppendLog("Read Data operation has been done successfully...", false);
                AppendLog("Previous Leaf Node Index: " + PreviousLeafNodeIndex, false);
                AppendLog("Current Contained Leaf Node Index: " + lblTargetLeafNode.Text, false);
                AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                AppendLog("Requested Record No: " + txtRecordID.Text, false);
                AppendLog("Retrived record information...", true);
                AppendLog("Key: " + txtObtainedRecKey.Text, false);
                AppendLog("Name: " + Name, false);
                AppendLog("Fanily: " + Family, false);
                AppendLog("Email: " + Email, false);
                AppendLog("City: " + City, true);
                AppendLog("Access Time: " + AccessTime + " ms", false);
                AppendLog("Decryption Time: " + DecryptionTime + " ms", false);
                AppendLog("Total Time Taken to complete the process: " + (AccessTime+DecryptionTime) + " ms", true);
                frmServer.AppendLog("Path starting from leaf index (" + PreviousLeafNodeIndex + ") has been successfully fetched by client...", true);

                UpdateStashTreeView();
                MessageBox.Show("Requeted record has been retrieved successfully...", "Read record operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else// --- block is in the stash
            {
                lblTargetLeafNode.Text = "-1";
                string EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                int RecordIndex = ClientStash.GetBlock(BlockIndexInStash).GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                txtObtainedRecKey.Text = ManageSecurity.Decrypt(ClientStash.GetBlock(BlockIndexInStash).GetRecord(RecordIndex).EncryptedKey).ToString();
                sw.Stop();

                AppendLog("Read Data operation has been done successfully...", false);
                AppendLog("This record is stored in the stash...", false);
                AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                AppendLog("Requested Record No: " + txtRecordID.Text, false);
                AppendLog("Retrived Key: " + txtObtainedRecKey.Text, false);
                AppendLog("Time Taken to complete the process: " + sw.ElapsedMilliseconds.ToString() + " ms", true);

                UpdateStashTreeView();
                MessageBox.Show("Requeted record has been retrieved successfully...", "Read record operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void UpdateStashTreeView()
        {
            tvStash.Nodes.Clear();
            for (int i = 0; i < ClientStash.GetBlocksCount(); i++)
            {
                string curBlockID = ClientStash.GetBlock(i).BlockID.ToString();
                tvStash.Nodes.Add(curBlockID, "Block (" + curBlockID + ")");
                for (int j = 0; j < ClientStash.GetBlock(i).ReadWholeRecords().Count; j++)
                {
                    string RecID = ManageSecurity.DecryptToString(ClientStash.GetBlock(i).GetRecord(j).EncryptedRecordID);
                    string Key = ManageSecurity.Decrypt(ClientStash.GetBlock(i).GetRecord(j).EncryptedKey).ToString();
                    tvStash.Nodes[tvStash.Nodes.Count - 1].Nodes.Add(RecID + " => " + Key);
                    tvStash.Nodes[tvStash.Nodes.Count - 1].Expand();
                }
            }
            lblTotalBlocksInStash.Text = ClientStash.GetBlocksCount().ToString();
        }
        private void cbOrientaion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbOrientaion.SelectedIndex)
            {
                case 0:
                    splitContainer1.Orientation = Orientation.Vertical;
                    splitContainer1.SplitterDistance = splitContainer1.Width / 2 - 50;
                    break;
                case 1:
                    splitContainer1.Orientation = Orientation.Horizontal;
                    splitContainer1.SplitterDistance = splitContainer1.Height / 2;
                    break;
            }
        }
        private void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            int vacantBlockID = ClientManager.FindVacantBlockID();
            if (vacantBlockID == -1)//--- there is no vacant record neither in the database tree nor in the stash ---
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //--- Make new block in the stash and store records inside that one.
                //--- here we must add new record inside created block.

                sw.Stop();
                long TimeElapsed = sw.ElapsedMilliseconds;

                frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, "Dummy", UserOperations.AddRecord, null);
                DialogResult d = frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.Cancel)
                {
                    UpdateStashTreeView();
                    AppendLog("There were no change in the leaf indices because the block is resided in the stash...", false);
                    AppendLog("Record addition operation for has been cancelled...", true);
                }
                else
                {
                    int NewBlockID = ClientManager.BlockToBuketPositionMapList.Count;
                    ClientStash.AddBlockSorted(new dbBlock(NewBlockID));
                    ClientManager.VacantRecordsList.Add(ClientManager.NoOfRecordsPerBlock);
                    ClientManager.BlockToBuketPositionMapList.Add(-1);//--- there is no free space in tree to assign leaf index then we store -1 which means stash

                    int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(NewBlockID);
                    ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                    ClientManager.RecordIdToBlockMapList.Add(txtRecordID.Text, NewBlockID);

                    ClientManager.NameToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Name, NewBlockID);
                    ClientManager.FamilyToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Family, NewBlockID);
                    ClientManager.CityToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.City, NewBlockID);

                    ClientManager.VacantRecordsList[NewBlockID]--;
                    //----string ObtainedKey = ClassSecurity.Decrypt(ClassClient.GetRequestedRecord(txtRecordID.Text).EncryptedKey).ToString();
                    //----txtObtainedRecKey.Text = ObtainedKey;

                    string EncryptedRecordID = ManageSecurity.Encrypt("Dummy");
                    int RequestedRecordIndex = ClientManager.RequestedDataBlock.GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey = ManageSecurity.Encrypt(txtObtainedRecKey.Text);

                    UpdateStashTreeView();
                    AppendLog("New record has been added to the stash successfully...", false);
                    AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                    AppendLog("New Record ID: " + txtRecordID.Text, false);
                    AppendLog("New Key Value: " + txtObtainedRecKey.Text, true);
                    AppendLog("New record has been added to the stash because there is no free space in the database tree...", false);
                    AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                    MessageBox.Show("New record has been added to the stash successfully...", "Record key addition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmServer.AppendLog("New record has been added to the stash because there is no free space in the database tree...", true);
                }
            }
            else//--- Vacant record found in the database or stash ---
            {
                //--- requested block resides in the stash ---
                if (ClientManager.BlockToBuketPositionMapList[vacantBlockID] == -1)
                {
                    frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, "Dummy", UserOperations.AddRecord, null);
                    DialogResult d = frm.ShowDialog(this);

                    if (frm.DialogResult == DialogResult.Cancel)
                    {
                        UpdateStashTreeView();
                        AppendLog("There were no change in the leaf indices because the block is resided in the stash...", false);
                        AppendLog("Record addition operation for has been cancelled...", true);
                    }
                    else
                    {
                        int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(vacantBlockID);
                        ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                        ClientManager.RecordIdToBlockMapList.Add(txtRecordID.Text, vacantBlockID);

                        ClientManager.NameToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Name, vacantBlockID);
                        ClientManager.FamilyToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Family, vacantBlockID);
                        ClientManager.CityToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.City, vacantBlockID);

                        ClientManager.VacantRecordsList[vacantBlockID]--;

                        string EncryptedRecordID = ManageSecurity.Encrypt("Dummy");
                        int RequestedRecordIndex = ClientManager.RequestedDataBlock.GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                        ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                        ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey = ManageSecurity.Encrypt(txtObtainedRecKey.Text);

                        UpdateStashTreeView();
                        AppendLog("New record has been added to the database successfully...", false);
                        AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                        AppendLog("New Record ID: " + txtRecordID.Text, false);
                        AppendLog("New Key Value: " + txtObtainedRecKey.Text, true);
                        AppendLog("Time Taken to complete the process: 0 ms", true);

                        MessageBox.Show("New record has been added to the database successfully...", "Record key addition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmServer.AppendLog("There were no change in the leaf indices because the block is resided in the stash...", true);
                    }
                }
                else//--- requested block resides in the database tree ---
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    int PreviousLeafNodeIndex = ClientManager.AccessGet(vacantBlockID);

                    int CurrentLeafNodeIndex = ClientManager.BlockToBuketPositionMapList[vacantBlockID];
                    string prevNodeIndex = lblTargetLeafNode.Text;
                    lblTargetLeafNode.Text = CurrentLeafNodeIndex.ToString();

                    sw.Stop();
                    long TimeElapsed = sw.ElapsedMilliseconds;

                    frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, "Dummy", UserOperations.AddRecord, null);
                    DialogResult d = frm.ShowDialog(this);

                    if (frm.DialogResult == DialogResult.Cancel)
                    {
                        ClientManager.AccessPut(Operation.ReadData, vacantBlockID, txtRecordID.Text, txtRecordID.Text, PreviousLeafNodeIndex, BigInteger.Parse(txtObtainedRecKey.Text), null);
                        UpdateStashTreeView();
                        AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                        AppendLog("Record addition operation for has been cancelled...", true);
                    }
                    else
                    {
                        sw.Start();
                        int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(vacantBlockID);
                        ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                        ClientManager.RecordIdToBlockMapList.Add(txtRecordID.Text, vacantBlockID);

                        ClientManager.NameToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Name, vacantBlockID);
                        ClientManager.FamilyToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.Family, vacantBlockID);
                        ClientManager.CityToBlockMapList.addNewBlockIDByFieldValue(enterdRecordInfo.City, vacantBlockID);

                        ClientManager.VacantRecordsList[vacantBlockID]--;
                        ClientManager.AccessPut(Operation.WriteData, vacantBlockID, "Dummy", txtRecordID.Text, PreviousLeafNodeIndex, BigInteger.Parse(txtObtainedRecKey.Text), enterdRecordInfo);
                        sw.Stop();
                        TimeElapsed += sw.ElapsedMilliseconds;

                        UpdateStashTreeView();
                        AppendLog("New record has been added to the database successfully...", false);
                        AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                        AppendLog("New Record ID: " + txtRecordID.Text, false);
                        AppendLog("New Key Value: " + txtObtainedRecKey.Text, true);
                        AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                        AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                        MessageBox.Show("New record has been added to the database successfully...", "Record key addition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmServer.AppendLog("Path starting from leaf index (" + prevNodeIndex + ") has been successfully fetched by client...", true);
                    }
                }
            }
            SetEnteredRecInfo();
        }
        private void frmClientSide_Load(object sender, EventArgs e)
        {
            txtRecordID.Focus();
        }
        private void SetEnteredRecInfo()
        {
            try
            {
                lblContainedBlockID.Text = ClientManager.RecordIdToBlockMapList[txtRecordID.Text].ToString();
                btnModifyRecord.Enabled = true;
                btnReadData.Enabled = true;
                btnDeleteRecord.Enabled = true;
            }
            catch (Exception ex)
            {
                lblContainedBlockID.Text = "-";
                lblTargetLeafNode.Text = "-";
                btnModifyRecord.Enabled = false;
                btnReadData.Enabled = false;
                btnDeleteRecord.Enabled = false;

                //MessageBox.Show(ex.Message, "Error in entered record number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtObtainedRecKey.Text = "0";
        }
        private void txtRecordID_TextChanged(object sender, EventArgs e)
        {
            SetEnteredRecInfo();
        }
        private void btnModifyRecord_Click(object sender, EventArgs e)
        {
            long TimeElapsed = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int blockID = ClientManager.RecordIdToBlockMapList[txtRecordID.Text];
            int LeafNodeIndex = ClientManager.BlockToBuketPositionMapList[blockID];
            if (LeafNodeIndex > -1)//--- Requested Block resides in the database tree ---
            {
                int PreviousLeafNodeIndex = ClientManager.AccessGet(blockID);

                sw.Stop();
                TimeElapsed = sw.ElapsedMilliseconds;

                //---PerformDatabaseOperation(Operation.ReadData, false);
                string PreviousRecordID = txtRecordID.Text;

                string Name = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedName);
                string Family = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedFamily);
                string Email = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedEmail);
                string City = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedCity);

                RecordInfo previousInfo = new RecordInfo(Name, Family, Email, City);
                BigInteger PreviousKey = ManageSecurity.Decrypt(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedKey);

                int CurrentLeafNodeIndex = ClientManager.BlockToBuketPositionMapList[blockID];
                string prevNodeIndex = lblTargetLeafNode.Text;
                lblTargetLeafNode.Text = CurrentLeafNodeIndex.ToString();
                frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, txtRecordID.Text, UserOperations.ModifyRecord, previousInfo);
                DialogResult d = frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.Cancel)
                {
                    ClientManager.AccessPut(Operation.ReadData, blockID, PreviousRecordID, txtRecordID.Text, PreviousLeafNodeIndex, BigInteger.Parse(txtObtainedRecKey.Text), null);
                    UpdateStashTreeView();
                    AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                    AppendLog("Modification operation for record ID(" + txtRecordID.Text + ") has been cancelled...", true);
                }
                else
                {
                    sw.Start();

                    ClientManager.ChangeKey(ClientManager.RecordIdToBlockMapList, PreviousRecordID, txtRecordID.Text);
                    if (Name != enterdRecordInfo.Name)
                        ClientManager.NameToBlockMapList.ModifyPositionMapFieldValue(Name, enterdRecordInfo.Name);
                    if (Family != enterdRecordInfo.Family)
                        ClientManager.FamilyToBlockMapList.ModifyPositionMapFieldValue(Family, enterdRecordInfo.Family);
                    if (City != enterdRecordInfo.City)
                        ClientManager.CityToBlockMapList.ModifyPositionMapFieldValue(City, enterdRecordInfo.City);

                    ClientManager.AccessPut(Operation.WriteData, blockID, PreviousRecordID, txtRecordID.Text, PreviousLeafNodeIndex, BigInteger.Parse(txtObtainedRecKey.Text), enterdRecordInfo);

                    sw.Stop();
                    TimeElapsed += sw.ElapsedMilliseconds;

                    UpdateStashTreeView();
                    AppendLog("Selected record information has been updated successfully...", false);
                    AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                    AppendLog("Previous Record ID: " + PreviousRecordID, false);
                    AppendLog("Previous Key Value: " + PreviousKey.ToString(), false);
                    AppendLog("Updated Record ID: " + txtRecordID.Text, false);
                    AppendLog("Updated Key Value: " + txtObtainedRecKey.Text, true);
                    AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                    AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                    MessageBox.Show("Selected records information has been updated successfully...", "Record information modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmServer.AppendLog("Path starting from leaf index (" + prevNodeIndex + ") has been successfully fetched by client...", true);
                }
            }
            else//--- requested block resides in the stash ---
            {
                sw.Start();
                int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(blockID);
                string EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                int RequestedRecordIndex = ClientStash.GetBlock(RequestedBlockIndexInStash).GetRecordIndexByEncryptedRecordID(EncryptedRecordID);

                string PreviousRecordID = txtRecordID.Text;
                string Name = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedName);
                string Family = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedFamily);
                string Email = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedEmail);
                string City = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedCity);

                RecordInfo previousInfo = new RecordInfo(Name, Family, Email, City);

                BigInteger PreviousKey = ManageSecurity.Decrypt(ClientManager.GetRequestedRecord(txtRecordID.Text).EncryptedKey);

                sw.Stop();
                TimeElapsed += sw.ElapsedMilliseconds;

                frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, txtRecordID.Text, UserOperations.ModifyRecord, previousInfo);
                DialogResult d = frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.Cancel)
                {
                    UpdateStashTreeView();
                    AppendLog("There were no change in the leaf indices because the block is resided in the stash...", false);
                    AppendLog("Modification operation for record ID(" + txtRecordID.Text + ") has been cancelled...", true);
                }
                else
                {
                    ClientManager.ChangeKey(ClientManager.RecordIdToBlockMapList, PreviousRecordID, txtRecordID.Text);
                    if (Name != enterdRecordInfo.Name)
                        ClientManager.NameToBlockMapList.ModifyPositionMapFieldValue(Name, enterdRecordInfo.Name);
                    if (Family != enterdRecordInfo.Family)
                        ClientManager.FamilyToBlockMapList.ModifyPositionMapFieldValue(Family, enterdRecordInfo.Family);
                    if (City != enterdRecordInfo.City)
                        ClientManager.CityToBlockMapList.ModifyPositionMapFieldValue(City, enterdRecordInfo.City);


                    ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey = ManageSecurity.Encrypt(txtObtainedRecKey.Text);

                    UpdateStashTreeView();
                    AppendLog("Selected record information has been updated successfully...", false);
                    AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                    AppendLog("Previous Record ID: " + PreviousRecordID, false);
                    AppendLog("Previous Key Value: " + PreviousKey.ToString(), false);
                    AppendLog("Updated Record ID: " + txtRecordID.Text, false);
                    AppendLog("Updated Key Value: " + txtObtainedRecKey.Text, true);
                    AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                    MessageBox.Show("Selected records information has been updated successfully...", "Record information modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmServer.AppendLog("There were no change in the leaf indices because the block is resided in the stash...", true);
                }
            }
            SetEnteredRecInfo();
        }
        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            long TimeElapsed = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int blockID = ClientManager.RecordIdToBlockMapList[txtRecordID.Text];
            int LeafNodeIndex = ClientManager.BlockToBuketPositionMapList[blockID];

            if (LeafNodeIndex > -1)//--- Requested Block resides in the database tree ---
            {
                string PreviousRecordID = txtRecordID.Text;
                int PreviousLeafNodeIndex = ClientManager.AccessGet(blockID);

                sw.Stop();
                TimeElapsed = sw.ElapsedMilliseconds;

                int CurrentLeafNodeIndex = ClientManager.BlockToBuketPositionMapList[blockID];
                string prevNodeIndex = lblTargetLeafNode.Text;
                lblTargetLeafNode.Text = CurrentLeafNodeIndex.ToString();

                int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(blockID);
                ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                string EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                int RequestedRecordIndex = ClientStash.GetBlock(RequestedBlockIndexInStash).GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                string KeyToDelete = ManageSecurity.Decrypt(ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey).ToString();

                string Name = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedName);
                string Family = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedFamily);
                string Email = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedEmail);
                string City = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedCity);

                RecordInfo recordInfo = new RecordInfo(Name, Family, Email, City);

                frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, txtRecordID.Text, UserOperations.DeleteRecord, recordInfo);
                DialogResult d = frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.Cancel)
                {
                    ClientManager.AccessPut(Operation.ReadData, blockID, PreviousRecordID, txtRecordID.Text, PreviousLeafNodeIndex, BigInteger.Zero, null);
                    UpdateStashTreeView();
                    AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                    AppendLog("Omission operation for record ID(" + txtRecordID.Text + ") has been cancelled...", true);
                }
                else
                {
                    sw.Start();

                    ClientManager.AccessPut(Operation.WriteData, blockID, PreviousRecordID, "Dummy", PreviousLeafNodeIndex, BigInteger.Zero, enterdRecordInfo);
                    ClientManager.RecordIdToBlockMapList.Remove(PreviousRecordID);

                    ClientManager.NameToBlockMapList.removeNewBlockIDByFieldValue(Name, blockID);
                    ClientManager.FamilyToBlockMapList.removeNewBlockIDByFieldValue(Family, blockID);
                    ClientManager.CityToBlockMapList.removeNewBlockIDByFieldValue(City, blockID);

                    ClientManager.VacantRecordsList[blockID]++;

                    sw.Stop();
                    TimeElapsed += sw.ElapsedMilliseconds;

                    UpdateStashTreeView();
                    AppendLog("Selected record information has been omited from database successfully...", false);
                    AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                    AppendLog("Omited Record ID: " + PreviousRecordID, false);
                    AppendLog("Omited Key Value: " + KeyToDelete, true);
                    AppendLog("Requested Record's leaf node index has been transfered from (" + PreviousLeafNodeIndex.ToString() + ") to (" + CurrentLeafNodeIndex.ToString() + ")", false);
                    AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                    txtRecordID.Text = string.Empty;

                    MessageBox.Show("Selected records information has been omited from database successfully...", "Record deleted from database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmServer.AppendLog("Path starting from leaf index (" + prevNodeIndex + ") has been successfully fetched by client...", true);
                }
            }
            else//--- requested block resides in the stash ---
            {
                int RequestedBlockIndexInStash = ClientStash.GetBlockIndexByID(blockID);
                ClientManager.RequestedDataBlock = ClientStash.GetBlock(RequestedBlockIndexInStash);
                string EncryptedRecordID = ManageSecurity.Encrypt(txtRecordID.Text);
                int RequestedRecordIndex = ClientStash.GetBlock(RequestedBlockIndexInStash).GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                string PreviousRecordID = txtRecordID.Text;
                string KeyToDelete = ManageSecurity.Decrypt(ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey).ToString();

                string Name = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedName);
                string Family = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedFamily);
                string Email = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedEmail);
                string City = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(PreviousRecordID).EncryptedCity);

                RecordInfo recordInfo = new RecordInfo(Name, Family, Email, City);

                frmClientAddEditRecord frm = new frmClientAddEditRecord(this, ServerManager.KeySize, txtRecordID.Text, UserOperations.DeleteRecord, recordInfo);
                DialogResult d = frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.Cancel)
                {
                    UpdateStashTreeView();
                    AppendLog("There were no change in the leaf indices because the block is resided in the stash...", false);
                    AppendLog("Modification operation for record ID(" + txtRecordID.Text + ") has been cancelled...", true);
                }
                else
                {
                    sw.Start();
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedRecordID = ManageSecurity.Encrypt("Dummy");
                    ClientManager.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey = ManageSecurity.Encrypt(txtObtainedRecKey.Text);
                    ClientManager.RecordIdToBlockMapList.Remove(PreviousRecordID);

                    ClientManager.NameToBlockMapList.removeNewBlockIDByFieldValue(Name, blockID);
                    ClientManager.FamilyToBlockMapList.removeNewBlockIDByFieldValue(Family, blockID);
                    ClientManager.CityToBlockMapList.removeNewBlockIDByFieldValue(City, blockID);

                    ClientManager.VacantRecordsList[blockID]++;

                    sw.Stop();
                    TimeElapsed += sw.ElapsedMilliseconds;

                    UpdateStashTreeView();

                    AppendLog("Selected record information has been omited from database successfully...", false);
                    AppendLog("Contained Block ID: " + lblContainedBlockID.Text, false);
                    AppendLog("Omited Record ID: " + PreviousRecordID, false);
                    AppendLog("Omited Key Value: " + KeyToDelete, true);
                    AppendLog("There were no change in the leaf indices because the block is resided in the stash...", false);
                    AppendLog("Time Taken to complete the process: " + TimeElapsed.ToString() + " ms", true);

                    MessageBox.Show("Selected records information has been omitted from database successfully...", "Record information omission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmServer.AppendLog("There were no change in the leaf indices because the block is resided in the stash...", true);
                }
                SetEnteredRecInfo();
            }
        }
        private void btnSqlCommand_Click(object sender, EventArgs e)
        {
            frmServerSqlCommand frm = new frmServerSqlCommand(this);
            frm.ShowDialog(this);
        }
    }
}
