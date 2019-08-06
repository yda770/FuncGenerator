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
        public CodeNode ForNode { get; set; }
        //public CodeNode WhileNode { get; set; }
        public CodeNode PlacementNode { get; set; }
        public CodeNode Number { get; set; }
        public CodeNode Boolean { get; set; }
        public CodeNode Variable { get; set; }
        public CodeNode NextNewVar { get; set; }
        public CodeNode CreateVar { get; set; }
        public CodeNode ArithmeticUnary { get; set; }

        public int VarNum { get; set; }

        public List<FuncCodeAndDesc> CodeAndDesc { get; set; }

        public void BuildCodeGraph()
        {
            
            this.GraphHead       = new CodeNode();
            this.ReturnNode      = new CodeNode();
            this.ArithmeticNode  = new CodeNode();
            this.OperandsNode    = new CodeNode();
            this.ConditionNode   = new CodeNode();
            this.ForNode         = new CodeNode();
            //this.WhileNode      = new CodeNode();
            this.PlacementNode   = new CodeNode();
            this.Number          = new CodeNode();
            this.Boolean         = new CodeNode();
            this.Variable        = new CodeNode();
            this.NextNewVar      = new CodeNode();
            this.CreateVar       = new CreateVarNode();
            this.ArithmeticUnary = new CodeNode();

            GraphHead.CodeStart = "function(var A, var B){";
            GraphHead.descriptions.Add("Get two parameters A and B ");
            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd1 = "}";

            this.BuildConditionNodes();
            this.BuildReturnNodes();
            this.BuildArithmeticNode();
            this.BuildOperandsCode();
            this.BuildNumberVars();
            this.BuildPlacment();
            this.BuildCreateVar();
            this.BuildForNode();

            GraphHead.CodeNodesInside1.Add(this.ReturnNode);
            GraphHead.CodeNodesInside1.Add(this.ConditionNode);
            GraphHead.CodeNodesInside1.Add(this.ForNode);
            //GraphHead.CodeNodesInside1.Add(this.WhileNode);
            GraphHead.CodeNodesInside1.Add(this.PlacementNode);
            GraphHead.CodeNodesInside1.Add(this.ArithmeticUnary);
        }

        private void BuildForNode()
        {
            CodeNode ForArithNode = new CodeNode();
            CodeNode ForPlacment = new CodeNode();
            CodeNode ForArirhUnary = new CodeNode();


            ForArithNode.CodeNodesInside1 = this.ArithmeticNode.CodeNodesInside1;

            ForPlacment.CodeEnd1 = " = ";
            ForPlacment.CodeNodesInside1 = this.PlacementNode.CodeNodesInside1;
            ForPlacment.CodeNodesInside2 = this.PlacementNode.CodeNodesInside2;
            ForArirhUnary.CodeNodesInside1 = this.ArithmeticUnary.CodeNodesInside1;
            ForArirhUnary.CodeNodesInside2 = this.ArithmeticUnary.CodeNodesInside2;

            CodeNode ForInside = new CodeNode();
            ForInside.CodeNodesInside1.Add(ForPlacment);
            ForInside.CodeEnd1 = ";";
            ForInside.CodeNodesInside2.Add(this.OperandsNode);
            ForInside.CodeEnd2 = ";";
            ForInside.CodeNodesAfter.Add(ForPlacment);
            ForInside.CodeNodesAfter.Add(ForArirhUnary);

            this.ForNode.CodeStart = "for (";
            this.ForNode.CodeNodesInside1.Add(ForInside);
            this.ForNode.CodeEnd1 = "){";
            this.ForNode.CodeEnd2 = "}";

            // Inside 2
            this.ForNode.CodeNodesInside2.Add(this.ReturnNode);
            this.ForNode.CodeNodesInside2.Add(this.ConditionNode);
            this.ForNode.CodeNodesInside2.Add(this.ForNode);
            //this.ForNode.CodeNodesInside2.Add(this.WhileNode);
            this.ForNode.CodeNodesInside2.Add(this.PlacementNode);
            this.ForNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            this.ForNode.CodeNodesInside2.Add(this.CreateVar);

            // After
            this.ForNode.CodeNodesAfter.Add(this.ReturnNode);
            this.ForNode.CodeNodesAfter.Add(this.ConditionNode);
            this.ForNode.CodeNodesAfter.Add(this.ForNode);
            //this.ForNode.CodeNodesAfter.Add(this.WhileNode);
            this.ForNode.CodeNodesAfter.Add(this.PlacementNode);
            this.ForNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.ForNode.CodeNodesAfter.Add(this.CreateVar);

        }

        private void BuildCreateVar()
        {
            ((CreateVarNode)this.CreateVar).NextVarNum = 1;
            this.CreateVar.CodeStart = "var " ;
            this.CreateVar.CodeEnd1 = ";";

            this.CreateVar.CodeNodesAfter.Add(this.ReturnNode);
            this.CreateVar.CodeNodesAfter.Add(this.ConditionNode);
            this.CreateVar.CodeNodesAfter.Add(this.ForNode);
            //this.CreateVar.CodeNodesAfter.Add(this.WhileNode);
            this.CreateVar.CodeNodesAfter.Add(this.PlacementNode);
            this.CreateVar.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.CreateVar.CodeNodesAfter.Add(this.CreateVar);

        }

        private void BuildPlacment()
        {
            this.PlacementNode.CodeEnd1 = " = ";
            this.PlacementNode.CodeNodesInside1.Add(this.Variable);
            this.PlacementNode.CodeNodesInside2.Add(this.Variable);
            this.PlacementNode.CodeNodesInside2.Add(this.Number);
            this.PlacementNode.CodeNodesInside2.Add(this.Boolean);
            this.PlacementNode.CodeNodesInside2.Add(this.OperandsNode);
            this.PlacementNode.CodeNodesInside2.Add(this.ArithmeticNode);
            this.PlacementNode.CodeEnd2 = ";";


            // After 
            this.PlacementNode.CodeNodesAfter.Add(this.ReturnNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ConditionNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ForNode);
            //this.PlacementNode.CodeNodesAfter.Add(this.WhileNode);
            this.PlacementNode.CodeNodesAfter.Add(this.PlacementNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.PlacementNode.CodeNodesAfter.Add(this.CreateVar);
        }

        private void BuildNumberVars()
        {
            CodeNode Number1 = new CodeNode();
            Number1.CodeStart = "1";

            CodeNode Number2 = new CodeNode();
            Number2.CodeStart = "2";

            CodeNode Number3 = new CodeNode();
            Number3.CodeStart = "3";

            CodeNode Number4 = new CodeNode();
            Number4.CodeStart = "4";

            CodeNode Number5 = new CodeNode();
            Number5.CodeStart = "5";

            CodeNode Number6 = new CodeNode();
            Number6.CodeStart = "6";

            CodeNode Number7 = new CodeNode();
            Number7.CodeStart = "7";

            CodeNode Number8 = new CodeNode();
            Number8.CodeStart = "8";

            CodeNode Number9 = new CodeNode();
            Number9.CodeStart = "9";

            CodeNode Number10 = new CodeNode();
            Number10.CodeStart = "10";

            this.Number.CodeNodesInside1.Add(Number1);
            this.Number.CodeNodesInside1.Add(Number2);
            this.Number.CodeNodesInside1.Add(Number3);
            this.Number.CodeNodesInside1.Add(Number4);
            this.Number.CodeNodesInside1.Add(Number5);
            this.Number.CodeNodesInside1.Add(Number6);
            this.Number.CodeNodesInside1.Add(Number7);
            this.Number.CodeNodesInside1.Add(Number8);
            this.Number.CodeNodesInside1.Add(Number9);
            this.Number.CodeNodesInside1.Add(Number10);

            CodeNode True = new CodeNode();
            True.CodeStart = "true";

            CodeNode False = new CodeNode();
            False.CodeStart = "false";

            this.Boolean.CodeNodesInside1.Add(True);
            this.Boolean.CodeNodesInside1.Add(False);
            
            CodeNode A = new CodeNode();
            A.CodeStart = "A";

            CodeNode B = new CodeNode();
            B.CodeStart = "B";

            this.Variable.CodeNodesInside1.Add(A);
            this.Variable.CodeNodesInside1.Add(B);
        }

        private void BuildOperandsCode()
        {
            this.OperandsNode.CodeStart = "(";
            this.OperandsNode.isHaveThenAfter = true;
            this.OperandsNode.CodeEnd1 = ")";

            CodeNode OrNode = new CodeNode();
            OrNode.CodeEnd1 = " || ";
            OrNode.CodeNodesInside1.Add(this.Variable);
            OrNode.CodeNodesInside2.Add(this.Variable);
            OrNode.CodeNodesInside1.Add(this.OperandsNode);
            OrNode.CodeNodesInside2.Add(this.OperandsNode);

            CodeNode AndNode = new CodeNode();
            AndNode.CodeEnd1 = " && ";
            AndNode.CodeNodesInside1.Add(this.Variable);
            AndNode.CodeNodesInside2.Add(this.Variable);
            OrNode.CodeNodesInside1.Add(this.OperandsNode);
            OrNode.CodeNodesInside2.Add(this.OperandsNode);

            CodeNode EqualNode = new CodeNode();
            EqualNode.CodeEnd1 = " == ";
            EqualNode.CodeNodesInside1.Add(this.Variable);
            EqualNode.CodeNodesInside2.Add(this.Variable);
            EqualNode.CodeNodesInside1.Add(this.ArithmeticNode);
            EqualNode.CodeNodesInside2.Add(this.ArithmeticNode);
            EqualNode.CodeNodesInside1.Add(this.OperandsNode);
            EqualNode.CodeNodesInside2.Add(this.OperandsNode);
            EqualNode.CodeNodesInside2.Add(this.Boolean);
            EqualNode.CodeNodesInside2.Add(this.Number);

            CodeNode NotEqualNode = new CodeNode();
            NotEqualNode.CodeEnd1 = " != ";
            NotEqualNode.CodeNodesInside1.Add(this.Variable);
            NotEqualNode.CodeNodesInside2.Add(this.Variable);
            NotEqualNode.CodeNodesInside1.Add(this.ArithmeticNode);
            NotEqualNode.CodeNodesInside2.Add(this.ArithmeticNode);
            NotEqualNode.CodeNodesInside1.Add(this.OperandsNode);
            NotEqualNode.CodeNodesInside2.Add(this.OperandsNode);
            NotEqualNode.CodeNodesInside2.Add(this.Boolean);
            NotEqualNode.CodeNodesInside2.Add(this.Number);

            CodeNode GreatNode = new CodeNode();
            GreatNode.CodeEnd1 = " > ";
            GreatNode.CodeNodesInside1.Add(this.Variable);
            GreatNode.CodeNodesInside2.Add(this.Variable);
            GreatNode.CodeNodesInside1.Add(this.OperandsNode);
            GreatNode.CodeNodesInside2.Add(this.OperandsNode);
            GreatNode.CodeNodesInside1.Add(this.ArithmeticNode);
            GreatNode.CodeNodesInside2.Add(this.ArithmeticNode);
            GreatNode.CodeNodesInside2.Add(this.Number);

            CodeNode LitleNode = new CodeNode();
            LitleNode.CodeEnd1 = " < ";
            LitleNode.CodeNodesInside1.Add(this.Variable);
            LitleNode.CodeNodesInside2.Add(this.Variable);
            LitleNode.CodeNodesInside1.Add(this.OperandsNode);
            LitleNode.CodeNodesInside2.Add(this.OperandsNode);
            LitleNode.CodeNodesInside1.Add(this.ArithmeticNode);
            LitleNode.CodeNodesInside2.Add(this.ArithmeticNode);
            LitleNode.CodeNodesInside2.Add(this.Number);

            this.OperandsNode.CodeNodesInside1.Add(OrNode);
            this.OperandsNode.CodeNodesInside1.Add(AndNode);
            this.OperandsNode.CodeNodesInside1.Add(EqualNode);
            this.OperandsNode.CodeNodesInside1.Add(NotEqualNode);
            this.OperandsNode.CodeNodesInside1.Add(GreatNode);
            this.OperandsNode.CodeNodesInside1.Add(LitleNode);
        }

        private void BuildArithmeticNode()
        {
            CodeNode UnaryPlus = new CodeNode();
            UnaryPlus.CodeStart = "++";

            CodeNode UnaryMinus = new CodeNode();
            UnaryMinus.CodeStart = "--";

            this.ArithmeticUnary.CodeNodesInside1.Add(this.Variable);
            this.ArithmeticUnary.CodeNodesInside2.Add(UnaryPlus);
            this.ArithmeticUnary.CodeNodesInside2.Add(UnaryMinus);
            this.ArithmeticUnary.CodeEnd2 = ";";

            // After 
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ReturnNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ConditionNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ForNode);
            //this.ArithmeticUnary.CodeNodesAfter.Add(this.WhileNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.PlacementNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.CreateVar);

            this.ArithmeticNode.CodeStart = "(";
            this.ArithmeticNode.isHaveThenAfter = true;
            this.ArithmeticNode.CodeEnd1 = ")";

            CodeNode AddNode = new CodeNode();
            AddNode.CodeEnd1 = " + ";
            AddNode.CodeNodesInside1.Add(this.Number);
            AddNode.CodeNodesInside2.Add(this.Number);
            AddNode.CodeNodesInside1.Add(this.ArithmeticNode);
            AddNode.CodeNodesInside2.Add(this.ArithmeticNode);
            AddNode.CodeNodesInside1.Add(this.Variable);
            AddNode.CodeNodesInside2.Add(this.Variable);

            CodeNode SubNode = new CodeNode();
            SubNode.CodeEnd1 = " - ";
            SubNode.CodeNodesInside1.Add(this.Number);
            SubNode.CodeNodesInside2.Add(this.Number);
            SubNode.CodeNodesInside1.Add(this.ArithmeticNode);
            SubNode.CodeNodesInside2.Add(this.ArithmeticNode);
            SubNode.CodeNodesInside1.Add(this.Variable);
            SubNode.CodeNodesInside2.Add(this.Variable);

            CodeNode DivNode = new CodeNode();
            DivNode.CodeEnd1 = " / ";
            DivNode.CodeNodesInside1.Add(this.Number);
            DivNode.CodeNodesInside2.Add(this.Number);
            DivNode.CodeNodesInside1.Add(this.ArithmeticNode);
            DivNode.CodeNodesInside2.Add(this.ArithmeticNode);
            DivNode.CodeNodesInside1.Add(this.Variable);
            DivNode.CodeNodesInside2.Add(this.Variable);

            CodeNode MulNode = new CodeNode();
            MulNode.CodeEnd1 = " * ";
            MulNode.CodeNodesInside1.Add(this.Number);
            MulNode.CodeNodesInside2.Add(this.Number);
            MulNode.CodeNodesInside1.Add(this.ArithmeticNode);
            MulNode.CodeNodesInside2.Add(this.ArithmeticNode);
            MulNode.CodeNodesInside1.Add(this.Variable);
            MulNode.CodeNodesInside2.Add(this.Variable);

            CodeNode PowNode = new CodeNode();
            PowNode.CodeEnd1 = " ^ ";
            PowNode.CodeNodesInside1.Add(this.Number);
            PowNode.CodeNodesInside2.Add(this.Number);
            PowNode.CodeNodesInside1.Add(this.ArithmeticNode);
            PowNode.CodeNodesInside2.Add(this.ArithmeticNode);
            PowNode.CodeNodesInside1.Add(this.Variable);
            PowNode.CodeNodesInside2.Add(this.Variable);

            // Indide 1
            this.ArithmeticNode.CodeNodesInside1.Add(AddNode);
            this.ArithmeticNode.CodeNodesInside1.Add(SubNode);
            this.ArithmeticNode.CodeNodesInside1.Add(DivNode);
            this.ArithmeticNode.CodeNodesInside1.Add(MulNode);
            this.ArithmeticNode.CodeNodesInside1.Add(PowNode);

        }

        public void BuildConditionNodes()
        {
            this.ConditionNode.CodeStart = "if (";
            this.ConditionNode.isHaveThenAfter = true;
            this.ConditionNode.CodeEnd1 = "){";
            this.ConditionNode.CodeEnd2 = "}";

            CodeNode ElseIfNode = new CodeNode();
            ElseIfNode.CodeStart = "else if (";
            ElseIfNode.CodeEnd1 = "){";
            ElseIfNode.CodeEnd2 = "}";

            CodeNode ElseNode = new CodeNode();
            ElseNode.CodeStart = "else{";
            ElseNode.CodeEnd1 = "}";

            // After 
           this.ConditionNode.CodeNodesAfter.Add(ElseNode);
           this.ConditionNode.CodeNodesAfter.Add(ElseIfNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ReturnNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ConditionNode);
            this.ConditionNode.CodeNodesAfter.Add(ForNode);
            //this.ConditionNode.CodeNodesAfter.Add(WhileNode);
            this.ConditionNode.CodeNodesAfter.Add(this.PlacementNode);
            this.ConditionNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.ConditionNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 1
            this.ConditionNode.CodeNodesInside1.Add(this.OperandsNode);

            // Inside 2 
            this.ConditionNode.CodeNodesInside2.Add(this.ReturnNode);
            this.ConditionNode.CodeNodesInside2.Add(this.ConditionNode);
            this.ConditionNode.CodeNodesInside2.Add(ForNode);
            //this.ConditionNode.CodeNodesInside2.Add(WhileNode);
            this.ConditionNode.CodeNodesInside2.Add(this.PlacementNode);
            this.ConditionNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            this.ConditionNode.CodeNodesAfter.Add(this.CreateVar);

            // After 
            ElseIfNode.CodeNodesAfter.Add(ElseIfNode);
            ElseIfNode.CodeNodesAfter.Add(ElseNode);
            ElseIfNode.CodeNodesAfter.Add(this.ReturnNode);
            ElseIfNode.CodeNodesAfter.Add(this.ConditionNode);
            ElseIfNode.CodeNodesAfter.Add(this.ForNode);
            //ElseIfNode.CodeNodesAfter.Add(this.WhileNode);
            ElseIfNode.CodeNodesAfter.Add(this.PlacementNode);
            ElseIfNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            ElseIfNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 1
            ElseIfNode.CodeNodesInside1.Add(this.OperandsNode);
            
            // Inside 2 
            ElseIfNode.CodeNodesInside2.Add(this.ReturnNode);
            ElseIfNode.CodeNodesInside2.Add(this.ConditionNode);
            ElseIfNode.CodeNodesInside2.Add(this.ForNode);
            //ElseIfNode.CodeNodesInside2.Add(this.WhileNode);
            ElseIfNode.CodeNodesInside2.Add(this.PlacementNode);
            ElseIfNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            ElseIfNode.CodeNodesInside2.Add(this.CreateVar);

            // After 
            ElseNode.CodeNodesAfter.Add(this.ReturnNode);
            ElseNode.CodeNodesAfter.Add(this.ConditionNode);
            ElseNode.CodeNodesAfter.Add(this.ForNode);
            //ElseNode.CodeNodesAfter.Add(this.WhileNode);
            ElseNode.CodeNodesAfter.Add(this.PlacementNode);
            ElseNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            ElseNode.CodeNodesInside2.Add(this.CreateVar);

            // Inside 2 
            ElseNode.CodeNodesInside1.Add(this.ReturnNode);
            ElseNode.CodeNodesInside1.Add(this.ConditionNode);
            ElseNode.CodeNodesInside2.Add(this.ForNode);
            //ElseNode.CodeNodesInside2.Add(this.WhileNode);
            ElseNode.CodeNodesInside2.Add(this.PlacementNode);
            ElseNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            ElseNode.CodeNodesInside2.Add(this.CreateVar);
        }

        public void BuildReturnNodes()
        {
            ReturnNode.CodeStart = "return (";
            ReturnNode.isHaveThenAfter = false;
            ReturnNode.CodeEnd1 = ");";

            this.ReturnNode.CodeNodesInside1.Add(this.Number);
            this.ReturnNode.CodeNodesInside1.Add(this.OperandsNode);
            this.ReturnNode.CodeNodesInside1.Add(this.ArithmeticNode);
            this.ReturnNode.CodeNodesInside1.Add(this.Variable);
            this.ReturnNode.CodeNodesInside1.Add(this.Boolean);

        }

        public string PrintRandomFunction( )
        {
            this.BuildCodeGraph();
            
            return this.GraphHead.getFuncStringRand(5, this.Variable);
            
        }
    }
}