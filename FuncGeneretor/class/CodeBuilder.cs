﻿using System;
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
        public CodeNode WhileNode { get; set; }
        public CodeNode PlacementNode { get; set; }
        public CodeNode Number { get; set; }
        public CodeNode Boolean { get; set; }
        public CodeNode Variable { get; set; }
        public CodeNode NextNewVar { get; set; }
        public CodeNode CreateVar { get; set; }
        public CodeNode ArithmeticUnary { get; set; }
        public CodeNode NotCondNode { get; set; }    // TODO
        public CodeNode StringVariable { get; set; } // TODO
        public CodeNode ScreenOut { get; set; }      // TODO

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
            this.WhileNode       = new CodeNode();
            this.PlacementNode   = new CodeNode();
            this.Number          = new CodeNode();
            this.Boolean         = new CodeNode();
            this.Variable        = new CodeNode();
            this.NextNewVar      = new CodeNode();
            this.CreateVar       = new CreateVarNode();
            this.ArithmeticUnary = new CodeNode();

            GraphHead.CodeStart = "function func(var A, var B){";

            GraphHead.isHaveThenAfter = true;
            GraphHead.CodeEnd1 = "}";
            GraphHead.descriptions.Add("Get two parameters A and B ");
            GraphHead.descriptions.Add("A function that receives two parameters, A and B.");
            GraphHead.descriptions.Add("A process that uses two variables, A and B.");
            GraphHead.descriptions.Add("A routine that consumes two values, A, B.");
            GraphHead.descriptions.Add("A procedure that gets two inputs: A, B." );
            GraphHead.descriptions.Add("A function that gets 2 params, A and B.");
            GraphHead.descriptions.Add("Get 2 params, A and B.");
            GraphHead.descriptions.Add("Get 2 parameters, A and B.");
            GraphHead.descriptions.Add("A process that receive two variables, A and B.");
            GraphHead.descriptions.Add("A routine that consumes two values, A, B.");
            GraphHead.descriptions.Add("A procedure that gets two inputs: A, B.");

            this.BuildConditionNodes();
            this.BuildReturnNodes();
            this.BuildArithmeticNode();
            this.BuildOperandsCode();
            this.BuildNumberVars();
            this.BuildPlacment();
            this.BuildCreateVar();
            this.BuildForNode();
            this.BuildWhileNode();

            GraphHead.CodeNodesInside1.Add(this.ReturnNode);
            GraphHead.CodeNodesInside1.Add(this.ConditionNode);
            GraphHead.CodeNodesInside1.Add(this.ForNode);
            GraphHead.CodeNodesInside1.Add(this.WhileNode);
            GraphHead.CodeNodesInside1.Add(this.PlacementNode);
            GraphHead.CodeNodesInside1.Add(this.ArithmeticUnary);
        }

        private void BuildWhileNode()
        {
            this.WhileNode.CodeStart = "while (";
            this.WhileNode.CodeEnd1 = "){";
            this.WhileNode.CodeEnd2 = "}";
            this.WhileNode.descriptions.Add("Perform loop as long as <cond> is fulfilled and do <inside>");
            this.WhileNode.descriptions.Add("Keep loop going while <conf> exists and perform <inside>");
            this.WhileNode.descriptions.Add("While <cond> is true do in loop <inside>");
            this.WhileNode.descriptions.Add("While <cond> do <inside>");

            // After 
            this.WhileNode.CodeNodesAfter.Add(this.ReturnNode);
            this.WhileNode.CodeNodesAfter.Add(this.ConditionNode);
            this.WhileNode.CodeNodesAfter.Add(this.ForNode);
            this.WhileNode.CodeNodesAfter.Add(this.WhileNode);
            this.WhileNode.CodeNodesAfter.Add(this.PlacementNode);
            this.WhileNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.WhileNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 1
            this.WhileNode.CodeNodesInside1.Add(this.OperandsNode);

            // Inside 2 
            this.WhileNode.CodeNodesInside2.Add(this.ReturnNode);
            this.WhileNode.CodeNodesInside2.Add(this.ConditionNode);
            this.WhileNode.CodeNodesInside2.Add(ForNode);
            this.WhileNode.CodeNodesInside2.Add(WhileNode);
            this.WhileNode.CodeNodesInside2.Add(this.PlacementNode);
            this.WhileNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            this.WhileNode.CodeNodesInside2.Add(this.CreateVar);
        }

        private void BuildForNode()
        {
            CodeNode ForArithNode = new CodeNode();
            CodeNode ForPlacment = new CodeNode();
            CodeNode ForArirhUnary = new CodeNode();


            ForArithNode.CodeNodesInside1 = this.ArithmeticNode.CodeNodesInside1;

            ForPlacment.CodeEnd1 = " = ";
            ForPlacment.CodeNodesInside1.Add(this.Variable);
            ForPlacment.CodeNodesInside2.Add(this.Variable);
            ForPlacment.CodeNodesInside2.Add(this.Number);
            ForPlacment.CodeNodesInside2.Add(this.Boolean);
            ForPlacment.CodeNodesInside2.Add(this.OperandsNode);
            ForPlacment.CodeNodesInside2.Add(this.ArithmeticNode);

            CodeNode UnaryPlus = new CodeNode();
            UnaryPlus.CodeStart = "++";
            UnaryPlus.descriptions.Add("Add 1 to variable <var>.");
            UnaryPlus.descriptions.Add("Increment variable A by 1 <var>.");
            UnaryPlus.descriptions.Add("Add to variable <var> 1 Increment.");
            UnaryPlus.descriptions.Add("Up the value of variable <var> by 1.");
            UnaryPlus.descriptions.Add("Raise variable <var>'s value by 1.");

            CodeNode UnaryMinus = new CodeNode();
            UnaryMinus.CodeStart = "--";
            UnaryMinus.descriptions.Add("Subtract 1 from variable <var>.");
            UnaryMinus.descriptions.Add("Decrement variable <var> by 1.");
            UnaryMinus.descriptions.Add("Reduce variable <var> by 1.");
            UnaryMinus.descriptions.Add("Reduce the value of variable <var> by 1.");
            UnaryMinus.descriptions.Add("Lower variable <var>'s value by 1.");

            ForArirhUnary.CodeNodesInside1.Add(this.Variable);
            ForArirhUnary.CodeNodesInside2.Add(UnaryPlus);
            ForArirhUnary.CodeNodesInside2.Add(UnaryMinus);

            CodeNode ForInside = new ForInsideNode();
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
            this.ForNode.descriptions.Add("Run the for loop and <palcement>, as long as the condition <condition> exists and when done, do <arithmetic>.");
            this.ForNode.descriptions.Add("Use the for loop and <palcement>, if the condition <condition> persists. Then go on to <arithmetic>.");
            this.ForNode.descriptions.Add("Run the for loop with <palcement> in use, if the condition <condition> is relevant. At the end of every iteration do <arithmetic>.");


            // Inside 2
         this.ForNode.CodeNodesInside2.Add(this.ReturnNode);
            this.ForNode.CodeNodesInside2.Add(this.ConditionNode);
            this.ForNode.CodeNodesInside2.Add(this.ForNode);
            this.ForNode.CodeNodesInside2.Add(this.WhileNode);
            this.ForNode.CodeNodesInside2.Add(this.PlacementNode);
            this.ForNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            this.ForNode.CodeNodesInside2.Add(this.CreateVar);

            // After
            this.ForNode.CodeNodesAfter.Add(this.ReturnNode);
            this.ForNode.CodeNodesAfter.Add(this.ConditionNode);
            this.ForNode.CodeNodesAfter.Add(this.ForNode);
            this.ForNode.CodeNodesAfter.Add(this.WhileNode);
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
            this.CreateVar.CodeNodesAfter.Add(this.WhileNode);
            this.CreateVar.CodeNodesAfter.Add(this.PlacementNode);
            this.CreateVar.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.CreateVar.CodeNodesAfter.Add(this.CreateVar);
            this.CreateVar.descriptions.Add("Declare variable <var>");
            this.CreateVar.descriptions.Add("Variable <var> declaration");
            


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
            this.PlacementNode.descriptions.Add("Let variable <var> contain <expr>.");
            this.PlacementNode.descriptions.Add("Place <expr> in variable <var>.");
            this.PlacementNode.descriptions.Add("Set variable <var> to <expr>.");
            this.PlacementNode.descriptions.Add("Let variable <var> be <expr>.");
            this.PlacementNode.descriptions.Add("<var> = <expr>.");

            // After 
            this.PlacementNode.CodeNodesAfter.Add(this.ReturnNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ConditionNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ForNode);
            this.PlacementNode.CodeNodesAfter.Add(this.WhileNode);
            this.PlacementNode.CodeNodesAfter.Add(this.PlacementNode);
            this.PlacementNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.PlacementNode.CodeNodesAfter.Add(this.CreateVar);
        }

        private void BuildNumberVars()
        {
            CodeNode Number1 = new CodeNode();
            Number1.CodeStart = "1";
            Number1.descriptions.Add("1");
            Number1.descriptions.Add("One");

           CodeNode Number2 = new CodeNode();
            Number2.CodeStart = "2";
            Number2.descriptions.Add("2");
            Number2.descriptions.Add("Two");

           CodeNode Number3 = new CodeNode();
            Number3.CodeStart = "3";
            Number3.descriptions.Add("3");
            Number3.descriptions.Add("three");

            CodeNode Number4 = new CodeNode();
            Number4.CodeStart = "4";
            Number4.descriptions.Add("4");
            Number4.descriptions.Add("Four");

            CodeNode Number5 = new CodeNode();
            Number5.CodeStart = "5";
            Number5.descriptions.Add("5");
            Number5.descriptions.Add("Five");

            CodeNode Number6 = new CodeNode();
            Number6.CodeStart = "6";
            Number6.descriptions.Add("6");
            Number6.descriptions.Add("Six");

            CodeNode Number7 = new CodeNode();
            Number7.CodeStart = "7";
            Number7.descriptions.Add("7");
            Number7.descriptions.Add("Seven");

            CodeNode Number8 = new CodeNode();
            Number8.CodeStart = "8";
            Number8.descriptions.Add("8");
            Number8.descriptions.Add("Eight");

            CodeNode Number9 = new CodeNode();
            Number9.CodeStart = "9";
            Number9.descriptions.Add("9");
            Number9.descriptions.Add("Nine");

            CodeNode Number10 = new CodeNode();
            Number10.CodeStart = "10";
            Number10.descriptions.Add("10");
            Number10.descriptions.Add("Ten");

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
            True.descriptions.Add("True");
            CodeNode False = new CodeNode();
            False.CodeStart = "false";
            False.descriptions.Add("False");

            this.Boolean.CodeNodesInside1.Add(True);
            this.Boolean.CodeNodesInside1.Add(False);
            
            CodeNode A = new CodeNode();
            A.CodeStart = "A";
            A.descriptions.Add("A");
            CodeNode B = new CodeNode();
            B.CodeStart = "B";
            B.descriptions.Add("B");

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
            OrNode.descriptions.Add("<cond1> or <con2> ");
            OrNode.descriptions.Add("One or both <con1>, <cond2>");
            OrNode.descriptions.Add("Either or both conditions <cond1>, <cond2>");

            CodeNode AndNode = new CodeNode();
            AndNode.CodeEnd1 = " && ";
            AndNode.CodeNodesInside1.Add(this.Variable);
            AndNode.CodeNodesInside2.Add(this.Variable);
            AndNode.CodeNodesInside1.Add(this.OperandsNode);
            AndNode.CodeNodesInside2.Add(this.OperandsNode);
            AndNode.descriptions.Add("<cond1> and <cond2>");
            AndNode.descriptions.Add("Both <cond1>, <cond2>");
    

            CodeNode EqualNode = new CodeNode();
            EqualNode.CodeEnd1 = " == ";
            EqualNode.descriptions.Add("<arg1> is equivalent to <arg2>");
            EqualNode.descriptions.Add("<arg1> = <arg2>");
            EqualNode.descriptions.Add("<arg1> and <arg2> are equivalent.");
            EqualNode.descriptions.Add("There is equivalency between <arg1> and <arg2>.");
            EqualNode.descriptions.Add("<arg1> has equivalent worth to <arg2>.");

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
            NotEqualNode.descriptions.Add("<arg1> is not equivalent to <arg2>.");
            NotEqualNode.descriptions.Add("<arg1> and <arg2> are not equivalent.");
            NotEqualNode.descriptions.Add("There is no equivalency between <arg1> and <arg2>.");
            NotEqualNode.descriptions.Add("<arg1> has no equivalent worth to <arg2>.");
            NotEqualNode.descriptions.Add("<arg1> does not have equivalent worth to <arg2>.");
            NotEqualNode.descriptions.Add("<arg1> is not equal to <arg2>.");
            NotEqualNode.descriptions.Add("<arg1> not like <arg2>.");
           
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
            GreatNode.descriptions.Add("<arg1> is more than <arg2>.");
            GreatNode.descriptions.Add("<arg1> is higher than <arg2>.");
            GreatNode.descriptions.Add("<arg1> has more value than <arg2>.");
            GreatNode.descriptions.Add("<arg1> is worth more than <arg2>.");
            GreatNode.descriptions.Add("<arg1> is greater than <arg2>.");

            GreatNode.CodeNodesInside1.Add(this.Variable);
            GreatNode.CodeNodesInside2.Add(this.Variable);
            GreatNode.CodeNodesInside1.Add(this.OperandsNode);
            GreatNode.CodeNodesInside2.Add(this.OperandsNode);
            GreatNode.CodeNodesInside1.Add(this.ArithmeticNode);
            GreatNode.CodeNodesInside2.Add(this.ArithmeticNode);
            GreatNode.CodeNodesInside2.Add(this.Number);

            CodeNode LitleNode = new CodeNode();
            LitleNode.CodeEnd1 = " < ";
            LitleNode.descriptions.Add("<arg1> is less than <arg2>.");
            LitleNode.descriptions.Add("<arg1> is lower than <arg2>.");
            LitleNode.descriptions.Add("<arg1> has less value than <arg2>");
            LitleNode.descriptions.Add("<arg1> is worth less than <arg2>.");
            LitleNode.descriptions.Add("<arg1> is worth less than <arg2>.");            

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
            UnaryPlus.descriptions.Add("Add 1 to variable <var>.");
            UnaryPlus.descriptions.Add("Increment variable A by 1 <var>.");
            UnaryPlus.descriptions.Add("Add to variable <var> 1 Increment.");
            UnaryPlus.descriptions.Add("Up the value of variable <var> by 1.");
            UnaryPlus.descriptions.Add("Raise variable <var>'s value by 1.");

            CodeNode UnaryMinus = new CodeNode();
            UnaryMinus.CodeStart = "--";
            UnaryMinus.descriptions.Add("Subtract 1 from variable <var>.");
            UnaryMinus.descriptions.Add("Decrement variable <var> by 1.");
            UnaryMinus.descriptions.Add("Reduce variable <var> by 1.");
            UnaryMinus.descriptions.Add("Reduce the value of variable <var> by 1.");
            UnaryMinus.descriptions.Add("Lower variable <var>'s value by 1.");

            this.ArithmeticUnary.CodeNodesInside1.Add(this.Variable);
            this.ArithmeticUnary.CodeNodesInside2.Add(UnaryPlus);
            this.ArithmeticUnary.CodeNodesInside2.Add(UnaryMinus);
            this.ArithmeticUnary.CodeEnd2 = ";";

            // After 
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ReturnNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ConditionNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ForNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.WhileNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.PlacementNode);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.ArithmeticUnary);
            this.ArithmeticUnary.CodeNodesAfter.Add(this.CreateVar);

            this.ArithmeticNode.CodeStart = "(";
            this.ArithmeticNode.isHaveThenAfter = true;
            this.ArithmeticNode.CodeEnd1 = ")";

            CodeNode AddNode = new CodeNode();
            AddNode.CodeEnd1 = " + ";
            AddNode.descriptions.Add("<arg1> plus <arg2>");
            AddNode.descriptions.Add("<arg1> together with <arg2>");
            AddNode.descriptions.Add("<arg1> in addition to <arg2>");
            AddNode.descriptions.Add("<arg1> + <arg2>");
            AddNode.CodeNodesInside1.Add(this.Number);
            AddNode.CodeNodesInside2.Add(this.Number);
            AddNode.CodeNodesInside1.Add(this.ArithmeticNode);
            AddNode.CodeNodesInside2.Add(this.ArithmeticNode);
            AddNode.CodeNodesInside1.Add(this.Variable);
            AddNode.CodeNodesInside2.Add(this.Variable);

            CodeNode SubNode = new CodeNode();
            SubNode.CodeEnd1 = " - ";
            SubNode.descriptions.Add("<arg1> minus <arg2>");
            SubNode.descriptions.Add("<arg1> - <arg2>");
            SubNode.descriptions.Add("<arg1> less <arg2>");
            SubNode.descriptions.Add("<arg2> subtracted from <arg1>");
            SubNode.descriptions.Add("Subtract <arg2> from variable <arg1>");

            SubNode.CodeNodesInside1.Add(this.Number);
            SubNode.CodeNodesInside2.Add(this.Number);
            SubNode.CodeNodesInside1.Add(this.ArithmeticNode);
            SubNode.CodeNodesInside2.Add(this.ArithmeticNode);
            SubNode.CodeNodesInside1.Add(this.Variable);
            SubNode.CodeNodesInside2.Add(this.Variable);




            CodeNode DivNode = new CodeNode();
            DivNode.CodeEnd1 = " / ";
            DivNode.descriptions.Add("<arg1> divided by <arg2>");
            DivNode.descriptions.Add("<arg1> / <arg2>");
            DivNode.descriptions.Add("<arg1> \\ <arg2>");
            DivNode.descriptions.Add("<arg2> divided into <arg1>");
            DivNode.CodeNodesInside1.Add(this.Number);
            DivNode.CodeNodesInside2.Add(this.Number);
            DivNode.CodeNodesInside1.Add(this.ArithmeticNode);
            DivNode.CodeNodesInside2.Add(this.ArithmeticNode);
            DivNode.CodeNodesInside1.Add(this.Variable);
            DivNode.CodeNodesInside2.Add(this.Variable);
         

            CodeNode MulNode = new CodeNode();
            MulNode.CodeEnd1 = " * ";
            MulNode.descriptions.Add("<arg1> multiplied by <arg2>");
            MulNode.descriptions.Add("<arg1> times <arg2>");
            MulNode.descriptions.Add("<arg1> * <arg2>");
            MulNode.descriptions.Add("Multiply <arg1> by <arg2>");
            MulNode.descriptions.Add("Mul <arg1> by <arg2>");
            
            MulNode.CodeNodesInside1.Add(this.Number);
            MulNode.CodeNodesInside2.Add(this.Number);
            MulNode.CodeNodesInside1.Add(this.ArithmeticNode);
            MulNode.CodeNodesInside2.Add(this.ArithmeticNode);
            MulNode.CodeNodesInside1.Add(this.Variable);
            MulNode.CodeNodesInside2.Add(this.Variable);

            CodeNode PowNode = new CodeNode();
            PowNode.CodeEnd1 = " ^ ";
            PowNode.descriptions.Add("<arg1> to the power of <arg2>");
            PowNode.descriptions.Add("<arg1> ^ <arg2>");
            PowNode.descriptions.Add("Multiply <arg1> by the power of <arg2>");

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
            this.ConditionNode.descriptions.Add(" Check if <cond> is in effect, then do <inside>");
            this.ConditionNode.descriptions.Add("If <cond> is active, then implement <inside>");
            this.ConditionNode.descriptions.Add("Do <inside> if <cond> is present.");
            this.ConditionNode.descriptions.Add("In the case of <cond>, do <inside>");
            this.ConditionNode.descriptions.Add("If <cond> is valid, do <inside>");
        








      CodeNode ElseIfNode = new CodeNode();
            ElseIfNode.CodeStart = "else if (";
            ElseIfNode.CodeEnd1 = "){";
            ElseIfNode.CodeEnd2 = "}";
            ElseIfNode.descriptions.Add("However if <cond> exists, do <inside>");
            ElseIfNode.descriptions.Add("But when <cond> prevails, then do <inside>");
            ElseIfNode.descriptions.Add("For a dissimilar <cond>, implement <inside>");
            ElseIfNode.descriptions.Add("Else if <cond>, implement <inside>");
            ElseIfNode.descriptions.Add("Else if <cond>, Do <inside>");
 

           CodeNode ElseNode = new CodeNode();
            ElseNode.CodeStart = "else{";
            ElseNode.CodeEnd1 = "}";
            ElseNode.descriptions.Add("In any other case, do <inside>");
            ElseNode.descriptions.Add("Else, carry out <inside>");
            ElseNode.descriptions.Add("Or else, implement <inside>");
            ElseNode.descriptions.Add("For a different case, then do <inside>");
            ElseNode.descriptions.Add("Else, <inside>");

            // After 
           this.ConditionNode.CodeNodesAfter.Add(ElseNode);
           this.ConditionNode.CodeNodesAfter.Add(ElseIfNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ReturnNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ConditionNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ForNode);
           this.ConditionNode.CodeNodesAfter.Add(this.WhileNode);
           this.ConditionNode.CodeNodesAfter.Add(this.PlacementNode);
           this.ConditionNode.CodeNodesAfter.Add(this.ArithmeticUnary);
           this.ConditionNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 1
            this.ConditionNode.CodeNodesInside1.Add(this.OperandsNode);

            // Inside 2 
            this.ConditionNode.CodeNodesInside2.Add(this.ReturnNode);
            this.ConditionNode.CodeNodesInside2.Add(this.ConditionNode);
            this.ConditionNode.CodeNodesInside2.Add(ForNode);
            this.ConditionNode.CodeNodesInside2.Add(WhileNode);
            this.ConditionNode.CodeNodesInside2.Add(this.PlacementNode);
            this.ConditionNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            this.ConditionNode.CodeNodesInside2.Add(this.CreateVar);

            // After 
            ElseIfNode.CodeNodesAfter.Add(ElseIfNode);
            ElseIfNode.CodeNodesAfter.Add(ElseNode);
            ElseIfNode.CodeNodesAfter.Add(this.ReturnNode);
            ElseIfNode.CodeNodesAfter.Add(this.ConditionNode);
            ElseIfNode.CodeNodesAfter.Add(this.ForNode);
            ElseIfNode.CodeNodesAfter.Add(this.WhileNode);
            ElseIfNode.CodeNodesAfter.Add(this.PlacementNode);
            ElseIfNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            ElseIfNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 1
            ElseIfNode.CodeNodesInside1.Add(this.OperandsNode);
            
            // Inside 2 
            ElseIfNode.CodeNodesInside2.Add(this.ReturnNode);
            ElseIfNode.CodeNodesInside2.Add(this.ConditionNode);
            ElseIfNode.CodeNodesInside2.Add(this.ForNode);
            ElseIfNode.CodeNodesInside2.Add(this.WhileNode);
            ElseIfNode.CodeNodesInside2.Add(this.PlacementNode);
            ElseIfNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            ElseIfNode.CodeNodesInside2.Add(this.CreateVar);

            // After 
            ElseNode.CodeNodesAfter.Add(this.ReturnNode);
            ElseNode.CodeNodesAfter.Add(this.ConditionNode);
            ElseNode.CodeNodesAfter.Add(this.ForNode);
            ElseNode.CodeNodesAfter.Add(this.WhileNode);
            ElseNode.CodeNodesAfter.Add(this.PlacementNode);
            ElseNode.CodeNodesAfter.Add(this.ArithmeticUnary);
            ElseNode.CodeNodesAfter.Add(this.CreateVar);

            // Inside 2 
            ElseNode.CodeNodesInside1.Add(this.ReturnNode);
            ElseNode.CodeNodesInside1.Add(this.ConditionNode);
            ElseNode.CodeNodesInside2.Add(this.ForNode);
            ElseNode.CodeNodesInside2.Add(this.WhileNode);
            ElseNode.CodeNodesInside2.Add(this.PlacementNode);
            ElseNode.CodeNodesInside2.Add(this.ArithmeticUnary);
            ElseNode.CodeNodesInside2.Add(this.CreateVar);
        }

        public void BuildReturnNodes()
        {
            ReturnNode.CodeStart = "return (";
            ReturnNode.isHaveThenAfter = false;
            ReturnNode.CodeEnd1 = ");";
            ReturnNode.descriptions.Add("Return <ret>");
            ReturnNode.descriptions.Add("Put back <ret>");
            ReturnNode.descriptions.Add("Exit with <ret>");

            this.ReturnNode.CodeNodesInside1.Add(this.Number);
            this.ReturnNode.CodeNodesInside1.Add(this.OperandsNode);
            this.ReturnNode.CodeNodesInside1.Add(this.ArithmeticNode);
            this.ReturnNode.CodeNodesInside1.Add(this.Variable);
            this.ReturnNode.CodeNodesInside1.Add(this.Boolean);

        }

        public FuncCodeAndDesc PrintRandomFunction(int commandNum, int conditionNum )
        {
            this.BuildCodeGraph();
            
            return this.GraphHead.getFuncStringRand(commandNum, commandNum, this.Variable);
            
        }
    }
}