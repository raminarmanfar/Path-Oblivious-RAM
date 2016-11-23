using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathObliviousRam
{
    public class ClientPositionMap
    {
        public string FieldValue { get; set; }
        List<int> FieldToBlockPositionMap;
        public void sortList(bool isAscending)
        {
            FieldToBlockPositionMap.Sort();
            if (!isAscending) FieldToBlockPositionMap.Reverse();
        }
        public bool isBlockExist(int BlockID)
        {
            int left = 0, right = FieldToBlockPositionMap.Count, mid;
            while (left <= right)
            {
                mid = (left + right) / 2;
                if (FieldToBlockPositionMap[mid] < BlockID) left = mid + 1;
                else if (FieldToBlockPositionMap[mid] > BlockID) right = mid - 1;
                else return true;
            }
            return false;
        }
        public ClientPositionMap(string FieldValue)
        {
            this.FieldValue = FieldValue.ToUpper();
            FieldToBlockPositionMap = new List<int>();
        }
        public ClientPositionMap(string FieldValue,int BlockID)
        {
            this.FieldValue = FieldValue.ToUpper();
            FieldToBlockPositionMap = new List<int>();
            FieldToBlockPositionMap.Add(BlockID);
        }
        public List<int> getPositionMapList()
        {
            return FieldToBlockPositionMap;
        }
        public int getPositionMapByIndex(int index)
        {
            if (index < FieldToBlockPositionMap.Count) return FieldToBlockPositionMap[index];
            return -1;
        }
        public void addNewPosMap(int BlockID)
        {
            FieldToBlockPositionMap.Add(BlockID);
        }
        public void removePosMap(int BlockID)
        {
            FieldToBlockPositionMap.Remove(BlockID);
        }
    }
}
