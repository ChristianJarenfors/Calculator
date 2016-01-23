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
        //Kalkylatorns referens
        Kalkylator Calkisen;
        // Fångar logg eventsen som kalkylatorn skickar ut
        Logger Loggarn;
        //Calcinfo innehåller olika variabler double,bool, string och int som kan sparas ner och laddas
        //Dessutom så använder både Formen och Kalkylatorn samma objekt
        Calcinfo CI = new Calcinfo(0,true,true,false,false,true,0,0,null,null);
        //En lista för de olika räknesätten för att skriva i den textbox där man ser vad man har gjort
        List<string> CalcMethod;
        //Strömreferens
        Stream strömmen;
        //Binaryreferens
        BinaryFormatter binForm;
        //Formateringsinfo för mina double.Parse operationer
        NumberFormatInfo fmt ;

        public Form1()
        {
            InitializeComponent();

            //Instanisering och sättning av min formateringsinfo
            fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";
            fmt.NumberDecimalSeparator = ".";

            //Instancing my binaryFormatter
            binForm = new BinaryFormatter();

            //Instansiering och sättning av min räknesättslista
            CalcMethod = new List<string>();
            CalcMethod.Add("+");
            CalcMethod.Add("-");
            CalcMethod.Add("*");
            CalcMethod.Add("/");

            //Loading previous history and calculations
            LoadHistory();
            LoadCurrentCalc();
            
            //Instancing the calkylator with the CalcInfo Loaded above
            Calkisen = new Kalkylator(CI);
            //Instancing the EventCatcher with the calkylator
            Loggarn = new Logger(Calkisen);

        }

        //All Numberbutton code is the same and is put in a method with a parameter.
        //For more specific detail I send you to look at that method where i will comment more
        //The method for the numberbuttons lies in the begining

        //Besides that the decimal method is uniqe and is at begining of the region

        /*
        Before I go into detail of all my methods I want to give a  little info on my diffrent boolflags
        They are these 5:
        decimalBoolNotUsed: This one indicates whether there is a "." in Indata string already. 
        inputIsNotDone    : This one indicates whether there have been any input done before.
        isCalcSelected    : This one indicates whether any calculationoperation has been choosen.
        Doubleloader      : This one indicates that someone has double clicked in the history and loaded a calculation.
        firstInput        : This one is an indicator to mark if the indata is the first data  
        */
        #region Numberbuttons + Decimal

        private void insertNumber(string s)
        {
            //This method enters numbers into the output textbox, which in a way also is where the indata comes from.
            if (!CI.Doubleloader)
            {
                //If noone has loaded from history
                if (CI.inputIsNotDone)
                {
                    //and if this is the first input.

                    //then the output textbox is set to "s"
                    textBoxOutput.Text = s;
                    //and hence the flag is set to false so the next number won't set but rather add the number to the outputtextbox
                    CI.inputIsNotDone = false;
                }
                //As said above this is where we come come if there have been input already´.
                else { textBoxOutput.Text += s; }
            }
            else
            {
                // this only happens if someone has loaded from history.
                //it is worthwhile noting that the doubleloader flag is set to false if a calculation
                //method is choosen so this case is if you load and decied to press a numberbutton imediatly

                //Clears the calculation
                buttonC_Click(null, null);
                //set the first number to "s" and sets the flag to false as now the first input has been made
                textBoxOutput.Text = s;
                CI.inputIsNotDone = false;
            }
        }
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            //Enters the decimal dot
            if (!CI.Doubleloader)
            {
                //If noone has loaded form history
                if (CI.inputIsNotDone)
                {
                    //and no input has been done

                    // the text is set to "0."
                    textBoxOutput.Text = "0.";
                    //Boolflag=false since  now we have input and we have a comma
                    CI.inputIsNotDone = false;
                    CI.decimalBoolNotUsed = false;
                }
                else if (CI.decimalBoolNotUsed)
                {
                    //if we already have input(CI.inputIsNotDone=false) and if we have no comma already
                    textBoxOutput.Text += ".";
                    CI.decimalBoolNotUsed = false;
                }

            }
            else
            {
                buttonC_Click(null, null);
                textBoxOutput.Text = "0.";
                CI.inputIsNotDone = false;
            }
        }
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
                    Calkisen.Calculations((operation),double.Parse(textBoxOutput.Text,fmt));
                    //CI.currentSum = (double)Methods[operation].Invoke(null, new object[] { CI.currentSum, double.Parse(textBoxOutput.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo) });
                    textBoxOutput.Text = CI.currentSum.ToString();
                }
                else
                {
                    CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
                }   
            }
            else
            {
                CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
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
            listBoxHistory.Items.Add(new HistoryRecord(textBoxCalculations.Text, double.Parse(textBoxOutput.Text,fmt)));
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
            if (!(double.Parse(textBoxOutput.Text,fmt)<0))
            {
                textBoxOutput.Text = Calkisen.SquareRot(double.Parse(textBoxOutput.Text,fmt)).ToString();

            }
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            removeDecimal();
            textBoxOutput.Text = Calkisen.Power2(double.Parse(textBoxOutput.Text,fmt)).ToString();
            
        }

        private void buttonBackDelete_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 1);
        }

        private void buttonPosNeg_Click(object sender, EventArgs e)
        {
            textBoxOutput.Text = Calkisen.Invert(double.Parse(textBoxOutput.Text, fmt)).ToString();
            CI.inputIsNotDone = false;
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
