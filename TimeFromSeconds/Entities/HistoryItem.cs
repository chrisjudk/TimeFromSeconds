using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace Entities
{
    public class HistoryItem
    {
        public decimal Input { get; private set; }
        public TFS.Unit Unit { get; private set; }
        public DateTime Time { get; private set; }

        public HistoryItem(decimal aInput, TFS.Unit aUnit, DateTime aTime)
        {
            Input = aInput;
            Unit = aUnit;
            Time = aTime;
        }//HistoryItem()

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Input);
            sb.Append("\t|\t");
            sb.Append(Unit);
            sb.Append("\t|\t");
            sb.Append(Time);
            return sb.ToString();
        }//ToString()
    }//HistoryItem
}//namespace
