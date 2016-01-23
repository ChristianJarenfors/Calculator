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
        Stream flow;
        StreamWriter SW;
        StreamReader SR;
        Kalkylator Calc;
        StringBuilder SB;
        public Logger(Kalkylator calc)
        {
            
            Calc = calc;
            calc.Negative += NegativeResult;
            calc.ClearButton += ClearPressing;
        }
        public void NegativeResult(double dd)
        {
            flow = File.Open("Logg.txt", FileMode.Append, FileAccess.Write);
            SW = new StreamWriter(flow);
            SW.WriteLine(DateTime.Now + "   Negative number: " + dd);

            flow.Flush();

            SW.Flush();
            SW.Close();
            flow.Close();
            //flow = File.Open("Logg.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //SR = new StreamReader(flow);
            //SB= new StringBuilder(SR.ReadToEnd());
            //SB.AppendLine(DateTime.Now + "   Negative number: " + dd);
            //flow.Flush();
            //SR.Close();
            //flow.Close();
            //flow = File.Open("Logg.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //SW = new StreamWriter(flow);
            //SW.WriteLine(SB);
            //flow.Flush();

            //SW.Flush();
            //SW.Close();
            //flow.Close();
        }
        public void ClearPressing()
        {
            //flow = File.Open("Logg.txt", FileMode.Open, FileAccess.Read);

            //SR = new StreamReader(flow);
            //SB = new StringBuilder(SR.ReadToEnd());
            //SB.AppendLine(DateTime.Now + "   Canceled Calculation ");


            //flow.Flush();
            //SR.Close();
            //flow.Close();

            //flow = File.Open("Logg.txt", FileMode.Create, FileAccess.Write);
            //SW = new StreamWriter(flow);
            //SW.WriteLine(SB);
            //flow.Flush();

            //SW.Flush();
            //SW.Close();
            //flow.Close();



            flow = File.Open("Logg.txt", FileMode.Append, FileAccess.Write);
            SW = new StreamWriter(flow);
            SW.WriteLine(DateTime.Now + "   Canceled Calculation ");

            flow.Flush();

            SW.Flush();
            SW.Close();
            flow.Close();
        }
        private void Loader()
        {

        }
    }
}
