using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace wincalcmini
{
    delegate void NegativeEventThrower(double dd);
    delegate void ClearEventThrower();
    class Kalkylator
    {
        public event NegativeEventThrower Negative;
        public event ClearEventThrower ClearButton;
        MethodInfo[] Methods;
        Calcinfo CI;
        public Kalkylator(Calcinfo CCI)
        {
            CI = CCI;
            var DLL = Assembly.LoadFrom("WinCalc.dll");
            var TH = DLL.GetType("WinCalc.Räknare");
            Methods = TH.GetMethods();
        }/// <summary>
        /// 1.Addition 2.Substraction 3.Multiplikation 4.Division
        /// </summary>
        /// <param name="switchis"></param>
        /// <returns></returns>
        public void Calculations(int switchis,double inputdata)
        {
            double returner = 0;
            switch (switchis)
            { 
                case 2:
                    {
                    returner= double.Parse(Methods[(switchis )].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
            case 3:
                {
                    returner = double.Parse(Methods[(switchis)].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
            case 4:
                {
                    returner = double.Parse(Methods[(switchis )].Invoke(null, new object[] { CI.currentSum, inputdata }).ToString());
                        break;
                    }
            case 5:
                {
                        if (inputdata == 0)
                        {
                            returner = 1337;
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
            if (returner < 0)
            {
                Negative(returner);
            }
            CI.currentSum = returner;
        }
        public double PI()
        {
            return double.Parse(Methods[0].Invoke(null, new object[] { }).ToString());
        }
        public double Euler()
        {
            return double.Parse(Methods[1].Invoke(null, new object[] { }).ToString());
        }
        public double SquareRot(double dd)
        {
            return double.Parse(Methods[6].Invoke(null, new object[] {dd }).ToString());
        }
        public double Power2(double dd)
        {
            return double.Parse(Methods[7].Invoke(null, new object[] { dd }).ToString());
        }
        public double Invert(double dd)
        {
            return dd * (-1);
        }
        public void ClearKey()
        {
            CI.Creset();
            ClearButton();
        }
    }
}
