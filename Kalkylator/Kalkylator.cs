using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace wincalcmini
{
    //two diffrent delegates
    delegate void NegativeEventThrower(double dd);
    delegate void ClearEventThrower();
    class Kalkylator
    {
        //Events to be used
        public event NegativeEventThrower Negative;
        public event ClearEventThrower ClearButton;
        //References to be used through the program
        MethodInfo[] Methods;
        Calcinfo CI;
        //Constructor sets the CalcInfo  and sets up the MethodInfo[] Methods
        public Kalkylator(Calcinfo CCI)
        {
            CI = CCI;
            var DLL = Assembly.LoadFrom("WinCalc.dll");
            var TH = DLL.GetType("WinCalc.Räknare");
            Methods = TH.GetMethods();
        }/// <summary>
        /// 2.Addition 3.Substraction 4.Multiplikation 5.Division
        /// </summary>
        /// <param name="switchis">Choose between the Arithmetics</param>
        /// <param name="inputdata">The number to count with</param>
        /// <returns></returns>
        public void Calculations(int switchis,double inputdata)
        {
            double returner = 0;
            switch (switchis)
            {
                //Addition 
                case 2:
                    {
                    returner= double.Parse(Methods[(switchis )].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
                //Substraction
                case 3:
                    {
                    returner = double.Parse(Methods[(switchis)].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
                //Multiplication
                case 4:
                    {
                    returner = double.Parse(Methods[(switchis )].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
                //Division
                case 5:
                    {
                        if (inputdata == 0)
                        {
                            //This is a joke. Could be set to anything. in any case dividing by 0 is an infinity
                            //soooo...  ...
                            //returner = 1337;

                            CI.Creset();
                        }

                        else
                        {
                            returner = double.Parse(Methods[(switchis)].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        }
                        break;
                    }
                default:
                {
                    break;
                }
            }
            //If the answer is less then 0 throw the Negative event with returner as parameter
            if (returner < 0)
            {
                Negative(returner);
            }
            //finaly set CI.currentsum to the value
            CI.currentSum = returner;
        }
        //Gets Pi from the reflected method
        public double PI()
        {
            return double.Parse(Methods[0].Invoke(null, new object[] { }).ToString());
        }
        //Gets Euler from the reflected method
        public double Euler()
        {
            return double.Parse(Methods[1].Invoke(null, new object[] { }).ToString());
        }
        //Calculates the squareroot from the reflected method with the input dd
        public double SquareRot(double dd)
        {
            return double.Parse(Methods[6].Invoke(null, new object[] {dd }).ToString());
        }
        //Calculates the Power2 from the reflected method with the input dd
        public double Power2(double dd)
        {
            return double.Parse(Methods[7].Invoke(null, new object[] { dd }).ToString());
        }
        //Inverts the inputdata dd
        public double Invert(double dd)
        {
            return dd * (-1);
        }

        //Runs the clearmethod in Calcinfo and fires the CLearButton event.
        public void ClearKey()
        {
            CI.Creset();
            ClearButton();
        }
    }
}
