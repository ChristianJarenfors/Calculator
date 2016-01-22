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
using System.Runtime.Serialization.Formatters.Binary;

namespace wincalcmini
{
    delegate void Loggcaller();
    public partial class Form1 : Form
    {
        event Loggcaller BreakEvent;
        event Loggcaller NegativeEvent;
        MethodInfo[] Methods;
        
        //double CIcurrentSum = 0;
        //bool CIinputIsNotDone = true, CIisCalcSelected = false,CIDoubleloader = false,CIfirstInput=true;     
        //int CIselectedcalcmethod,CIoldCalcMethod;
        Calcinfo CI = new Calcinfo(0,true,true,false,false,true,0,0,null,null);
        List<string> CalcMethod;
        Stream strömmen;
        BinaryFormatter binForm;
        public Form1()
        {
            InitializeComponent();

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
        }
        //bool CIdecimalBoolNotUsed = true;


        #region Numberbuttons + Decimal

        private void buttonNumber1_Click(object sender, EventArgs e)
        {
            insertNumber("1");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "1";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "1"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "1";
            //    CI.inputIsNotDone = false;
            //}
        }

        private void buttonNumber2_Click(object sender, EventArgs e)
        {
            insertNumber("2");
        }
        private void buttonNumber3_Click(object sender, EventArgs e)
        {
            insertNumber("3");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "3";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "3"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "3";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonNumber4_Click(object sender, EventArgs e)
        {
            insertNumber("4");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "4";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "4"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "4";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonNumber5_Click(object sender, EventArgs e)
        {
            insertNumber("5");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "5";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "5"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "5";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonNumber6_Click(object sender, EventArgs e)
        {
            insertNumber("6");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "6";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "6"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "6";
            //    CI.inputIsNotDone = false;
            //}
        }

        private void buttonNumber7_Click(object sender, EventArgs e)
        {
            insertNumber("7");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "7";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "7"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "7";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonNumber8_Click(object sender, EventArgs e)
        {
            insertNumber("8");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "8";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "8"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "8";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonNumber9_Click(object sender, EventArgs e)
        {
            insertNumber("9");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "9";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "9"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "9";
            //    CI.inputIsNotDone = false;
            //}
        }

        private void buttonNumber0_Click(object sender, EventArgs e)
        {
            insertNumber("0");
            //if (!CI.Doubleloader)
            //{
            //    if (CI.inputIsNotDone)
            //    {
            //        textBoxOutput.Text = "0";
            //        CI.inputIsNotDone = false;
            //    }
            //    else { textBoxOutput.Text += "0"; }
            //}
            //else
            //{
            //    buttonC_Click(null, null);
            //    textBoxOutput.Text = "0";
            //    CI.inputIsNotDone = false;
            //}
        }
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if (CI.decimalBoolNotUsed)
            {
                textBoxOutput.Text += ".";
                CI.decimalBoolNotUsed = false;
            }
        }
        #endregion

        #region Operations

        private void buttonPlus_Click(object sender, EventArgs e)
        {

            CI.selectedcalcmethod = 2;
            CalcTextboxUpdate(CI.selectedcalcmethod);
            if (CI.firstInput)
            {
                CI.currentSum = double.Parse(textBoxOutput.Text);
                CI.firstInput = false;
            }
            else
            {
                calculate(CI.oldCalcMethod);
            }
            CI.oldCalcMethod = CI.selectedcalcmethod;
            CI.inputIsNotDone = true;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

            CI.selectedcalcmethod = 3;
            CalcTextboxUpdate(CI.selectedcalcmethod);
            if (CI.firstInput)
            {
                CI.currentSum = double.Parse(textBoxOutput.Text);
                CI.firstInput = false;
            }
            else
            {
                calculate(CI.oldCalcMethod);
            }
            CI.oldCalcMethod = CI.selectedcalcmethod;
            CI.inputIsNotDone = true;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {

            CI.selectedcalcmethod = 4;
            CalcTextboxUpdate(CI.selectedcalcmethod);
            if (CI.firstInput)
            {
                CI.currentSum = double.Parse(textBoxOutput.Text);
                CI.firstInput = false;
            }
            else
            {
                calculate(CI.oldCalcMethod);
            }
            CI.oldCalcMethod = CI.selectedcalcmethod;
            CI.inputIsNotDone = true;
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            CI.selectedcalcmethod = 5;
            CalcTextboxUpdate(CI.selectedcalcmethod);
            if (CI.firstInput)
            {
                CI.currentSum = double.Parse(textBoxOutput.Text);
                CI.firstInput = false;
            }
            else
            {
                calculate(CI.oldCalcMethod);
            }
            CI.oldCalcMethod = CI.selectedcalcmethod;
            CI.inputIsNotDone = true;
        }
        #endregion

        public void calculate(int operation)
        {
            if (!CI.inputIsNotDone)
            {
                if (CI.isCalcSelected)
                {
                    
                    CI.currentSum = (double)Methods[operation].Invoke(null, new object[] { CI.currentSum, double.Parse(textBoxOutput.Text) });
                    textBoxOutput.Text = CI.currentSum.ToString();
                }
                else
                {
                    CI.currentSum = double.Parse(textBoxOutput.Text);
                }   
            }
            else
            {
                CI.currentSum = double.Parse(textBoxOutput.Text);
                //textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
                //textBoxCalculations.Text += CalcMethod[(operation - 2)];
            }
        }
        public void calculationStringUpdate(bool calcMethod,int operation)
        {
            //if()
            textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(operation - 2)];
        }

        private void buttonSum_Click(object sender, EventArgs e)
        {
            CalcTextboxUpdate(CI.selectedcalcmethod);
            calculate(CI.selectedcalcmethod);
            textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
            listBoxHistory.Items.Add(new HistoryRecord(textBoxCalculations.Text, double.Parse(textBoxOutput.Text)));
            if (double.Parse(textBoxOutput.Text)<0)
            {
                if (NegativeEvent!=null)
                {
                    NegativeEvent.Invoke();
                }   
            }
            buttonC_Click(null, null);
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            textBoxCalculations.Text = null;
            CI.currentSum = 0;
            CI.decimalBoolNotUsed = true;
            CI.isCalcSelected = false;
            CI.inputIsNotDone = true;
            CI.firstInput = true;
            CI.Doubleloader = false;
            if (BreakEvent != null)
            {
                BreakEvent.Invoke();
            }
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            CI.inputIsNotDone = true;
        }

        private void buttonPi_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[0].Invoke(null, new object[] { }).ToString();
            CI.inputIsNotDone = false;

        }

        private void buttonEuler_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[1].Invoke(null, new object[] { }).ToString();
            CI.inputIsNotDone = false;
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[6].Invoke(null, new object[] { double.Parse(textBoxOutput.Text) }).ToString();
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[7].Invoke(null, new object[] { double.Parse(textBoxOutput.Text) }).ToString();
        }

        private void buttonBackDelete_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 1);
        }

        private void buttonPosNeg_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[8].Invoke(null, new object[] { double.Parse(textBoxOutput.Text) }).ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveHistory();
            SaveCurrentCalc();
        }

        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonC_Click(null,null);
            textBoxCalculations.Text = ((HistoryRecord)listBoxHistory.SelectedItem).s;
            textBoxOutput.Text = ((HistoryRecord)listBoxHistory.SelectedItem).sum.ToString();
            CI.currentSum = ((HistoryRecord)listBoxHistory.SelectedItem).sum;
            CI.Doubleloader = true;
            //inputIsNotDone = true;
            //Doubleloader = true;
            //isCalcSelected = false;
        }
        public void CalcTextboxUpdate(int i)
        {
            //if (!isCalcSelected)
            //{
            CI.selectedcalcmethod = i;
            if (CI.Doubleloader)
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
            //Calcinfo CI = new Calcinfo(CIcurrentSum, CIinputIsNotDone, CIisCalcSelected, CIDoubleloader, CIfirstInput, CIselectedcalcmethod, CIoldCalcMethod,textBoxCalculations.Text,textBoxOutput.Text);
            binForm.Serialize(strömmen, CI);
            strömmen.Close();

        }
        public void LoadCurrentCalc()
        {
            if (File.Exists("Restore1.bin"))
            {
                //Calcinfo CI= new Calcinfo();
                strömmen = File.Open("Restore1.bin", FileMode.Open, FileAccess.Read);
                if (!(strömmen.Length==0))
                {
                    CI = (Calcinfo)binForm.Deserialize(strömmen);
                }
                //CIcurrentSum = CI.currentSum;

                //CIinputIsNotDone= CI.inputIsNotDone;
                //CIisCalcSelected= CI.isCalcSelected;
                //CIDoubleloader= CI.Doubleloader;
                //CIfirstInput= CI.firstInput;
                //CIselectedcalcmethod= CI.selectedcalcmethod;
                //CIoldCalcMethod = CI.oldCalcMethod;
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

    }
}
