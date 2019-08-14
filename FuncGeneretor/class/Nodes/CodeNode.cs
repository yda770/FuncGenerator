using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace FuncGeneretor
{
    public class CodeNode
    {
       public string CodeStart { get; set; }
       public List<string> descriptions{ get; set; }
       public List<CodeNode> CodeNodesInside1 { get; set; }
       public string CodeEnd1 { get; set; }
       //public List<string> descriptionsEnd1 { get; set; }
       public List<CodeNode> CodeNodesInside2 { get; set; }
       public string CodeEnd2 { get; set; }
       //public List<string> descriptionsEnd2 { get; set; }
       public List<CodeNode> CodeNodesAfter { get; set; }
       public bool isHaveThenAfter { get; set; }
       
       protected static Random random = new Random();

        public CodeNode()
        {
            this.descriptions = new List<string>();
            this.CodeNodesInside1 = new List<CodeNode>();
            this.CodeNodesInside2 = new List<CodeNode>();
            this.CodeNodesAfter = new List<CodeNode>();
        }

        public virtual FuncCodeAndDesc getFuncStringRand(int levels, int condLevel, CodeNode vars)
        {
            int ran1;
            int ran2;
            int ran3;
            FuncCodeAndDesc inside1Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc inside2Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc AfterGen   = new FuncCodeAndDesc();
            FuncCodeAndDesc MyGenCode  = new FuncCodeAndDesc();

            if  (this.CodeNodesInside1.Any())
            {
                ran1 = random.Next(0, this.CodeNodesInside1.Count);
                inside1Gen = this.CodeNodesInside1[ran1].getFuncStringRand(levels, condLevel, vars);
            }

            if (this.CodeNodesInside2.Any()) 
            {
                ran2 = random.Next(0, this.CodeNodesInside2.Count);
                inside2Gen = this.CodeNodesInside2[ran2].getFuncStringRand(levels, condLevel, vars);
            }

            if (this.CodeNodesAfter.Any())
            {
                if (levels > 0)
                {
                    levels--;
                    ran3 = random.Next(0, this.CodeNodesAfter.Count);
                    AfterGen = this.CodeNodesAfter[ran3].getFuncStringRand(levels, condLevel, vars);
                }
            }
            
            MyGenCode.FuncCode = this.CodeStart + inside1Gen.FuncCode + this.CodeEnd1 + inside2Gen.FuncCode + this.CodeEnd2 + AfterGen.FuncCode;
            MyGenCode.FuncDesc = this.GetFuncDesc(inside1Gen.FuncDesc, inside2Gen.FuncDesc, AfterGen.FuncDesc);
            return MyGenCode; 
        }

        public virtual string GetFuncDesc(string inside1, string inside2, string after)
        {
            int rand;
            string myDesc = "";
            if (this.descriptions.Any())
            {

                rand = random.Next(0, this.descriptions.Count);
                myDesc = this.descriptions[rand];
            }
            
            return myDesc + " " + inside1 + " " + inside2 + " " + after;
        }

    }
}