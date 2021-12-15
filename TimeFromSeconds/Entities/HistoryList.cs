using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HistoryList
    {
        private List<HistoryItem> HistList { get; set; }

        //Constructor
        public HistoryList()
        {
        }//HistoryList()

        //Methods
        public void AddItem(HistoryItem item)
        {
            HistList.Add(item);
        }
        public List<HistoryItem> GetHistoryList()
        {
            return HistList;
        }//GetHistoryList()
    }//HistoryList
}//namespace
