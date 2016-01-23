using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace wincalcmini

{
    [Serializable()]
    class HistoryRecord:Object
    {
        //String to hold the calculation
        public string s;
        //double to hold the sum of the calculation
        public double sum;
        //Constructor set the variabels
        public HistoryRecord(string text, double Sum)
        {
            s = text;
            sum = Sum;
        }
        //Overriden ToString function
        public override string ToString()
        {
            return s + " = " + sum.ToString();
        }
    }
}
