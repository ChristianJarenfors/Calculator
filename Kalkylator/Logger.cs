using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wincalcmini
{
    class Logger
    {
        Kalkylator Calc;
        public Logger(Kalkylator calc)
        {
            Calc = calc;
            calc.Negative += NegativeResult;
            calc.ClearButton += ClearPressing;
        }
        public void NegativeResult()
        {

        }
        public void ClearPressing()
        {

        }

    }
}
