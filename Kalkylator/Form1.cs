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

namespace wincalcmini
{
    public partial class Form1 : Form
    {
        MethodInfo[] Methods;
        double currentSum = 0;
        bool inputIsNotDone = true, isCalcSelected = false;
        List<string> CalcMethod;
        int selectedcalcmethod;
        public Form1()
        {
            InitializeComponent();
            var DLL = Assembly.LoadFrom("WinCalc.dll");
            var TH = DLL.GetType("WinCalc.Räknare");
            //var c = Activator.CreateInstance(TH);
            Methods = TH.GetMethods();
            CalcMethod = new List<string>();
            CalcMethod.Add("+");
            CalcMethod.Add("-");
            CalcMethod.Add("*");
            CalcMethod.Add("/");
        }
        bool decimalBoolNotUsed = true;
        
        
        

        private void buttonNumber1_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "1";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "1"; }
        }

        private void buttonNumber2_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "2";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "2"; }
        }

        private void buttonNumber3_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "3";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "3"; }
        }

        private void buttonNumber4_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "4";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "4"; }
        }

        private void buttonNumber5_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "5";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "5"; }
        }
        private void buttonNumber6_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "6";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "6"; }
        }

        private void buttonNumber7_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "7";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "7"; }
        }
        private void buttonNumber8_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "8";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "8"; }
        }

        private void buttonNumber9_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "9";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "9"; }
        }

        private void buttonNumber0_Click(object sender, EventArgs e)
        {
            if (inputIsNotDone)
            {
                textBoxOutput.Text = "0";
                inputIsNotDone = false;
            }
            else { textBoxOutput.Text += "0"; }
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if (decimalBoolNotUsed)
            {
                textBoxOutput.Text += ".";
                decimalBoolNotUsed = false;
            }
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (!isCalcSelected)
            {
                selectedcalcmethod = 2;
            }
            calculate(selectedcalcmethod);
            selectedcalcmethod = 2;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (!isCalcSelected)
            {
                selectedcalcmethod = 3;
            }
            calculate(selectedcalcmethod);
            selectedcalcmethod = 3;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (!isCalcSelected)
            {
                selectedcalcmethod = 4;
            }
            calculate(selectedcalcmethod);
            selectedcalcmethod = 4;
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            if (!isCalcSelected)
            {
                selectedcalcmethod = 5;
            }
            calculate(selectedcalcmethod);
            selectedcalcmethod = 5;
        }
        public void calculate(int operation)
        {
            if (!inputIsNotDone)
            {
                textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(operation - 2)];
                if (isCalcSelected)
                {
                    currentSum = (double)Methods[operation].Invoke(null, new object[] { currentSum, double.Parse(textBoxOutput.Text) });
                    textBoxOutput.Text = currentSum.ToString();
                }
                else
                {
                    currentSum = double.Parse(textBoxOutput.Text);
                }
                isCalcSelected = true;
                
                inputIsNotDone = true;
            }
            else
            {
                textBoxCalculations.Text= textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length-1);
                textBoxCalculations.Text += CalcMethod[(operation - 2)];
            }
        }
        public void calculationStringUpdate(bool calcMethod,int operation)
        {
            //if()
            textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(operation - 2)];
        }

        private void buttonSum_Click(object sender, EventArgs e)
        {
            calculate(selectedcalcmethod);
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
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = "0";
            inputIsNotDone = true;
        }

        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxCalculations.Text = ((HistoryRecord)listBoxHistory.SelectedItem).s;
            textBoxOutput.Text = ((HistoryRecord)listBoxHistory.SelectedItem).sum.ToString();
            currentSum = ((HistoryRecord)listBoxHistory.SelectedItem).sum;
        }
    }
}
