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
        //Variabels to be used in the calculator and in the form
        //Set up this way to be easier serializable

        //The old sum of past calculations
        public double currentSum = 0;
        
        //5 bools to act as flags in the program
        public bool decimalBoolNotUsed=true, inputIsNotDone = true, isCalcSelected = false, Doubleloader = false, firstInput = true;
        
        //Keeps track of the arithmetic ways to be used
        public int selectedcalcmethod, oldCalcMethod;
        
        //Keep track of the Textbox fields text when saved
        public string Calculations,Output;

        //Constructor mainly used for first uses.
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
        //empty constructor just in case
        public Calcinfo()
        {

        }

        //Method for easier updating of the string variabels
        public void UpdateOutputInfo(string calc,string output)
        {
            Calculations = calc;
            Output = output;
        }
        //Sets  the current sum and the flags to initial values
        public void Creset()
        {
            currentSum = 0;
            decimalBoolNotUsed = true;
            isCalcSelected = false;
            inputIsNotDone = true;
            firstInput = true;
            Doubleloader = false;
            selectedcalcmethod = 2;
        }
    }
}
