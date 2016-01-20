using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wincalcmini
{
    [Serializable()]
    class Calcinfo
    {
        public double currentSum = 0;
        public bool inputIsNotDone = true, isCalcSelected = false, Doubleloader = false, firstInput = true;
        public int selectedcalcmethod, oldCalcMethod;
        public Calcinfo(double CurrentSum,bool InputIsNotDone, bool IsCalcSelected, bool doubleloader, bool FirstInput,int SelectedCalcMethod, int OldCalcMethod)
        {
            currentSum = CurrentSum;
            inputIsNotDone = InputIsNotDone;
            isCalcSelected = IsCalcSelected;
            Doubleloader = doubleloader;
            firstInput = FirstInput;
            selectedcalcmethod = SelectedCalcMethod;
            oldCalcMethod = OldCalcMethod;
        }
        public Calcinfo()
        {

        }
    }
}
