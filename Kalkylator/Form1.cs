using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

namespace wincalcmini
{
    public partial class Form1 : Form
    {
        Kalkylator Calkisen;
        MethodInfo[] Methods;
        Logger Loggarn;
        Calcinfo CI = new Calcinfo(0,true,true,false,false,true,0,0,null,null);
        List<string> CalcMethod;
        Stream strömmen;
        BinaryFormatter binForm;
        
        NumberFormatInfo fmt ;

        public Form1()
        {
            InitializeComponent();
            fmt = new NumberFormatInfo();

            fmt.NegativeSign = "-";
            fmt.NumberDecimalSeparator = ".";
            binForm = new BinaryFormatter();
            
            var DLL = Assembly.LoadFrom("WinCalc.dll");
            var TH = DLL.GetType("WinCalc.Räknare");
            //var c = Activator.CreateInstance(TH);
            Methods = TH.GetMethods();
            CalcMethod = new List<string>();
            CalcMethod.Add("+");
            CalcMethod.Add("-");
            CalcMethod.Add("*");
            CalcMethod.Add("/");
            textBoxCalculations.Text = null;
            LoadHistory();
            LoadCurrentCalc();
            Calkisen = new Kalkylator(CI);
            Loggarn = new Logger(Calkisen);

        }
        //bool CIdecimalBoolNotUsed = true;


        #region Numberbuttons + Decimal

        private void buttonNumber1_Click(object sender, EventArgs e)
        {
            insertNumber("1");
            
        }

        private void buttonNumber2_Click(object sender, EventArgs e)
        {
            insertNumber("2");
        }
        private void buttonNumber3_Click(object sender, EventArgs e)
        {
            insertNumber("3");
            
        }
        private void buttonNumber4_Click(object sender, EventArgs e)
        {
            insertNumber("4");
            
        }
        private void buttonNumber5_Click(object sender, EventArgs e)
        {
            insertNumber("5");
            
        }
        private void buttonNumber6_Click(object sender, EventArgs e)
        {
            insertNumber("6");
            
        }

        private void buttonNumber7_Click(object sender, EventArgs e)
        {
            insertNumber("7");
            
        }
        private void buttonNumber8_Click(object sender, EventArgs e)
        {
            insertNumber("8");
            
        }
        private void buttonNumber9_Click(object sender, EventArgs e)
        {
            insertNumber("9");
            
        }

        private void buttonNumber0_Click(object sender, EventArgs e)
        {
            insertNumber("0");
            
        }
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if (!CI.Doubleloader)
            {
                if (CI.inputIsNotDone)
                {
                    textBoxOutput.Text = "0.";
                    CI.inputIsNotDone = false;
                }
                else if (CI.decimalBoolNotUsed)
                {
                    textBoxOutput.Text += ".";
                    CI.decimalBoolNotUsed = false;
                }

            }
            else {
                buttonC_Click(null, null);
                textBoxOutput.Text = "0.";
                CI.inputIsNotDone = false;
            }
        }
        private void insertNumber(string s)
        {
            if (!CI.Doubleloader)
            {
                if (CI.inputIsNotDone)
                {
                    textBoxOutput.Text = s;
                    CI.inputIsNotDone = false;
                }
                else { textBoxOutput.Text += s; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = s;
                CI.inputIsNotDone = false;
            }
        }
        #endregion

        #region Operations

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            removeDecimal();
            UpdateMathOutput(2);
        }
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            removeDecimal();
            UpdateMathOutput(3);

        }
        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            removeDecimal();
            UpdateMathOutput(4);
        }
        private void buttonDivision_Click(object sender, EventArgs e)
        {
            removeDecimal();
            UpdateMathOutput(5);
        }
        private void UpdateMathOutput(int v)
        {
            CI.selectedcalcmethod = v;
            CalcTextboxUpdate(CI.selectedcalcmethod);
            removeDecimal();
            if (CI.firstInput)
            {
                if (textBoxOutput.Text != null && textBoxOutput.Text!="")
                {
                    CI.currentSum = double.Parse(textBoxOutput.Text,fmt);
                    CI.firstInput = false;
                }
                else
                {
                    CI.currentSum = 0;
                    CI.firstInput = false;
                }
            }
            else
            {
                calculate(CI.oldCalcMethod);
            }
            CI.oldCalcMethod = CI.selectedcalcmethod;
            CI.inputIsNotDone = true;
            CI.decimalBoolNotUsed = true;
        }
        #endregion

        public void calculate(int operation)
        {
            removeDecimal();
            if (!CI.inputIsNotDone)
            {
                if (CI.isCalcSelected)
                {
                    Calkisen.Calculations((operation),double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo));
                    //CI.currentSum = (double)Methods[operation].Invoke(null, new object[] { CI.currentSum, double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo) });
                    textBoxOutput.Text = CI.currentSum.ToString();
                }
                else
                {
                    CI.currentSum = double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                }   
            }
            else
            {
                CI.currentSum = double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            }
        }
        public void CalcTextboxUpdate(int i)
        {
            CI.selectedcalcmethod = i;
            if (textBoxOutput.Text == null || textBoxOutput.Text == "")
            {
                textBoxOutput.Text = "0";
            }
            if (textBoxCalculations.Text == null || textBoxCalculations.Text == "")
            {
                textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(CI.selectedcalcmethod - 2)];
                CI.isCalcSelected = true;
            }else if (CI.Doubleloader)
            {
                textBoxCalculations.Text += CalcMethod[(CI.selectedcalcmethod - 2)];
                CI.Doubleloader = false;
                CI.isCalcSelected = true;
            }
            else
            {
                if (!CI.inputIsNotDone)
                {
                    textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(CI.selectedcalcmethod - 2)];
                    CI.isCalcSelected = true;
                }
                else
                {
                    textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
                    textBoxCalculations.Text += CalcMethod[(CI.selectedcalcmethod - 2)];
                    CI.isCalcSelected = true;
                }
            }
        }
        public void calculationStringUpdate(bool calcMethod,int operation)
        { 
            textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(operation - 2)];
        }

        private void buttonSum_Click(object sender, EventArgs e)
        {
            removeDecimal();
            CalcTextboxUpdate(CI.selectedcalcmethod);
            calculate(CI.selectedcalcmethod);
            textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
            listBoxHistory.Items.Add(new HistoryRecord(textBoxCalculations.Text, double.Parse(textBoxOutput.Text)));
            textBoxOutput.Text = "0";
            textBoxCalculations.Text = null;
            CI.Creset();
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            textBoxCalculations.Text = null;
            Calkisen.ClearKey();
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            CI.inputIsNotDone = true;
        }

        private void buttonPi_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Calkisen.PI().ToString();
            CI.inputIsNotDone = false;

        }

        private void buttonEuler_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Calkisen.Euler().ToString();
            CI.inputIsNotDone = false;
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            removeDecimal();
            if (!(double.Parse(textBoxOutput.Text)<0))
            {
                textBoxOutput.Text = Calkisen.SquareRot(double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo)).ToString();

            }
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            removeDecimal();
            textBoxOutput.Text = Calkisen.Power2(double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo)).ToString();
            
        }

        private void buttonBackDelete_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 1);
        }

        private void buttonPosNeg_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Calkisen.Invert(double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo)).ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveHistory();
            SaveCurrentCalc();
        }

        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxHistory.SelectedItem!=null)
            {
                buttonC_Click(null, null);
                textBoxCalculations.Text = ((HistoryRecord)listBoxHistory.SelectedItem).s;
                textBoxOutput.Text = null;
                foreach (char c in ((HistoryRecord)listBoxHistory.SelectedItem).sum.ToString())
                {
                    if (c == ',')
                    {
                        textBoxOutput.Text += '.';
                    }
                    else
                    {
                        textBoxOutput.Text += c.ToString();
                    }
                } 
                CI.currentSum = ((HistoryRecord)listBoxHistory.SelectedItem).sum;
                CI.Doubleloader = true;
            }
        }
        

        private void buttonDeleteHistory_Click(object sender, EventArgs e)
        {
            listBoxHistory.Items.Clear();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            strömmen = File.Open("calculations.csv", FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(strömmen);
                        
            foreach (HistoryRecord item in listBoxHistory.Items)
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in item.ToString())
                {
                    if (c == '+' || c == '-' || c == '*' || c == '/' || c == '=')
                    {
                        sb.Append(',');
                        sb.Append(c);
                        sb.Append(',');
                        //sträng += ",";
                    }
                    else
                    {
                        sb.Append(c);
                        //sträng += c;
                    }
                }
                sr.Write(sb.ToString());
                sr.Write("\r\n");
                //sträng = null;
            }
            sr.Flush();
            sr.Close();
            strömmen.Close();
        }

        private void buttonLoggFile_Click(object sender, EventArgs e)
        {
            //LoggPopper.Invoke();
            //MessageBox.Show(Logg);
        }

        public void SaveCurrentCalc()
        {
            strömmen = File.Open("Restore1.bin", FileMode.Create, FileAccess.Write);
            CI.UpdateOutputInfo(textBoxCalculations.Text,textBoxOutput.Text);
            binForm.Serialize(strömmen, CI);
            strömmen.Close();

        }
        public void LoadCurrentCalc()
        {
            if (File.Exists("Restore1.bin"))
            {
                strömmen = File.Open("Restore1.bin", FileMode.Open, FileAccess.Read);
                if (!(strömmen.Length==0))
                {
                    CI = (Calcinfo)binForm.Deserialize(strömmen);
                }
                textBoxCalculations.Text = CI.Calculations;
                textBoxOutput.Text = CI.Output;
                strömmen.Close(); 
            }
        }
        public void SaveHistory()
        {
            strömmen = File.Open("Restore2.bin", FileMode.Create, FileAccess.Write);
            List<HistoryRecord> list = new List<HistoryRecord>();
            foreach (HistoryRecord item in listBoxHistory.Items)
            {
                list.Add(item);
            }
            binForm.Serialize(strömmen, list);
            strömmen.Close();
        }
        public void LoadHistory()
        {
            if (File.Exists("Restore2.bin"))
            {
                strömmen = File.Open("Restore2.bin", FileMode.Open, FileAccess.Read);
                List<HistoryRecord> list = new List<HistoryRecord>();
                if (!(strömmen.Length==0))
                {
                    list = (List<HistoryRecord>)binForm.Deserialize(strömmen);
                    foreach (HistoryRecord item in list)
                    {
                        listBoxHistory.Items.Add(item);
                    }
                }
                
                strömmen.Close();
            }
        }
        public void removeDecimal()
        {
            string s = textBoxOutput.Text;
            textBoxOutput.Text = "";
            foreach (char c in s)
            {
                if(c==',')
                {
                    textBoxOutput.Text += '.';
                }
                else
                {
                    textBoxOutput.Text += c;
                }
            }
        }
        

    }
}
