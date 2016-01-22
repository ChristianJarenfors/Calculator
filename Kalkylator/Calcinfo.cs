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
        public bool decimalBoolNotUsed=true, inputIsNotDone = true, isCalcSelected = false, Doubleloader = false, firstInput = true;
        public int selectedcalcmethod, oldCalcMethod;
        public string Calculations,Output;
        public Calcinfo(double CurrentSum,bool DecimalBoolNotUsed, bool InputIsNotDone, bool IsCalcSelected, bool doubleloader, bool FirstInput,int SelectedCalcMethod, int OldCalcMethod,string calculations,string output)
        {
            currentSum = CurrentSum;
            decimalBoolNotUsed = DecimalBoolNotUsed;
            inputIsNotDone = InputIsNotDone;
            isCalcSelected = IsCalcSelected;
            Doubleloader = doubleloader;
            firstInput = FirstInput;
            selectedcalcmethod = SelectedCalcMethod;
            oldCalcMethod = OldCalcMethod;
            Calculations = calculations;
            Output = output;
        }
        public Calcinfo()
        {

        }
    }
}
