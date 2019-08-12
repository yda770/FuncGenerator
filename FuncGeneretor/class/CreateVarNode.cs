using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class CreateVarNode : CodeNode
    {
        public int NextVarNum { get; set; }

        public override FuncCodeAndDesc getFuncStringRand(int levels, int condLevel, CodeNode vars)
        {
            int ran3;
            FuncCodeAndDesc inside1Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc inside2Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc AfterGen = new FuncCodeAndDesc();
            FuncCodeAndDesc MyGenCode = new FuncCodeAndDesc();

            inside1Gen.FuncCode = "myVar" + this.NextVarNum;
            CodeNode NewVar = new CodeNode();
            NewVar.CodeStart = inside1Gen.FuncCode ;
            NewVar.descriptions.Add(inside1Gen.FuncCode);
            vars.CodeNodesInside1.Add(NewVar);
            this.NextVarNum++;

            if (this.CodeNodesAfter.Any())
            {
                if (levels > 0)
                {
                    levels--;
                    ran3 = AfterRand.Next(0, this.CodeNodesAfter.Count);
                    AfterGen = this.CodeNodesAfter[ran3].getFuncStringRand(levels,condLevel, vars);
                }
            }

            MyGenCode.FuncCode = this.CodeStart + inside1Gen.FuncCode + this.CodeEnd1 + inside2Gen.FuncCode + this.CodeEnd2 + AfterGen.FuncCode;
            MyGenCode.FuncDesc = this.GetFuncDesc(inside1Gen.FuncDesc, inside2Gen.FuncDesc, AfterGen.FuncDesc);
            return MyGenCode;
        }
    }
}