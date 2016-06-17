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
    /*
    ****************************************************************************************************
    This Program has been commented in both english and swedish due to my mind wandering away while
    Commentating. Though it's mostly english and I would have prefered to Have it all English. to
    Save me some trauma I will leave it like this for the time being. 
    ****************************************************************************************************
        */ 
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
            fmt.NumberDecimalSeparator = ",";
            
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
                    textBoxOutput.Text = "0,";
                    //Boolflag=false since  now we have input and we have a comma
                    CI.inputIsNotDone = false;
                    CI.decimalBoolNotUsed = false;
                }
                else if (CI.decimalBoolNotUsed)
                {
                    //if we already have input(CI.inputIsNotDone=false) and if we have no comma already

                    //Add a dot
                    textBoxOutput.Text += ",";
                    //and set the flag
                    CI.decimalBoolNotUsed = false;
                }

            }
            else
            {
                //If someone has loaded from history
                //Clear calculation 
                buttonC_Click(null, null);
                //Set output to "0." 
                textBoxOutput.Text = "0,";
                //Input is done, hence = false
                CI.inputIsNotDone = false;
            }
        }

        private void buttonNumber1_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("1");  
        }

        private void buttonNumber2_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("2");
        }
        private void buttonNumber3_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("3");
            
        }
        private void buttonNumber4_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("4");
            
        }
        private void buttonNumber5_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("5");
            
        }
        private void buttonNumber6_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("6");
            
        }

        private void buttonNumber7_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("7");
            
        }
        private void buttonNumber8_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("8");
            
        }
        private void buttonNumber9_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("9");
            
        }

        private void buttonNumber0_Click(object sender, EventArgs e)
        {
            //see above   /\
            //            ||
            insertNumber("0");
            
        }


        #endregion

        //Arithmetic Buttoncode and Methods used by them
        #region Arithmetic Operations
        /*
        This region contains the code for the Arithmetic buttons
        It consists of a Method of nesteled methods
        For more info look at the Arithmetic Method Region
        */
        #region Arithmetics buttons
        private void buttonPlus_Click(object sender, EventArgs e)
        {
            
            UpdateMathOutput(2);
        }
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            UpdateMathOutput(3);

        }
        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            UpdateMathOutput(4);
        }
        private void buttonDivision_Click(object sender, EventArgs e)
        {
            UpdateMathOutput(5);
        }
        #endregion


        //Contains the Arithmetic Methods
        #region Arithmetics Methods
        private void UpdateMathOutput(int v)
        {
            //A format method which might be a bit excessive 
            removeDecimal();
            
            //Sets the selectedcalcmethod to the choosen number. The number is based on the
            //the order in which the method comes in the reflected DLL in the calcylator class. 
            CI.selectedcalcmethod = v;

            //This is a method that come below. It Updates the Cálculationstextbox
            CalcTextboxUpdate(CI.selectedcalcmethod);
            
            //This is an if-clause to check if its the first input or not 
            if (CI.firstInput)
            {
                //If it is. Then...  See if the  the textbox is empty 
                if (textBoxOutput.Text != null && textBoxOutput.Text != "")
                {
                    //If not: store the value
                    //and change flag
                    CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
                    CI.firstInput = false;
                }
                else
                {
                    //else since the textbox is empty set the currentsum to 0 
                    CI.currentSum = 0;
                    CI.firstInput = false;
                }
            }
            else
            {
                //A calculationmethod that operates with the previous calcmethod
                calculate(CI.oldCalcMethod);
            }
            //Change the oldCalcMethod in order to set the  next Arithmetics method for the next
            //Calculation
            CI.oldCalcMethod = CI.selectedcalcmethod;
            //The 2 flags are set to enable new input
            CI.inputIsNotDone = true;
            CI.decimalBoolNotUsed = true;
        }

        //This Method mainly Updates the textBoxCalculations textbox in case of 
        //an Arithmetic or sum operation
        public void CalcTextboxUpdate(int i)
        {
            //this might be excessive
            CI.selectedcalcmethod = i;

            //If the OutPut textBox is empty... ..it is set to currentsum.
            if (textBoxOutput.Text == null || textBoxOutput.Text == "")
            {
                textBoxOutput.Text = CI.currentSum.ToString();
            }

            //If the Calculation textBox is empty... ..it is set to output textbox + the current Arithmetic sign.
            if (textBoxCalculations.Text == null || textBoxCalculations.Text == "")
            {
                textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(CI.selectedcalcmethod - 2)];
                CI.isCalcSelected = true;
            }
            //if there is something in textBoxCalculations
            //and if Doubleloader is true
            //add an arithmetic sign to the calculation
            else if (CI.Doubleloader)
            {
                textBoxCalculations.Text += CalcMethod[(CI.selectedcalcmethod - 2)];
                CI.Doubleloader = false;
                CI.isCalcSelected = true;
            }
            else
            {   
                //if doubleloader is false
                // and inputisnotdone is false
                //then add the output text and the arithmetic sign
                if (!CI.inputIsNotDone)
                {
                    textBoxCalculations.Text += textBoxOutput.Text + CalcMethod[(CI.selectedcalcmethod - 2)];
                    CI.isCalcSelected = true;
                }
                //otherwise the old arithmetic sign is replaced with a new one
                else
                {
                    textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);
                    textBoxCalculations.Text += CalcMethod[(CI.selectedcalcmethod - 2)];
                    CI.isCalcSelected = true;
                }
            }
        }
        public bool calculate(int operation)
        {
            //removeDecimal();
            if (!CI.inputIsNotDone)
            {
                //if there have been an input
                //And a calculation have been previously selected
                //then A calculation based on currentsum and the outputtextbox text will procced
                if (CI.isCalcSelected)
                {
                    Calkisen.Calculations((operation), double.Parse(textBoxOutput.Text, fmt));
                    textBoxOutput.Text = CI.currentSum.ToString();
                    if (Loggarn.DivideZeroFlag)
                    {
                        textBoxCalculations.Text = "";
                        MessageBox.Show("Can't Divide by 0. Calculation is Cleared");
                        Loggarn.DivideZeroFlag = false;
                        return false;
                    }
                    if (CI.currentSum> 1.7976931348623157E+308)
                    {
                        MessageBox.Show("Too big number. Calculations is resetting...");
                        buttonC_Click(null, null);
                    }
                    else if (-1.7976931348623157E+308 > CI.currentSum)
                    {
                        MessageBox.Show("Too small number. Calculations is resetting...");
                        buttonC_Click(null,null);
                    }
                }
                //otherwise the number in textboxoutput is saved in currentsum
                else
                {
                    CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
                }
            }
            //And as above so here if there have'nt been any input
            else
            {
                CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
            }
            return true;
        }
        #endregion

        #endregion




        //Sumbutton
        private void buttonSum_Click(object sender, EventArgs e)
        {

            removeDecimal();
            //Methods already gone through first update the calculationtextbox so all info
            //is brought to the listbox
            CalcTextboxUpdate(CI.selectedcalcmethod);
            
            //The old arithmetic sign is removed from the Calculationstring before adding it to the listbox
            textBoxCalculations.Text = textBoxCalculations.Text.Remove(textBoxCalculations.Text.Length - 1);

            //Then make sure the current calculation is carried out and if everything is ok...
            if (calculate(CI.selectedcalcmethod))
            {
                //... An item is added as  a historyrecord object unless there have been an error in calculations
                listBoxHistory.Items.Add(new HistoryRecord(textBoxCalculations.Text, double.Parse(textBoxOutput.Text, fmt)));
            }
            //This is basically a C button event but to avoid getting a canceling event i the logg
            //I reset it piece by piece

            #region CHANGE`S 1.
            //Old code, Would return 0 to the Output textbox after sum
            
            //textBoxOutput.Text = "0";
            //textBoxCalculations.Text = null;
            //CI.Creset();

            //New code, Let's the value of the outputbox retain it's value to the next calculation

            textBoxCalculations.Text = null;
            CI.Creset();
            CI.currentSum = double.Parse(textBoxOutput.Text, fmt);
            #endregion




        }

        //Clearbutton
        private void buttonC_Click(object sender, EventArgs e)
        {
            //Clears the 2 textboxes and resets the calculation flags and variabels
            textBoxOutput.Text = "0";
            textBoxCalculations.Text = null;
            Calkisen.ClearKey();
        }

        //Clear the entrytextBox
        private void buttonCE_Click(object sender, EventArgs e)
        {
            //Set the outputdata to 0
            textBoxOutput.Text = "0";
            CI.inputIsNotDone = true;
        }
        //Get PI
        private void buttonPi_Click(object sender, EventArgs e)
        {
            //gets PI from the calculator object method
            textBoxOutput.Text = Calkisen.PI().ToString();
            CI.inputIsNotDone = false;

        }
        //Get e
        private void buttonEuler_Click(object sender, EventArgs e)
        {
            //gets the Euler constant from the calculator object metod
            textBoxOutput.Text = Calkisen.Euler().ToString();
            CI.inputIsNotDone = false;
        }
        //SquareRoot
        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            //saftey method
            removeDecimal();
            if (!(double.Parse(textBoxOutput.Text,fmt)<0))
            {
                //unless the number in textboxoutput is less then 0 the squareroot is calculated
                textBoxOutput.Text = Calkisen.SquareRot(double.Parse(textBoxOutput.Text,fmt)).ToString();
                CI.inputIsNotDone = false;
            }
        }
        //Number times Number
        private void buttonSquare_Click(object sender, EventArgs e)
        {
            removeDecimal();
            if (textBoxOutput.Text != "∞")
            {
                textBoxOutput.Text = Calkisen.Power2(double.Parse(textBoxOutput.Text, fmt)).ToString();
                CI.inputIsNotDone = false;
            }
            else
            {
                buttonC_Click(null, null);
            }
            
        }
        //Delete the last entered letter
        private void buttonBackDelete_Click(object sender, EventArgs e)
        {
            //Unless it's empty the last number input is removed
            //Some check are being made to ensure we don't remove the E numbers or "E" itself
            if (textBoxOutput.Text.Length>=4 && textBoxOutput.Text[textBoxOutput.Text.Length-4]=='E')
            {
                textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 5,1);
                CI.inputIsNotDone = false;
            }else if (textBoxOutput.Text.Length >= 5 && textBoxOutput.Text[textBoxOutput.Text.Length - 5] == 'E')
            {
                textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 6,1);
                CI.inputIsNotDone = false;
            }else if (!(textBoxOutput.Text==null||textBoxOutput.Text==""))
            {
                textBoxOutput.Text = textBoxOutput.Text.Remove(textBoxOutput.Text.Length - 1);
                CI.inputIsNotDone = false;
            }
        }
        //Negate the entered number
        private void buttonPosNeg_Click(object sender, EventArgs e)
        {
            //The number in textboxoutput times -1
            textBoxOutput.Text = Calkisen.Invert(double.Parse(textBoxOutput.Text, fmt)).ToString();
            CI.inputIsNotDone = false;
        }
        //When the form closes
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //When the form is closed The data for the current session is serialised by these methods
            //More detail below...
            SaveHistory();
            SaveCurrentCalc();
        }


        #region listBoxMethods

        //Laddar object från listboxen när man dubbelklickar
        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Om man clickar där det är tomt så kör det inte
            if (listBoxHistory.SelectedItem != null)
            {
                //annars så Rensas det med kalkylatorn och Textboxarna får sin info plus att currentsum laddas
                Calkisen.ClearKey();
                textBoxCalculations.Text = ((HistoryRecord)listBoxHistory.SelectedItem).s;
                CI.currentSum = ((HistoryRecord)listBoxHistory.SelectedItem).sum;
                textBoxOutput.Text = CI.currentSum.ToString();
                CI.Doubleloader = true;
            }
        }

        //Rensar listan
        private void buttonDeleteHistory_Click(object sender, EventArgs e)
        {
            listBoxHistory.Items.Clear();
        }

        //Sparning av listboxen i filen "calculations.csv"
        private void buttonExport_Click(object sender, EventArgs e)
        {
            strömmen = File.Open("calculations.csv", FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(strömmen);

            //Loops each HistoryRecord in thelistbox so that a "," is put inbetween numbers and signs
            // and new lines for each calculation
            foreach (HistoryRecord item in listBoxHistory.Items)
            {
                //Here the StringBuilder is reset
                StringBuilder sb = new StringBuilder();
                //Every char in the calculation is gone through
                foreach (char c in item.ToString())
                {
                    //If one of these signs appears, "," is inserted
                    if (c == '+' || c == '-' || c == '*' || c == '/' || c == '=')
                    {
                        sb.Append(',');
                        sb.Append(c);
                        sb.Append(',');
                        
                    }
                    else
                    {
                        //Otherwise it's added as normal.
                        sb.Append(c);
                        
                    }
                }
                //here its written with the streamwriter
                sr.Write(sb.ToString());
                sr.Write("\r\n");
                
            }
            sr.Flush();
            sr.Close();
            strömmen.Close();
        }
        #endregion
        
        //Serializes the Calcinfo into "Restore1.bin"
        public void SaveCurrentCalc()
        {
            strömmen = File.Open("Restore1.bin", FileMode.Create, FileAccess.Write);
            CI.UpdateOutputInfo(textBoxCalculations.Text,textBoxOutput.Text);
            binForm.Serialize(strömmen, CI);
            strömmen.Close();

        }
        //Deserializes "Restore1.bin" and load it into The CalcInfo
        public void LoadCurrentCalc()
        {
            //If the file exist...
            if (File.Exists("Restore1.bin"))
            { 
                strömmen = File.Open("Restore1.bin", FileMode.Open, FileAccess.Read);
                //...and if the streamLength isn't 0
                if (!(strömmen.Length==0))
                {
                    //then the stream is deserialzed into CI
                    CI = (Calcinfo)binForm.Deserialize(strömmen);
                }

                //Some textbox updates
                textBoxCalculations.Text = CI.Calculations;
                textBoxOutput.Text = CI.Output;
                strömmen.Close(); 
            }
        }

        //Serializes the listbox into "Restore2.bin"
        public void SaveHistory()
        {
            //Stream is opened
            strömmen = File.Open("Restore2.bin", FileMode.Create, FileAccess.Write);
            //The object to be serialized is created (list)
            List<HistoryRecord> list = new List<HistoryRecord>();
            //All HistoryRecords are added
            foreach (HistoryRecord item in listBoxHistory.Items)
            {
                list.Add(item);
            }
            //Serialize and close
            binForm.Serialize(strömmen, list);
            strömmen.Close();
        }
        public void LoadHistory()
        {
            // Some basic checks to avoid craches
            //does it exist?
            if (File.Exists("Restore2.bin"))
            {
                strömmen = File.Open("Restore2.bin", FileMode.Open, FileAccess.Read);
                List<HistoryRecord> list = new List<HistoryRecord>();
                //is it empty?
                if (!(strömmen.Length==0))
                {
                    //and then we go
                    list = (List<HistoryRecord>)binForm.Deserialize(strömmen);
                    //every item is added
                    foreach (HistoryRecord item in list)
                    {
                        listBoxHistory.Items.Add(item);
                    }
                }
                //and close
                strömmen.Close();
            }
        }

        //this method might be excessive after my Fomatinfo update but i save it as a relic
        //It Switches "." with ","
        public void removeDecimal()
        {
            string s = textBoxOutput.Text;
            textBoxOutput.Text = "";
            foreach (char c in s)
            {
                //if a char is "." then add ","
                if(c=='.')
                {
                    textBoxOutput.Text += ',';
                }
                //otherwise add the normal char
                else
                {
                    textBoxOutput.Text += c;
                }
            }
        }
        //Utifall att man går utanför typens ramar
        private void textBoxOutput_TextChanged(object sender, EventArgs e)
        {
            if(textBoxOutput.Text.Contains('∞'))
            {
                MessageBox.Show("Too big or too small number for the current type. \r\n Hence the calculation will be cancelled. \r\n Be more carefull next time ;P");
                buttonC_Click(null, null);
            }
        }
    }
}
