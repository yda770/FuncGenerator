using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class CreateVarNode : CodeNode
    {
        public int NextVarNum { get; set; }

        public override string getFuncStringRand(int levels, CodeNode vars)
        {
            int ran3;
            string inside1Gen = "";
            string inside2Gen = "";
            string AfterGen = "";


            inside1Gen = "myVar" + this.NextVarNum;
            CodeNode NewVar = new CodeNode();
            NewVar.CodeStart = inside1Gen;
            vars.CodeNodesInside1.Add(NewVar);
            this.NextVarNum++;

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