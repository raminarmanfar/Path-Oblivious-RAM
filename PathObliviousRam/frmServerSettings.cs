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
using System.IO;

namespace PathObliviousRam
{
    public partial class frmServerSettings : Form
    {
        frmServerSide frmParent;
        public frmServerSettings(frmServerSide frmParent)
        {
            InitializeComponent();
            btnSave.Enabled = false;
            this.frmParent = frmParent;
            nudTreeHeight.Value = ServerManager.TreeHeight;
            nudChildsPerNode.Value = ServerManager.NoOfChildsPerNode;
            nudBlocksPerBucket.Value = ServerManager.NoOfBlocksPerBucket;
            nudRecordsPerBlock.Value = ServerManager.NoOfRecordsPerBlock;
            nudKeySizeInBits.Value = ServerManager.KeySize;
            switch (ServerManager.keysValueAssignType)
            {
                case KeysValueAssignType.Zero: cbKeyInitialValue.SelectedIndex = 0; break;
                case KeysValueAssignType.One: cbKeyInitialValue.SelectedIndex = 1; break;
                case KeysValueAssignType.Randomly: cbKeyInitialValue.SelectedIndex = 2; break;
            }
            SetTotalInformation();
            BigInteger totalRecords = ManagerCalculation.GetTotalRecords((int)nudTreeHeight.Value, (int)nudChildsPerNode.Value, (int)nudBlocksPerBucket.Value, (int)nudRecordsPerBlock.Value);
            nudNoOfRecordsToGenerate.Value = (decimal)totalRecords / 2;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void nudTreeHeight_ValueChanged(object sender, EventArgs e)
        {
            SetTotalInformation();
        }

        public void SetTotalInformation()
        {
            txtTotalNodes.Text = ManagerCalculation.GetTreeNodeCounts((int)nudTreeHeight.Value, (int)nudChildsPerNode.Value).ToString();
            txtTotalBlocks.Text = ManagerCalculation.GetTotalBlocks((int)nudTreeHeight.Value, (int)nudChildsPerNode.Value, (int)nudBlocksPerBucket.Value).ToString();
            BigInteger totalRecords = ManagerCalculation.GetTotalRecords((int)nudTreeHeight.Value, (int)nudChildsPerNode.Value, (int)nudBlocksPerBucket.Value, (int)nudRecordsPerBlock.Value);
            txtTotalRecords.Text = totalRecords.ToString();
            nudNoOfRecordsToGenerate.Maximum = (decimal)totalRecords;
        }

        private void nudTreeHeight_KeyUp(object sender, KeyEventArgs e)
        {
            SetTotalInformation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ServerManager.Initialize((int)nudTreeHeight.Value, (int)nudChildsPerNode.Value, (int)nudBlocksPerBucket.Value, (int)nudRecordsPerBlock.Value, (int)nudKeySizeInBits.Value, KeysValueAssignType.Randomly, (int)nudNoOfRecordsToGenerate.Value, (int)nudRandNameCount.Value, (int)nudRandFamilyCount.Value, (int)nudRandCityCount.Value);
            Close();
        }

        private void txtTotalRecords_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nudNoOfRecordsToGenerate.Maximum = Convert.ToDecimal(txtTotalRecords.Text);
                nudNoOfRecordsToGenerate.Value = nudNoOfRecordsToGenerate.Maximum / 2;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnLoadRecords_Click(object sender, EventArgs e)
        {
            Stream fileStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Title = "Load Records Information";

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                RecordInfoList.removeAll();
                int recCountToFetch = (int)nudNoOfRecordsToGenerate.Value;
                try
                {
                    if ((fileStream = openFileDialog1.OpenFile()) != null)
                    {
                        // Insert code to read the stream here.
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            string data = "-";
                            while (--recCountToFetch >= 0)
                            {
                                // Read the first line from the file and write it the textbox.
                                data = reader.ReadLine();
                                if (data.ToUpper().Contains("USERID"))
                                {
                                    data = reader.ReadLine();
                                }
                                if (data.Length == 0) break;
                                string[] strs = data.Split(',');

                                RecordInfoList.addNewRecord(new RecordInfo(strs[1], strs[2], strs[3], strs[4]));
                            }
                        }
                        btnSave.Enabled = true;
                        if (recCountToFetch > 0)
                        {
                            //nudRandNameCount.Value -= recCountToFetch;
                            MessageBox.Show("Records Information have been loaded successfully...\r\nNote: There were not enough records in the file.", "Load record information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Records Information have been loaded successfully...", "Load record information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk.\r\nOriginal error: " + ex.Message, "Load Records information error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Cursor = Cursors.Arrow;
            }
        }
    }
}
