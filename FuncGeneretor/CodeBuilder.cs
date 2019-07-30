using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class CodeBuilder
    {
        public CodeNode GraphHead { get; set; }
        public CodeNode ReturnNode { get; set; }
        public CodeNode ArithmeticNode { get; set; }
        public CodeNode OperandsNode { get; set; }
        public CodeNode ConditionNode { get; set; }
        public CodeNode LoopNode { get; set; }


        public List<FuncCodeAndDesc> CodeAndDesc { get; set; }

        public void BuildCodeGraph()
        {
            GraphHead = new CodeNode("function(var A, var B){ ");
            GraphHead.AddDescription("Get two parameters A and B ");
            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd = " }";

            ReturnNode = new CodeNode("return (");
            GraphHead.AddDescription("");
            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd = ");";

            ArithmeticNode = new CodeNode("( ");
            GraphHead.AddDescription("");
            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd = " )";

            OperandsNode = new CodeNode("( ");
            GraphHead.AddDescription("");
            GraphHead.isHaveThenAfter = true;

            ConditionNode = new CodeNode("if( ");
            GraphHead.AddDescription("");
            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd = "){";
            GraphHead.CodeEndInside = " }";
        }

        public void CreateFuncCodes(int iterations)
        {


        }

        public void PrintNode(CodeNode codeNode, FuncCodeAndDesc funcCodeDesc )
        {

            funcCodeDesc.FuncCode += codeNode.Code;
            foreach (string desc in codeNode.descriptions)
            {
                funcCodeDesc.FuncDesc = desc;

                foreach (CodeNode node in codeNode.CodeNodes)
                {
                    this.PrintNode(node, funcCodeDesc);
                }
            }
          
            funcCodeDesc.FuncCode = codeNode.CodeEnd;

            foreach (CodeNode nodeInside in codeNode.CodeNodesInside)
            {
                this.PrintNode(nodeInside, funcCodeDesc);
            }

            funcCodeDesc.FuncCode = codeNode.CodeEndInside;

        }
    }
}