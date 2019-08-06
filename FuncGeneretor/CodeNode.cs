using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class CodeNode
    {
       public string CodeStart { get; set; }
       public List<CodeNode> CodeNodesInside1 { get; set; }
       public string CodeEnd1 { get; set; }
       public List<CodeNode> CodeNodesInside2 { get; set; }
       public string CodeEnd2 { get; set; }
       public List<CodeNode> CodeNodesAfter { get; set; }
       public bool isHaveThenAfter { get; set; }
       public List<string> descriptions { get; set; }
       protected Random ins1Rand;
       protected Random ins2Rand ;
       protected Random AfterRand;

        public CodeNode()
        {
            descriptions = new List<string>();
            CodeNodesInside1 = new List<CodeNode>();
            CodeNodesInside2 = new List<CodeNode>();
            CodeNodesAfter = new List<CodeNode>();
            ins1Rand = new Random();
            ins2Rand = new Random();
            AfterRand = new Random();
        }

        public virtual string getFuncStringRand(int levels, CodeNode vars)
        {

            int ran1;
            int ran2;
            int ran3;
            string inside1Gen = "";
            string inside2Gen = "";
            string AfterGen = "";

            if  (this.CodeNodesInside1.Any())
            {
                ran1 = ins1Rand.Next(0, this.CodeNodesInside1.Count);
                inside1Gen = this.CodeNodesInside1[ran1].getFuncStringRand(levels, vars);
            }

            if (this.CodeNodesInside2.Any()) 
            {
                ran2 = ins2Rand.Next(0, this.CodeNodesInside2.Count);
                inside2Gen = this.CodeNodesInside2[ran2].getFuncStringRand(levels, vars);
            }

            if (this.CodeNodesAfter.Any())
            {
                if (levels > 0)
                {
                    levels--;
                    ran3 = AfterRand.Next(0, this.CodeNodesAfter.Count);
                    AfterGen = this.CodeNodesAfter[ran3].getFuncStringRand(levels, vars);
                }
            }

            return CodeStart + inside1Gen + this.CodeEnd1 + inside2Gen + this.CodeEnd2 + AfterGen;
        }
    }
}