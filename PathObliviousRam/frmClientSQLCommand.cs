using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PathObliviousRam
{
    public partial class frmServerSqlCommand : Form
    {
        frmClientSide frmParent;
        public frmServerSqlCommand(frmClientSide frmParent)
        {
            this.frmParent = frmParent;
            InitializeComponent();
        }
        public void AppendLog(string Message, bool putLine)
        {
            txtLog.Text += ">>> " + Message + "\r\n";
            if (putLine)
                txtLog.Text += "--------------------------------------------------------------------------------------------------------------\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            // Creating a Excel object. 
            Excel._Application excel = new Excel.Application();
            Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "ExportedFromDatGrid";
                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                worksheet.Cells.EntireColumn.NumberFormat = "@";

                //Loop through each row and read value from each column. 
                for (int i = 0; i <= dgvRecords.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvRecords.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvRecords.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvRecords.Rows[i - 1].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Query Result Export to Excel";
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Query result has been exported to excel file successfully...", "Query Result Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on result export to excel file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }
        enum FieldPart { Name = 0, Value = 1 }
        private List<String>[] ParseSQLQuery(string QueryCommand)
        {
            Stack stack = new Stack();
            List<string> postfixExpression = new List<string>();
            StringBuilder SQLExp = new StringBuilder(QueryCommand.ToUpper());
            int i;
            for (i = 0; i < SQLExp.Length; i++)
            {
                if (SQLExp[i] == 32)
                {
                    int j = i + 1;
                    while (SQLExp[j] == 32)
                    {
                        SQLExp = SQLExp.Remove(j, 1);
                        if (j >= SQLExp.Length) j = SQLExp.Length - 1;
                    }
                }
            }

            for (i = 0; i < SQLExp.Length; i++)
            {
                if (SQLExp[i] == '=' || SQLExp[i] == '(' || SQLExp[i] == ')')
                {
                    if (i + 1 < SQLExp.Length && SQLExp[i + 1] == 32) SQLExp = SQLExp.Remove(i + 1, 1);
                    if (i > 0 && SQLExp[i - 1] == 32) SQLExp = SQLExp.Remove(i - 1, 1);
                }
            }

            SQLExp = SQLExp.Replace("AND", "&");
            SQLExp = SQLExp.Replace("OR", "|");
            for (i = 0; i < SQLExp.Length; i++)
            {
                if (SQLExp[i] == 32)
                {
                    if ((SQLExp[i + 1] == '&') || (SQLExp[i + 1] == '|'))
                    {
                        SQLExp[i] = ')';
                        for (int j = i; j >= 0; j--)
                        {
                            if (SQLExp[j] == '(' || j == 0)
                            {
                                SQLExp.Insert(j, '(');
                                break;
                            }
                        }
                    }
                    else if ((SQLExp[i - 1] == '&') || (SQLExp[i - 1] == '|'))
                    {
                        SQLExp[i] = '(';
                        for (int j = i; j < SQLExp.Length; j++)
                        {
                            if (SQLExp[j] == ')')
                            {
                                SQLExp.Insert(j, ')');
                                break;
                            }
                            else if (j == SQLExp.Length - 1)
                            {
                                SQLExp.Append(')');
                                break;
                            }
                            else if (SQLExp[j] == 32)
                            {
                                SQLExp[j] = ')';
                                break;
                            }
                        }
                    }
                }
            }
            SQLExp.Insert(0, '(');
            SQLExp.Append(')');
            string exp;
            i = 0;
            while (i < SQLExp.Length)
            {
                switch (SQLExp[i])
                {
                    case '(': stack.Push(SQLExp[i]); break;
                    case ')':
                        if (stack.Count <= 0 || !stack.Contains('(')) return null;
                        if ((char)stack.Peek() == '&' || (char)stack.Peek() == '|')
                        {
                            while (stack.Count > 0 && (char)stack.Peek() != '(')
                            {
                                exp = stack.Pop().ToString();
                                postfixExpression.Add(exp);
                            }
                        }
                        else if ((char)stack.Peek() != '(')
                        {
                            exp = string.Empty;
                            while (stack.Count > 0 && (char)stack.Peek() != '(')
                            {
                                exp = stack.Pop() + exp;
                            }
                            postfixExpression.Add(exp);
                        }

                        if (stack.Count <= 0) return null;
                        else if ((char)stack.Peek() == '(') stack.Pop();
                        break;
                    default: stack.Push(SQLExp[i]); break;

                }
                i++;
            }
            if (stack.Contains('(')) return null;

            List<string>[] expressionsList = new List<string>[2];//--- first entry if for fields name & second one for fields values
            expressionsList[0] = new List<string>();
            expressionsList[1] = new List<string>();
            foreach (string expression in postfixExpression)
            {
                if (expression != "&" && expression != "|")
                {
                    string tmp = expression.Substring(0, expression.IndexOf("="));
                    if (tmp != "NAME" && tmp != "FAMILY" && tmp != "CITY") return null;
                    else expressionsList[(int)FieldPart.Name].Add(tmp);
                    tmp = expression.Substring(expression.IndexOf("=") + 1);
                    expressionsList[(int)FieldPart.Value].Add(tmp.Substring(1, tmp.Length - 2));
                }
                else
                {
                    expressionsList[(int)FieldPart.Name].Add(expression);
                    expressionsList[(int)FieldPart.Value].Add(expression);
                }
                //txtLog.Text += expression + "\r\n";
            }

            return expressionsList;
        }
        private void ShowRecords(List<ClientRetrivedRecord> ClientRetrivedRecords)
        {
            dgvRecords.Rows.Clear();
            foreach (ClientRetrivedRecord rec in ClientRetrivedRecords)
            {
                dgvRecords.Rows.Add(rec.LeafNodeIndex, rec.BlockID, rec.RecordID, rec.Name, rec.Family, rec.Email, rec.City, rec.Key);
            }
        }
        private ClientPositionMap GetPositionMapList(string FieldName, string FieldValue)
        {
            ClientPositionMap clientPosMap = null;
            switch (FieldName)
            {
                case "RECORDID":
                    //clientPosMap = ClientManager.RecordIdToBlockMapList.getPositionMapList(FieldValue);
                    break;
                case "NAME":
                    clientPosMap = ClientManager.NameToBlockMapList.getPositionMapList(FieldValue);
                    break;
                case "FAMILY":
                    clientPosMap = ClientManager.FamilyToBlockMapList.getPositionMapList(FieldValue);
                    break;
                case "CITY":
                    clientPosMap = ClientManager.CityToBlockMapList.getPositionMapList(FieldValue);
                    break;
            }
            return clientPosMap;
        }
        private List<ClientRetrivedRecord> RunQueryWithOneCondition(string FieldName, string FieldValue)
        {
            ClientPositionMap clientPosMap = GetPositionMapList(FieldName, FieldValue);
            AppendLog("SQL Command: " + txtSQLCommand.Text + " WHERE " + txtSQLCondition.Text, false);
            if (clientPosMap == null)
            {
                AppendLog("Given value has not been found in the database!!!", true);
                return null;
            }
            else
            {
                List<ClientRetrivedRecord> ClientRetrivedRecords = new List<ClientRetrivedRecord>();
                clientPosMap.sortList(true);
                List<int> posList = clientPosMap.getPositionMapList().ToList();
                while (posList.Count > 0)
                {
                    int BlockID = posList[0];

                    //--- check whether requested blockID is in stash or not ---
                    int PreviousLeafNodeIndex = -1;
                    int BlockIndexInStash = ClientStash.GetBlockIndexByID(BlockID);
                    //--- block is NOT in the stash ---
                    if (BlockIndexInStash == -1) PreviousLeafNodeIndex = ClientManager.AccessGet(BlockID);

                    //List<dbRecord> recordsList = ClientManager.GetRequestedRecordsByField(FieldName, FieldValue);
                    foreach (dbBlock block in ClientStash.GetAllBlocks())
                    {
                        if (posList.Contains(block.BlockID))
                        {
                            string EncryptedFieldValue = ManageSecurity.Encrypt(FieldValue);
                            List<dbRecord> recordsList = block.GetRecordsByFieldName(FieldName, EncryptedFieldValue);
                            foreach (dbRecord rec in recordsList)
                            {
                                string RecordID = ManageSecurity.DecryptToString(rec.EncryptedRecordID).ToString();
                                string Key = ManageSecurity.Decrypt(rec.EncryptedKey).ToString();
                                string Name = ManageSecurity.DecryptToString(rec.EncryptedName);
                                string Family = ManageSecurity.DecryptToString(rec.EncryptedFamily);
                                string Email = ManageSecurity.DecryptToString(rec.EncryptedEmail);
                                string City = ManageSecurity.DecryptToString(rec.EncryptedCity);
                                ClientRetrivedRecords.Add(new ClientRetrivedRecord(PreviousLeafNodeIndex, block.BlockID, RecordID, Name, Family, Email, City, Key));
                            }
                            while (posList.Count > 0 && posList.Contains(block.BlockID)) posList.Remove(block.BlockID);
                        }
                    }
                    if (BlockIndexInStash == -1) ClientManager.AccessPut(BlockID, PreviousLeafNodeIndex);
                }
                int RetrievedRecordsCount = clientPosMap.getPositionMapList().Count;
                AppendLog("Totally " + RetrievedRecordsCount.ToString() + " were found for a given query...", true);
                return ClientRetrivedRecords;
            }
        }
        private bool checkForValue(dbRecord rec, string FieldName, string FieldValue)
        {
            string EncryptedFieldValue = ManageSecurity.Encrypt(FieldValue);
            switch (FieldName.ToUpper())
            {
                case "RECORDID":
                    if (rec.EncryptedRecordID == EncryptedFieldValue) return true;
                    break;
                case "NAME":
                    if (rec.EncryptedName == EncryptedFieldValue) return true;
                    break;
                case "FAMILY":
                    if (rec.EncryptedFamily == EncryptedFieldValue) return true;
                    break;
                case "CITY":
                    if (rec.EncryptedCity == EncryptedFieldValue) return true;
                    break;
            }
            return false;
        }
        private bool checkForValue(ClientRetrivedRecord rec, string FieldName, string FieldValue)
        {
            switch (FieldName.ToUpper())
            {
                case "RECORDID":
                    if (rec.RecordID == FieldValue) return true;
                    break;
                case "NAME":
                    if (rec.Name == FieldValue) return true;
                    break;
                case "FAMILY":
                    if (rec.Family == FieldValue) return true;
                    break;
                case "CITY":
                    if (rec.City == FieldValue) return true;
                    break;
            }
            return false;
        }
        private List<ClientRetrivedRecord> RunANDQuery(string FieldNameA, string FieldValueA, string FieldNameB, string FieldValueB,bool NOT)
        {
            ClientPositionMap clientPosMap = GetPositionMapList(FieldNameA, FieldValueA);
            if (clientPosMap == null)
            {
                AppendLog("Given value has not been found in the database!!!", true);
                return null;
            }
            else
            {
                List<ClientRetrivedRecord> ClientRetrivedRecords = new List<ClientRetrivedRecord>();
                clientPosMap.sortList(true);
                List<int> posList = clientPosMap.getPositionMapList().ToList();
                while (posList.Count > 0)
                {
                    int BlockID = posList[0];

                    //--- check whether requested blockID is in stash or not ---
                    int PreviousLeafNodeIndex = -1;
                    int BlockIndexInStash = ClientStash.GetBlockIndexByID(BlockID);
                    //--- block is NOT in the stash ---
                    if (BlockIndexInStash == -1) PreviousLeafNodeIndex = ClientManager.AccessGet(BlockID);

                    //List<dbRecord> recordsList = ClientManager.GetRequestedRecordsByField(FieldName, FieldValue);
                    foreach (dbBlock block in ClientStash.GetAllBlocks())
                    {
                        if (posList.Contains(block.BlockID))
                        {
                            string EncryptedFieldValueA = ManageSecurity.Encrypt(FieldValueA);
                            List<dbRecord> recordsList = block.GetRecordsByFieldName(FieldNameA, EncryptedFieldValueA);
                            foreach (dbRecord rec in recordsList)
                            {
                                if (NOT ^ checkForValue(rec, FieldNameB, FieldValueB))
                                {
                                    string RecordID = ManageSecurity.DecryptToString(rec.EncryptedRecordID).ToString();
                                    string Key = ManageSecurity.Decrypt(rec.EncryptedKey).ToString();
                                    string Name = ManageSecurity.DecryptToString(rec.EncryptedName);
                                    string Family = ManageSecurity.DecryptToString(rec.EncryptedFamily);
                                    string Email = ManageSecurity.DecryptToString(rec.EncryptedEmail);
                                    string City = ManageSecurity.DecryptToString(rec.EncryptedCity);
                                    ClientRetrivedRecords.Add(new ClientRetrivedRecord(PreviousLeafNodeIndex, block.BlockID, RecordID, Name, Family, Email, City, Key));
                                }
                            }
                            while (posList.Count > 0 && posList.Contains(block.BlockID)) posList.Remove(block.BlockID);
                        }
                    }
                    if (BlockIndexInStash == -1) ClientManager.AccessPut(BlockID, PreviousLeafNodeIndex);
                }
                int RetrievedRecordsCount = clientPosMap.getPositionMapList().Count;
                AppendLog("Totally " + RetrievedRecordsCount.ToString() + " were found for a given query...", true);
                return ClientRetrivedRecords;
            }
        }
        private List<ClientRetrivedRecord> joinTables(List<ClientRetrivedRecord> tblA, List<ClientRetrivedRecord> tblB, char Operator)
        {
            if (Operator == '&')
            {
                List<ClientRetrivedRecord> resultTbl = new List<ClientRetrivedRecord>();
                foreach (ClientRetrivedRecord retRecordA in tblA)
                {
                    foreach (ClientRetrivedRecord retRecordB in tblB)
                    {
                        if (retRecordA.RecordID == retRecordB.RecordID)
                        {
                            resultTbl.Add(retRecordA); //--- or resultTbl.Add(retRecordB);
                        }
                    }
                }
                return resultTbl;
            }
            else if (Operator == '|')
            {
                foreach (ClientRetrivedRecord retRecordA in tblA)
                {
                    bool found = false;
                    foreach (ClientRetrivedRecord retRecordB in tblB)
                    {
                        if (retRecordA.RecordID == retRecordB.RecordID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        tblB.Add(retRecordA);
                    }
                }
                return tblB;
            }
            else return null;
        }
        private List<ClientRetrivedRecord> RunORQuery(string FieldNameA, string FieldValueA, string FieldNameB, string FieldValueB)
        {
            List<ClientRetrivedRecord> RetrivedRecordsA = RunQueryWithOneCondition(FieldNameA, FieldValueA);
            List<ClientRetrivedRecord> RetrivedRecordsB = RunQueryWithOneCondition(FieldNameB, FieldValueB);

            return joinTables(RetrivedRecordsA, RetrivedRecordsB, '|');
        }
        private List<ClientRetrivedRecord> RunORQuery2(string FieldNameA, string FieldValueA, string FieldNameB, string FieldValueB)
        {
            List<ClientRetrivedRecord> ClientRetrivedRecordsA = RunQueryWithOneCondition(FieldNameA, FieldValueA);
            List<ClientRetrivedRecord> ClientRetrivedRecordsB = RunANDQuery(FieldNameB, FieldValueB, FieldNameA, FieldValueA, true);

            return ClientRetrivedRecordsA.Concat(ClientRetrivedRecordsB).Distinct().ToList();
        }
        private List<ClientRetrivedRecord> OperationOnTables(List<ClientRetrivedRecord> tblA, string FieldName, string FieldValue, char Operator)
        {
            List<ClientRetrivedRecord> resultTbl = null;
            if (Operator == '&')
            {
                resultTbl = new List<ClientRetrivedRecord>();
                foreach (ClientRetrivedRecord rec in tblA)
                {
                    if (checkForValue(rec, FieldName, FieldValue))
                    {
                        resultTbl.Add(rec);
                    }
                }
            }
            else if (Operator == '|')
            {
                List<ClientRetrivedRecord> tblB = RunQueryWithOneCondition(FieldName, FieldValue);
                resultTbl = joinTables(tblA, tblB, Operator).Distinct().ToList();
            }
            else return null;

            return resultTbl;
        }
        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            List<List<ClientRetrivedRecord>> RetrievedTables = new List<List<ClientRetrivedRecord>>();
            bool ErrorFound = false;
            dgvRecords.Rows.Clear();
            List<string>[] expressionsList = ParseSQLQuery(txtSQLCondition.Text);

            //--- evaluate expressions ---
            if (expressionsList == null)
            {
                ErrorFound = true;
                MessageBox.Show("Error on experision....", "Error on entered experision", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                //ClientManager.CityToBlockMapList
                if (expressionsList[0].Count == 1)
                {
                    sw.Start();

                    string FieldName = expressionsList[(int)FieldPart.Name][0];
                    string FieldValue = expressionsList[(int)FieldPart.Value][0];
                    List<ClientRetrivedRecord> RetrievedRecords = RunQueryWithOneCondition(FieldName, FieldValue);
                    RetrievedTables.Add(RetrievedRecords);
                }
                else
                {
                    AppendLog("SQL Command: " + txtSQLCommand.Text + " WHERE " + txtSQLCondition.Text, false);

                    sw.Start();

                    Stack FieldsNameStack = new Stack();
                    Stack FieldsValueStack = new Stack();
                    int token = 0;
                    while (token < expressionsList[(int)FieldPart.Name].Count)
                    {
                        if(expressionsList[(int)FieldPart.Name][token] != "&" && expressionsList[(int)FieldPart.Name][token] != "|")
                        {
                            FieldsNameStack.Push(expressionsList[(int)FieldPart.Name][token]);
                            FieldsValueStack.Push(expressionsList[(int)FieldPart.Value][token]);
                            token++;
                        }
                        else//--- token is an operator (either & or |)
                        {
                            char op = expressionsList[(int)FieldPart.Name][token][0];
                            if (FieldsNameStack.Count > 1)//--- two or more operand are in the stack
                            {
                                string FieldNameA = (string)FieldsNameStack.Pop();
                                string FieldValueA = (string)FieldsValueStack.Pop();
                                string FieldNameB = (string)FieldsNameStack.Pop();
                                string FieldValueB = (string)FieldsValueStack.Pop();
                                List<ClientRetrivedRecord> RetrivedRecords = null;
                                if (op == '&')
                                    RetrivedRecords = RunANDQuery(FieldNameA, FieldValueA, FieldNameB, FieldValueB, false);
                                else if (op == '|')
                                    RetrivedRecords = RunORQuery(FieldNameA, FieldValueA, FieldNameB, FieldValueB);

                                RetrievedTables.Add(RetrivedRecords);
                            }
                            else if (FieldsNameStack.Count == 1 && RetrievedTables.Count > 0)
                            {
                                string FieldName = (string)FieldsNameStack.Pop();
                                string FieldValue = (string)FieldsValueStack.Pop();

                                List<ClientRetrivedRecord> result = OperationOnTables(RetrievedTables[RetrievedTables.Count - 1], FieldName, FieldValue, op);
                                RetrievedTables.RemoveAt(RetrievedTables.Count - 1);
                                RetrievedTables.Add(result);
                            }
                            else if (FieldsNameStack.Count == 0 && RetrievedTables.Count > 0)
                            {
                                List<ClientRetrivedRecord> tblA = RetrievedTables[RetrievedTables.Count - 1];
                                List<ClientRetrivedRecord> tblB = RetrievedTables[RetrievedTables.Count - 2];
                                List<ClientRetrivedRecord> result = joinTables(tblA, tblB, op);
                                RetrievedTables.RemoveAt(RetrievedTables.Count - 1);
                                RetrievedTables.RemoveAt(RetrievedTables.Count - 1);
                                RetrievedTables.Add(result);
                            }
                            else
                            {
                                ErrorFound = true;
                                MessageBox.Show("Error on experision....", "Error on entered experision", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            token++;
                        }
                    }

                    sw.Stop();
                }
                if (RetrievedTables.Count > 1)
                {
                    ErrorFound = true;
                    MessageBox.Show("There is an error on query process...\r\nDescription: Number of tables are more than one", "Query operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!ErrorFound)
                {
                    txtTimeTaken.Text = sw.ElapsedMilliseconds.ToString();
                    txtRetrievedRecordsCount.Text = RetrievedTables[0].Count.ToString();
                    ShowRecords(RetrievedTables[0]);
                    MessageBox.Show("Query has been proceed successfully...", "Query operation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            frmParent.UpdateStashTreeView();
        }
    }
}