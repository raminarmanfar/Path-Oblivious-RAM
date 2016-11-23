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

namespace PathObliviousRam
{
    public partial class frmClientAddEditRecord : Form
    {
        public static Random rnd;
        frmClientSide frmClientSide;
        int KeySizeInBit;
        string PreviousRecordID;
        BigInteger key;
        UserOperations userOperation;
        public frmClientAddEditRecord(frmClientSide frmClientSide, int KeySizeInBit, string RecordID, UserOperations userOperation, RecordInfo recordInfo)
        {
            InitializeComponent();
            this.userOperation = userOperation;
            this.frmClientSide = frmClientSide;
            this.KeySizeInBit = KeySizeInBit;
            PreviousRecordID = RecordID;

            rnd = new Random(DateTime.Now.Millisecond);

            switch (userOperation)
            {
                case UserOperations.AddRecord:
                    Text = lblTitle.Text = "Add New Record To Database";
                    pictureBox1.Image = Properties.Resources.database_add_insert_21836;
                    btnSaveChanges.Text = "Add Record";
                    btnSaveChanges.Image = Properties.Resources.system_database_add_icon_48;

                    //-------> block id should be added to here
                    txtRecordID.Text = txtEditedKey.Text = string.Empty;
                    txtLeafNodeIndex.Text = ClientManager.BlockToBuketPositionMapList[ClientManager.RequestedDataBlock.BlockID].ToString();
                    txtBlockID.Text = ClientManager.RequestedDataBlock.BlockID.ToString();
                    txtName.Text = txtFamily.Text = txtEmail.Text = txtCity.Text = string.Empty;
                    break;
                case UserOperations.ModifyRecord:
                    Text = lblTitle.Text = "Modify Selected Record Information";
                    pictureBox1.Image = Properties.Resources.edit;
                    btnSaveChanges.Text = "Save Changes";
                    btnSaveChanges.Image = Properties.Resources.save_as_2_48;

                    txtLeafNodeIndex.Text = ClientManager.BlockToBuketPositionMapList[ClientManager.RequestedDataBlock.BlockID].ToString();
                    txtBlockID.Text = ClientManager.RequestedDataBlock.BlockID.ToString();
                    txtRecordID.Text = RecordID.ToString();
                    key = ManageSecurity.Decrypt(ClientManager.GetRequestedRecord(RecordID).EncryptedKey);
                    txtName.Text = recordInfo.Name;
                    txtFamily.Text = recordInfo.Family;
                    txtEmail.Text = recordInfo.Email;
                    txtCity.Text = recordInfo.City;
                    /*
                    txtName.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedName);
                    txtFamily.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedFamily);
                    txtEmail.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedEmail);
                    txtCity.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedCity);
                    */
                    break;
                case UserOperations.DeleteRecord:
                    Text = lblTitle.Text = "Delete Selected Record From Database";
                    pictureBox1.Image = Properties.Resources.Misc_Delete_Database_icon_256;
                    txtRecordID.TabStop = txtEditedKey.TabStop = txtName.TabStop = txtFamily.TabStop = txtEmail.TabStop = txtCity.TabStop = false;
                    txtRecordID.ReadOnly = txtEditedKey.ReadOnly = txtName.ReadOnly = txtFamily.ReadOnly = txtEmail.ReadOnly = txtCity.ReadOnly = true;
                    txtRecordID.BackColor = txtEditedKey.BackColor = txtName.BackColor = txtFamily.BackColor = txtEmail.BackColor = txtCity.BackColor = Color.FromArgb(255, 255, 192);
                    txtRecordID.ForeColor = txtEditedKey.ForeColor = txtName.ForeColor = txtFamily.ForeColor = txtEmail.ForeColor = txtCity.ForeColor = Color.Blue;
                    btnSaveChanges.Text = "Delete Record";
                    btnSaveChanges.Image = Properties.Resources.Misc_Delete_Database_icon_48;
                    btnGenerateRandomKey.Enabled = false;

                    txtLeafNodeIndex.Text = ClientManager.BlockToBuketPositionMapList[ClientManager.RequestedDataBlock.BlockID].ToString();
                    txtBlockID.Text = ClientManager.RequestedDataBlock.BlockID.ToString();
                    txtRecordID.Text = RecordID.ToString();
                    key = ManageSecurity.Decrypt(ClientManager.GetRequestedRecord(RecordID).EncryptedKey);

                    txtName.Text = recordInfo.Name;
                    txtFamily.Text = recordInfo.Family;
                    txtEmail.Text = recordInfo.Email;
                    txtCity.Text = recordInfo.City;

                    /*
                    txtName.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedName);
                    txtFamily.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedFamily);
                    txtEmail.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedEmail);
                    txtCity.Text = ManageSecurity.DecryptToString(ClientManager.GetRequestedRecord(RecordID).EncryptedCity);
                    */
                    break;
            }

            txtKeySizeInBit.Text = KeySizeInBit.ToString();
            txtEditedKey.Text = key.ToString();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (userOperation == UserOperations.AddRecord) frmClientSide.SetRecordInfo(string.Empty, BigInteger.Zero, null);
            this.Close();
        }

        private BigInteger GenerateRandomKey(int KeySizeInBit)
        {
            int len = (int)Math.Ceiling(KeySizeInBit / 8.0);
            if (KeySizeInBit % 8 == 0) len++;
            byte mask = (byte)(Math.Pow(2, KeySizeInBit % 8) - 1);
            BigInteger minNum = new BigInteger();
            minNum = BigInteger.Pow(2, KeySizeInBit - 1);
            BigInteger maxNum = new BigInteger();
            maxNum = BigInteger.Pow(2, KeySizeInBit);
            byte[] rndBytes = new byte[len];
            BigInteger Key;
            bool isGrader, isLess;
            do
            {
                rnd.NextBytes(rndBytes);
                rndBytes[len - 1] &= mask;
                Key = new BigInteger(rndBytes);
                isGrader = isLess = false;
                if (BigInteger.Compare(Key, maxNum) == 1) isGrader = true;
                if (BigInteger.Compare(Key, minNum) == -1) isLess = true;
            } while (/*db.Contains(tmp) ||*/ isGrader || isLess);
            return Key;
        }
        private void btnGenerateRandomKey_Click(object sender, EventArgs e)
        {
            key = GenerateRandomKey(KeySizeInBit);
            txtEditedKey.Text = key.ToString();
        }
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            key = BigInteger.Parse(txtEditedKey.Text);
            switch (userOperation)
            {
                case UserOperations.AddRecord:
                    if (ClientManager.RecordIdToBlockMapList.Keys.Contains(txtRecordID.Text) && txtRecordID.Text != PreviousRecordID)
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Entered record ID is already exist in the database!!!\r\nPlease Try another unique record Id.", "Record Id is not unique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        frmClientSide.SetRecordInfo(txtRecordID.Text, BigInteger.Parse(txtEditedKey.Text), new RecordInfo(txtName.Text, txtFamily.Text, txtEmail.Text, txtCity.Text));
                    }
                    break;
                case UserOperations.ModifyRecord:
                    if (ClientManager.RecordIdToBlockMapList.Keys.Contains(txtRecordID.Text) && txtRecordID.Text!=PreviousRecordID)
                    {
                        DialogResult = DialogResult.None;
                        MessageBox.Show("Entered record ID is already exist in the database!!!\r\nPlease Try another unique record Id.", "Record Id is not unique", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        frmClientSide.SetRecordInfo(txtRecordID.Text, BigInteger.Parse(txtEditedKey.Text), new RecordInfo(txtName.Text, txtFamily.Text, txtEmail.Text, txtCity.Text));
                    }
                    break;
                case UserOperations.DeleteRecord:
                    DialogResult = DialogResult.OK;
                    frmClientSide.SetRecordInfo(string.Empty, BigInteger.Zero, null);

                    /*
                    EncryptedRecordID = ClassSecurity.Encrypt(PreviousRecordID);
                    RequestedRecordIndex = ClassClient.RequestedDataBlock.GetRecordIndexByEncryptedRecordID(EncryptedRecordID);
                    ClassClient.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedKey = ClassSecurity.Encrypt(key);
                    ClassClient.RequestedDataBlock.GetRecord(RequestedRecordIndex).EncryptedRecordID = ClassSecurity.Encrypt(txtRecordID.Text);
                    ClassClient.ChangeKey(ClassClient.RecordToBlockMapList, PreviousRecordID, txtRecordID.Text);
                    frmClientSide.SetRecordInfo("Dummy", BigInteger.Zero);
                    */
                    break;
            }
        }
    }
}