﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace FuncGeneretor
{
    public class IfNode : CodeNode    
    {
       override public string GetFuncDesc(string inside1, string inside2, string after)
        {
            int rand;
            string myDesc = "";
            if (this.descriptions.Any())
            {

                rand = random.Next(0, this.descriptions.Count);
                myDesc = this.descriptions[rand];
            }

            myDesc = myDesc.Replace("<cond>", inside1);
            myDesc = myDesc.Replace("<inside>", inside2);

            return myDesc + after;
        }

    }
}