using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathObliviousRam
{
    public partial class frmServerRecordContents : Form
    {
        public frmServerRecordContents(string NodeAddress, string BlockID, dbRecord recordInfo)
        {
            InitializeComponent();

            txtNodeAddress.Text = NodeAddress;
            txtBlockID.Text = BlockID;
            txtRecordID.Text = ManageSecurity.DecryptToString(recordInfo.EncryptedRecordID);
            txtKey.Text = ManageSecurity.Decrypt(recordInfo.EncryptedKey).ToString();
            txtName.Text = ManageSecurity.DecryptToString(recordInfo.EncryptedName);
            txtFamily.Text = ManageSecurity.DecryptToString(recordInfo.EncryptedFamily);
            txtEmail.Text = ManageSecurity.DecryptToString(recordInfo.EncryptedEmail);
            txtCity.Text = ManageSecurity.DecryptToString(recordInfo.EncryptedCity);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
