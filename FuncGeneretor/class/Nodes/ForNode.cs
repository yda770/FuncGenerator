using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace FuncGeneretor
{
    public class ForNode : CodeNode    
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
            int index1;
            int index2;

            index1 = inside1.IndexOf(";");
            index2 = inside1.IndexOf(";", index1 + 1);
            myDesc = myDesc.Replace("<palcement>", inside1.Substring(0, index1));
            myDesc = myDesc.Replace("<condition>", inside1.Substring(index1 + 1, index2 - index1 - 1));
            myDesc = myDesc.Replace("<arithmetic>", inside1.Substring(index2 + 1));
            myDesc = myDesc.Replace(".", "");
            return myDesc + inside2 + after;
        }

    }
}