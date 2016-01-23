using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace wincalcmini
{
    class Logger
    {
        //References to be used
        Stream flow;
        StreamWriter SW;
        Kalkylator Calc;
        //Constructor that sets up the Kalkylator reference with the the kalkylator object used elsewhere (in Form1)
        //also adds two Methods to the events in the kalkylator object
        public Logger(Kalkylator calc)
        {
            Calc = calc;
            calc.Negative += NegativeResult;
            calc.ClearButton += ClearPressing;
        }
        public void NegativeResult(double dd)
        {
            //Opens a stream and sets a streamwriter to it
            flow = File.Open("Logg.txt", FileMode.Append, FileAccess.Write);
            SW = new StreamWriter(flow);

            //Writes the information in the dokument and the number of this instance (dd)
            SW.WriteLine(DateTime.Now + "   Negative number: " + dd);

            //FLush and close the Streamwriter and the stream
            flow.Flush();
            SW.Flush();
            SW.Close();
            flow.Close();
        }
        public void ClearPressing()
        {
            //Opens a stream and sets a streamwriter to it
            flow = File.Open("Logg.txt", FileMode.Append, FileAccess.Write);
            SW = new StreamWriter(flow);

            //Writes the information in the dokument
            SW.WriteLine(DateTime.Now + "   Canceled Calculation ");
            
            //FLush and close the Streamwriter and the stream
            flow.Flush();
            SW.Flush();
            SW.Close();
            flow.Close();
        }
    }
}
