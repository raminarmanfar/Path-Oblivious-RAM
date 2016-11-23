using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace PathObliviousRam
{
    public class dbRecord
    {
        public string EncryptedRecordID { get; set; }
        //public BigInteger Key { get; set; }
        public string EncryptedKey { get; set; }
        public string EncryptedName { get; set; }
        public string EncryptedFamily { get; set; }
        public string EncryptedEmail { get; set; }
        public string EncryptedCity { get; set; }
        public dbRecord(string EncryptedRecordID, string EncryptedKey, string EncryptedName, string EncryptedFamily, string EncryptedEmail, string EncryptedCity)
        {
            this.EncryptedRecordID = EncryptedRecordID;
            this.EncryptedKey = EncryptedKey;
            this.EncryptedName = EncryptedName;
            this.EncryptedFamily = EncryptedFamily;
            this.EncryptedEmail = EncryptedEmail;
            this.EncryptedCity = EncryptedCity;
        }
        public dbRecord(string EncryptedRecordID, string EncryptedName, string EncryptedFamily, string EncryptedEmail, string EncryptedCity)
        {
            this.EncryptedRecordID = EncryptedRecordID;
            this.EncryptedName = EncryptedName;
            this.EncryptedFamily = EncryptedFamily;
            this.EncryptedEmail = EncryptedEmail;
            this.EncryptedCity = EncryptedCity;

            BigInteger Key = BigInteger.Zero;
            switch (ServerManager.keysValueAssignType)
            {
                case KeysValueAssignType.Zero: Key = BigInteger.Zero; break;
                case KeysValueAssignType.One: Key = BigInteger.One; break;
                case KeysValueAssignType.Randomly:
                    int len = (int)Math.Ceiling(ServerManager.KeySize / 8.0);
                    if (ServerManager.KeySize % 8 == 0) len++;
                    byte mask = (byte)(Math.Pow(2, ServerManager.KeySize % 8) - 1);
                    BigInteger minNum = new BigInteger();
                    minNum = BigInteger.Pow(2, ServerManager.KeySize - 1);
                    BigInteger maxNum = new BigInteger();
                    maxNum = BigInteger.Pow(2, ServerManager.KeySize);
                    byte[] rndBytes = new byte[len];
                    BigInteger tmp;
                    bool isGrader, isLess;
                    do
                    {
                        ServerManager.rnd.NextBytes(rndBytes);
                        rndBytes[len - 1] &= mask;
                        tmp = new BigInteger(rndBytes);
                        isGrader = isLess = false;
                        if (BigInteger.Compare(tmp, maxNum) == 1) isGrader = true;
                        if (BigInteger.Compare(tmp, minNum) == -1) isLess = true;
                    } while (/*db.Contains(tmp) ||*/ isGrader || isLess);
                    Key = tmp;
                    break;
            }
            EncryptedKey = ManageSecurity.Encrypt(Key);
        }
        public void SetRecordInfo(string EncryptedRecordID, string EncryptedKey, string EncryptedName, string EncryptedFamily, string EncryptedEmail, string EncryptedCity)
        {
            this.EncryptedRecordID = EncryptedRecordID;
            this.EncryptedKey = EncryptedKey;
            this.EncryptedName = EncryptedName;
            this.EncryptedFamily = EncryptedFamily;
            this.EncryptedEmail = EncryptedEmail;
            this.EncryptedCity = EncryptedCity;

        }
    }
}
