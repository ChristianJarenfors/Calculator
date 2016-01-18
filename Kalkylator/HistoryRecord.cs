﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wincalcmini

{
    class HistoryRecord:Object
    {
        public string s;
        public double sum;
        public HistoryRecord(string text, double Sum)
        {
            s = text;
            sum = Sum;
        }
        public override string ToString()
        {
            return s + " = " + sum.ToString();
        }
    }
}