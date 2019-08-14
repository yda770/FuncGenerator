using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class ForInsideNode : CodeNode
    {
        public override FuncCodeAndDesc getFuncStringRand(int levels, int condLevel, CodeNode vars)
        {
            int ran1;
            int ran2;
            int ran3;
            FuncCodeAndDesc inside1Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc inside2Gen = new FuncCodeAndDesc();
            FuncCodeAndDesc AfterGen = new FuncCodeAndDesc();
            FuncCodeAndDesc MyGenCode = new FuncCodeAndDesc();

            if (this.CodeNodesInside1.Any())
            {
                ran1 = random.Next(0, this.CodeNodesInside1.Count);
                inside1Gen = this.CodeNodesInside1[ran1].getFuncStringRand(levels,condLevel, vars);
            }

            if (this.CodeNodesInside2.Any())
            {
                ran2 = random.Next(0, this.CodeNodesInside2.Count);
                inside2Gen = this.CodeNodesInside2[ran2].getFuncStringRand(levels,condLevel, vars);
            }

            if (this.CodeNodesAfter.Any())
            {
                    ran3 = random.Next(0, this.CodeNodesAfter.Count);
                    AfterGen = this.CodeNodesAfter[ran3].getFuncStringRand(levels,condLevel, vars);
            }

            MyGenCode.FuncCode = this.CodeStart + inside1Gen.FuncCode + this.CodeEnd1 + inside2Gen.FuncCode + this.CodeEnd2 + AfterGen.FuncCode;
            MyGenCode.FuncDesc = this.GetFuncDesc(inside1Gen.FuncDesc + ";", inside2Gen.FuncDesc + ";", AfterGen.FuncDesc);
            return MyGenCode;
        }
    }
}