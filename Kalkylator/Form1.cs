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
    public partial class Form1 : Form
    {
        MethodInfo[] Methods;
        Calcinfo CI= new Calcinfo()
        double currentSum = 0;
        bool inputIsNotDone = true, isCalcSelected = false,Doubleloader = false,firstInput=true;
        
        int selectedcalcmethod,oldCalcMethod;
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
        }
        bool decimalBoolNotUsed = true;


        #region Numberbuttons + Decimal

        private void buttonNumber1_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "1";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "1"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "1";
                inputIsNotDone = false;
            }
        }

        private void buttonNumber2_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "2";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "2"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "2";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber3_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "3";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "3"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "3";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber4_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "4";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "4"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "4";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber5_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "5";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "5"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "5";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber6_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "6";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "6"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "6";
                inputIsNotDone = false;
            }
        }

        private void buttonNumber7_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "7";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "7"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "7";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber8_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "8";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "8"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "8";
                inputIsNotDone = false;
            }
        }
        private void buttonNumber9_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "9";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "9"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "9";
                inputIsNotDone = false;
            }
        }

        private void buttonNumber0_Click(object sender, EventArgs e)
        {
            if (!Doubleloader)
            {
                if (inputIsNotDone)
                {
                    textBoxOutput.Text = "0";
                    inputIsNotDone = false;
                }
                else { textBoxOutput.Text += "0"; }
            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "0";
                inputIsNotDone = false;
            }
        }
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if (decimalBoolNotUsed)
            {
                textBoxOutput.Text += ".";
                decimalBoolNotUsed = false;
            }
        }
        #endregion

        #region Operations

        private void buttonPlus_Click(object sender, EventArgs e)
        {

            selectedcalcmethod = 2;
            CalcTextboxUpdate(selectedcalcmethod);
            if (firstInput)
            {
                currentSum = double.Parse(textBoxOutput.Text);
                firstInput = false;
            }
            else
            {
                calculate(oldCalcMethod);
            }
            oldCalcMethod = selectedcalcmethod;
            inputIsNotDone = true;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

            selectedcalcmethod = 3;
            CalcTextboxUpdate(selectedcalcmethod);
            if (firstInput)
            {
                currentSum = double.Parse(textBoxOutput.Text);
                firstInput = false;
            }
            else
            {
                calculate(oldCalcMethod);
            }
            oldCalcMethod = selectedcalcmethod;
            inputIsNotDone = true;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {

            selectedcalcmethod = 4;
            CalcTextboxUpdate(selectedcalcmethod);
            if (firstInput)
            {
                currentSum = double.Parse(textBoxOutput.Text);
                firstInput = false;
            }
            else
            {
                calculate(oldCalcMethod);
            }
            oldCalcMethod = selectedcalcmethod;
            inputIsNotDone = true;
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            selectedcalcmethod = 5;
            CalcTextboxUpdate(selectedcalcmethod);
            if (firstInput)
            {
                currentSum = double.Parse(textBoxOutput.Text);
                firstInput = false;
            }
            else
            {
                calculate(oldCalcMethod);
            }
            oldCalcMethod = selectedcalcmethod;
            inputIsNotDone = true;
        }
        #endregion

        public void calculate(int operation)
        {
            if (!inputIsNotDone)
            {
                if (isCalcSelected)
                {
                    
                    currentSum = (double)Methods[operation].Invoke(null, new object[] { currentSum, double.Parse(textBoxOutput.Text) });
                    textBoxOutput.Text = currentSum.ToString();
                }
                else
                {
                    currentSum = double.Parse(textBoxOutput.Text);
                }
                
                
                
            }
            else
            {
                currentSum = double.Parse(textBoxOutput.Text);
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
            CalcTextboxUpdate(selectedcalcmethod);
            calculate(selectedcalcmethod);
            textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
            listBoxHistory.Items.Add(new HistoryRecord(textBoxCalculations.Text, double.Parse(textBoxOutput.Text)));
            buttonC_Click(null, null);
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            textBoxCalculations.Text = null;
            currentSum = 0;
            isCalcSelected = false;
            inputIsNotDone = true;
            firstInput = true;
            Doubleloader = false;
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            inputIsNotDone = true;
        }

        private void buttonPi_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[0].Invoke(null, new object[] { }).ToString();
            inputIsNotDone = false;

        }

        private void buttonEuler_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Methods[1].Invoke(null, new object[] { }).ToString();
            inputIsNotDone = false;
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
        }

        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonC_Click(null,null);
            textBoxCalculations.Text = ((HistoryRecord)listBoxHistory.SelectedItem).s;
            textBoxOutput.Text = ((HistoryRecord)listBoxHistory.SelectedItem).sum.ToString();
            currentSum = ((HistoryRecord)listBoxHistory.SelectedItem).sum;
            Doubleloader = true;
            //inputIsNotDone = true;
            //Doubleloader = true;
            //isCalcSelected = false;
        }
        public void CalcTextboxUpdate(int i)
        {
            //if (!isCalcSelected)
            //{
            selectedcalcmethod = i;
            if (Doubleloader)
            {
                textBoxCalculations.Text += CalcMethod[(selectedcalcmethod - 2)];
                Doubleloader = false;
                isCalcSelected = true;
            }
            else
            {
                if (!inputIsNotDone)
                {
                    textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(selectedcalcmethod - 2)];
                    isCalcSelected = true;
                }
                else
                {
                    textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
                    textBoxCalculations.Text += CalcMethod[(selectedcalcmethod - 2)];
                    isCalcSelected = true;
                }
            }
        }
        public void SaveCurrentCalc()
        {
            strömmen = File.Open("Restore1.bin", FileMode.Create, FileAccess.Write);

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

    }
}
