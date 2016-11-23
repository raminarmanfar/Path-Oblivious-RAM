using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    public class ClientPositionMapList
    {
        public string FieldName { get; private set; }
        private List<ClientPositionMap> FieldToBlockPositionMapList;
        public ClientPositionMapList(string FieldName)
        {
            this.FieldName = FieldName;
            FieldToBlockPositionMapList = new List<ClientPositionMap>();
        }
        public bool addNewPositionMapList(string FieldValue)
        {
            if (getPositionMapList(FieldValue.ToUpper()) == null)
            {
                FieldToBlockPositionMapList.Add(new ClientPositionMap(FieldValue.ToUpper()));
                return true;
            }
            return false;
        }
        public ClientPositionMap getPositionMapList(string FieldValue)
        {
            for (int i = 0; i < FieldToBlockPositionMapList.Count; i++)
            {
                if (FieldToBlockPositionMapList[i].FieldValue.ToUpper() == FieldValue.ToUpper())
                {
                    return FieldToBlockPositionMapList[i];
                }
            }
            return null;
        }
        public void addNewBlockIDByFieldValue(string FieldValue, int BlockID)
        {
            ClientPositionMap posMap = getPositionMapList(FieldValue.ToUpper());
            if (posMap == null)
            {
                FieldToBlockPositionMapList.Add(new ClientPositionMap(FieldValue.ToUpper()));
            }
            else posMap.addNewPosMap(BlockID);
        }
        public bool ModifyPositionMapFieldValue(string currentFieldValue, string newFieldValue)
        {
            ClientPositionMap posMap = getPositionMapList(currentFieldValue.ToUpper());
            if (posMap == null) return false;
            posMap.FieldValue = newFieldValue.ToUpper();
            return true;
        }
        public bool removeNewBlockIDByFieldValue(string FieldValue, int BlockID)
        {
            ClientPositionMap posMap = getPositionMapList(FieldValue.ToUpper());
            if (posMap == null) return false;
            posMap.removePosMap(BlockID);
            if (posMap.getPositionMapList().Count <= 0) FieldToBlockPositionMapList.Remove(posMap);
            return true;
        }
    }
}
