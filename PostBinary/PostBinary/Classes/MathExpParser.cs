using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PH.DataTree;
using System.Text.RegularExpressions;

namespace PostBinary.Classes
{
    
    class nodeValue
    {
        public nodeValue() { }

        public nodeValue(int ind, int pos, int prior, String opSym)
        {
            this.ind = ind;
            this.pos = pos;
            this.priority = prior;
            this.operatorSym = opSym;
            this.operandLeft = "";
            this.operandRight = "";
            this.nested = 0;
        }

        // index of operator, just increment
        public int ind;

        // position in equation
        public int pos;

        // How much operator are nested
        public int nested;

        // priority of operator, the higher number the more prioritezed operator
        public int priority;
        public String operatorSym;
        public String operandLeft;

        public String operandRight;
        public String toString()
        {
            return "[ " + this.operatorSym + " ] Ps[" + this.pos + "] Pr[ " + this.priority + " ] N[ " + this.nested + " ] L{" + operandLeft + "}" + this.operatorSym + " R{" + operandRight + "}";
        }

    };

    class MathExpParser
    {
        int BEGIN = 0;
        string _inputEquation = "";
        Stack<nodeValue> _operatorStack;
        Stack<nodeValue> _operatorsStackCopy;
        Stack<nodeValue> _tempOperStack;
        nodeValue _curStackValue;
        Stack<String> _variable;
        public MathExpParser()
        {
            _operatorStack = countOperators(_inputEquation);
            _operatorsStackCopy = new Stack<nodeValue>(_operatorStack);
            _operatorStack = new Stack<nodeValue>(_operatorStack);
            _variable = new Stack<string>();
        }
        public MathExpParser(String equation)
        {
            _inputEquation = equation;
            _operatorStack = countOperators(_inputEquation);
            _operatorsStackCopy = new Stack<nodeValue>(_operatorStack);
            _operatorStack = new Stack<nodeValue>(_operatorStack);
            _variable = new Stack<string>();
        }

        public Stack<String> getVars()
        {
            Stack<String> tempStack = new Stack<string>();
            MatchCollection mc = Regex.Matches(_inputEquation, @"[a-zA-z]");
            if (mc.Count > 0)
            {
                foreach (Match match in mc)
	            {
	                foreach (Capture capture in match.Captures)
	                {
                        tempStack.Push(capture.Value);
	                }
                }
                return tempStack;
            }  
            else
                return null;

        }
        public int compile(String equation)
        {
            _inputEquation = equation;
            Console.WriteLine("Analizing... \n\r \tEquation=[" + _inputEquation + "]");

            _curStackValue = new nodeValue();

            // Creating root 
            DTreeNode<String> root = new DTreeNode<String>();

            //DTreeBuilder<nodeValue> trB = new DTreeBuilder<nodeValue>();
            // Initing root
            root.Value = "Root";//_operatorStack.Peek();

            // Position to add next Node
            DTreeNode<String> curNode = null;

            // Position where were added last Node
            DTreeNode<String> lastNode = null;

            // Temporary data storage
            DTreeNode<String> tempNode = null;
            // Temporary data storage
            DTreeNode<String>[] tempNodeArray = null;
            // DEBUG Operators position and index
            if (verifyAlpha())
            { 

            }
            while (_operatorStack.Count > 0)
            {
                _curStackValue.ind = _operatorStack.Peek().ind;
                _curStackValue.pos = _operatorStack.Peek().pos;
                _curStackValue.priority = _operatorStack.Pop().priority;

                Console.WriteLine("Ind= " + _curStackValue.ind + " Pos= " + _curStackValue.pos + " Prior= " + _curStackValue.priority);
            }
            Console.WriteLine("_________________________________________");


            if (verifyParentheses())
            {
                if (verifyParenthesesExp())
                {
                    // Verify all brackets

                    nodeValue[] allOperators = _operatorsStackCopy.ToArray();

                    _tempOperStack = new Stack<nodeValue>();
                    // DEBUG Operators nesting
                    for (int i = 0; i < allOperators.Length; i++)
                    {
                        allOperators[i].nested = getPriorForOperator(allOperators[i].pos);
                        _tempOperStack.Push(allOperators[i]);
                        Console.WriteLine("Nesting= " + allOperators[i].nested + "  Op" + allOperators[i].operatorSym);
                    }
                    Console.WriteLine("_________________________________________");

                    int curPos, operBeginPos, operEndPos;
                    bool foundBegin;
                    char[] sym = { '-', '/', '*', '+', ')', '(' };
                    // Setting operators operands
                    for (int i = 0; i < allOperators.Length; i++)
                    {
                        foundBegin = false;
                        // Find left operand
                        if (_inputEquation[allOperators[i].pos - 1] != ')')
                        {
                            operBeginPos = operEndPos = allOperators[i].pos - 1;
                            while (operBeginPos > 0 && !foundBegin)
                            {
                                if (_inputEquation.IndexOfAny(sym, operBeginPos - 1, 1) != -1)
                                    foundBegin = true;
                                else
                                    operBeginPos--;
                            }
                            allOperators[i].operandLeft = _inputEquation.Substring(operBeginPos, operEndPos - operBeginPos + 1);
                        }
                        else
                        {
                            // Find nested 
                        }

                        foundBegin = false;
                        // Find right operand
                        if (_inputEquation[allOperators[i].pos + 1] != '(')
                        {
                            operBeginPos = operEndPos = allOperators[i].pos + 1;
                            while (operEndPos < _inputEquation.Length - 1 && !foundBegin)
                            {

                                if (_inputEquation.IndexOfAny(sym, operEndPos + 1, 1) != -1)
                                    foundBegin = true;
                                else
                                    operEndPos++;
                            }
                            allOperators[i].operandRight = _inputEquation.Substring(operBeginPos, operEndPos - operBeginPos + 1);
                        }
                    }


                    /*
                // DEBUG Tree
                MainForm testForm = new MainForm();
                TreeView testTV = new TreeView();

                testForm.Controls.Add(testTV);
                testForm.Controls[0].Width = 500;
                testForm.Controls[0].Height = 1500;

                testTV.Width = 500;
                testTV.Height = 1500;
                bool first = false;
                int lastNested = 0;
                int lastPrior = 0;
                int curNested = 0;
                int curPrior = 0;
                string tempString;

                TreeViewController<String> treeViewController = new TreeViewController<String>((TreeView)testForm.Controls[0], root);
                
                while (_tempOperStack.Count > 0)
                {
                    curStackValue = _tempOperStack.Pop();
                    if (!first)
                    {
                        // Lowest priority, means last in evaluation
                        if (curNode != null)
                            lastNode = curNode.Nodes.Add(curStackValue.toString());
                        else
                            curNode = lastNode = root.Nodes.Add(curStackValue.toString());                   
                        first = true;
                    }
                    else
                    {
                        // Lowest priority, means last in evaluation
                        // The same nesting, adding to to this leave
                        if (lastNested == curStackValue.nested)
                        {
                            if (lastPrior <= curStackValue.priority)
                            {
                                lastNode = curNode.Nodes.Add(curStackValue.toString());
                                //CopyNodeToNode(lastNode, curNode);
                                //lastNode.Remove();
                                //lastNode = curNode;
                            }
                            else
                            {
                               //curNode = root.Nodes.InsertBefore(lastNode, curStackValue.toString());
                               
                               //CopyNodeToNode(curNode, lastNode);
                               lastNode = curNode.Nodes.Add(curStackValue.toString());//tempString
                            }
                        }
                        else
                        {
                            if (lastNested > curStackValue.nested)
                            {
                                if (curNode.Nodes.Count < 2)
                                {
                                    curNode = curNode.Nodes.Add(curStackValue.toString());
                                    curNested = curStackValue.nested;
                                    curPrior = curStackValue.priority;

                                    tempString = lastNode.Value;
                                    lastNode.Remove();
                                    curNode.Nodes.Add(tempString);
                                }
                                else
                                {
                                    if (curNode.Parent.Parent == null)
                                    {
                                        lastNode = root.Nodes.InsertBefore(curNode, curStackValue.toString());
                                        CopyNodeToNode(curNode, lastNode);
                                        
                                        curNode.Remove();                                        
                                        curNode = lastNode;
                                    }
                                    else
                                    {
                                        lastNode = curNode.Parent.Nodes.Add(curStackValue.toString());
                                        curNode = curNode.Parent;
                                    }
                                }
                            }
                            else
                            {
                                lastNode = curNode.Nodes.Add(curStackValue.toString());
                            }  
                        }
                    }
                    lastNested = curStackValue.nested;
                    lastPrior = curStackValue.priority;
                }
                testForm.ShowDialog();                
                */
                }
                else
                {
                    Console.WriteLine("validation Error! Operator Missing!");
                    return -1;
                }

            }
            else
            {
                Console.WriteLine("validation Error! Parentheses Missing!");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// Copies Src Node to Dest Node, and copies all nodes also.
        /// </summary>
        /// <param name="src">Source to copy from.</param>
        /// <param name="dest">Destenation node to copy to.</param>
        public void CopyNodeToNode(DTreeNode<String> src, DTreeNode<String> dest)
        {
            DTreeNode<String> retNode = new DTreeNode<string>(src.Value);
            for (int i = 0; i < src.Nodes.Count; i++)
            {
                CopyNodeToNode(src.Nodes[i], retNode);
            }
            dest.Nodes.Add(retNode);
        }

        #region Usefull shit

        /// <summary>
        /// Finds any symbols from alphabet
        /// </summary>
        /// <returns>True if symbol found, else False.</returns>
        public bool verifyAlpha()
        {
            MatchCollection mc = Regex.Matches(_inputEquation, @"[a-zA-z]");
            if (mc.Count > 0)
                return true;
            else
                return false;
        }

        public bool verifyParenthesesExp()
        {
            MatchCollection mc = Regex.Matches(_inputEquation, @"\(([^)\*\+\/-]+?)\)|\(([^)\*\+\/-]?)\)");

            if (mc.Count > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Verifies number of left-side brakets and right-side brackets 
        /// </summary>
        /// <returns>True if number of lparen and rparen are equal, otherwise false</returns>
        public bool verifyParentheses()
        {
            char[] sym = { '(', ')' };
            int n = 0, countLeft = 0, countRight = 0;
            while ((n = _inputEquation.IndexOf(sym[0], n)) != -1)
            {
                n++;
                countLeft++;
            }

            n = 0;
            while ((n = _inputEquation.IndexOf(sym[1], n)) != -1)
            {
                n++;
                countRight++;
            }

            if (countLeft == countRight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calculates nesting of operator based on parentheses it's wrapped in.
        /// </summary>
        /// <param name="operatorPosition">Position of operator you are counting parentheses for.</param>
        /// <returns>Nested number for current operator</returns>
        public int getPriorForOperator(int operatorPosition)
        {
            // Search to left of opertaor and count brackets
            // If all parentheses are present and number is equal, then we can count only on part
            char[] sym = { '(', ')' };
            int n = 0, count = 0;
            while ((n = _inputEquation.IndexOf(sym[0], n)) != -1 && (n < operatorPosition))
            {
                n++;
                count++;
            }

            n = 0;
            while ((n = _inputEquation.IndexOf(sym[1], n)) != -1 && (n < operatorPosition))
            {
                n++;
                count--;
            }

            return count;
        }

        public Stack<nodeValue> countOperators(string equation)
        {
            Stack<nodeValue> count = new Stack<nodeValue>();
            char[] sym = { '-', '/', '*', '+' };
            int n = 0, ind = 0;
            int prior;
            while ((n = equation.IndexOfAny(sym, n)) != -1)
            {
                prior = equation[n] == '/' || equation[n] == '*' ? 1 : 0;
                count.Push(new nodeValue(0, n, prior, equation[n].ToString()));
                n++;
                ind++;
            }

            return count;
        }
        #endregion

        #region Useless shit!
        /// <summary>
        /// Finds for every pair of parentheses presense of operator
        /// </summary>
        /// <returns>-1 - if error; Overwise - if all parentheses has operators</returns>
        public int verifyParenthesesOperator(int begin)
        {
            char[] sym = { '(', ')' };
            int n = begin, prev = -1, next = -1;
            //bool first = true;
            //while (verifyParentheses())
            while ((n = _inputEquation.IndexOfAny(sym, n)) != -1)
            {
                if (prev == -1)
                {
                    prev = n;
                    // First appearence should be '(' symbol
                    if (_inputEquation[prev] == ')')
                    {
                        return -1;
                    }
                }
                else
                {
                    if (_inputEquation[n] == '(')
                    {
                        next = verifyParenthesesOperator(n);
                    }
                    else
                    {
                        next = n;
                        //find operators
                        if (findOperator(prev, next) != 0)
                        {
                            return -1;
                        }
                        else
                        {
                            prev = -1;
                            next = -1;
                        }
                    }

                }

                n++;

            }
            return next;
        }

        public int findOperator(int start, int end)
        {
            char[] sym = { '(', ')', '+', '-', '/', '*' };
            int n = start, count = 0;
            bool first = false;
            //while (verifyParentheses())
            while (((n = _inputEquation.IndexOfAny(sym, n)) != -1) && (n < end))
            {
                if ((_inputEquation[n] == '(') && (!first))
                {
                    first = true;
                }
                else
                {
                    if (_inputEquation[n] == '(')
                    {
                        count++;
                    }

                    if (_inputEquation[n] == ')')
                    {
                        if (count == 0)
                        {
                            return -1; // Exception Operator Missing!
                        }
                    }

                    if ((_inputEquation[n] == '+') || (_inputEquation[n] == '-') || (_inputEquation[n] == '/') || (_inputEquation[n] == '*'))
                    {
                        if (count == 0)
                        {
                            return count;
                        }
                    }

                }

                n++;
            }
            return -1;
        }
        #endregion

    }

}
