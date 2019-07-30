using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuncGeneretor
{
    public class CodeNode
    {
       public string Code { get; set; }
       public string CodeEnd { get; set; }
       public List<string> descriptions { get; set; }
       public List<CodeNode> CodeNodes { get; set; }
       public List<CodeNode> CodeNodesInside { get; set; }
        public string CodeEndInside { get; set; }
        public bool isHaveThenAfter { get; set; }


        public CodeNode(string code)
        {
            descriptions = new List<string>();
            CodeNodes = new List<CodeNode>();
            Code = code;
        }

        public void AddCodeNode(CodeNode codeNode)
        {
            this.CodeNodes.Add(codeNode);
        }

        public void AddDescription(string description)
        {
            this.descriptions.Add(description);
        }
    }
}