using System;
using System.Collections.Generic;
using System.Linq;

namespace PostBinary.Classes.Utils.Parser 
{
    /// <summary>
    /// Saves data when stack willing
    /// </summary>
    public class StackData 
    {
        public String dataType;
        public double dataValue;
        public StackData(String _dataType, double _dataValue)
        {
            this.dataType = _dataType;
            this.dataValue = _dataValue;
        }
    }

    public class Parser
    {
        private readonly Queue<NumberClass> _outputQueue;
        private readonly Stack<string> _operatorStack;
        private readonly Dictionary<string, NumberClass> _variables;
        private readonly List<FunctionClass> _functions;
        private readonly Dictionary<string, Delegate> _customFunctions;
        private Stack _pbStack;
        public enum RoundingMethods
        {
            Round,
            RoundUp,
            RoundDown,
            Truncate
        }

        public Parser()
        {
            _outputQueue = new Queue<NumberClass>();
            _operatorStack = new Stack<string>();
            _variables = new Dictionary<string, NumberClass>();
            _functions = new List<FunctionClass>();
            _customFunctions = new Dictionary<string, Delegate>();
            _pbStack = new Stack();
        }

        public Stack GetStack()
        {
            return _pbStack;
        }

        public void UnregisterCustomFunction(string functionName)
        {
            try
            {
                _customFunctions.Remove(functionName);
            }
            catch (Exception)
            {
                throw new NoSuchFunctionException();
            }
        }

        public void UnregisterAllCustomFunctions()
        {
            _customFunctions.Clear();
        }

        public void RegisterCustomFunction(string functionName, Func<object, object> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomFunction(string functionName, Func<object, object, object> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomFunction(string functionName, Func<object, object, object, object> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomFunction(string functionName, Func<object, object, object, object, object> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomIntegerFunction(string functionName, Func<int, int> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomIntegerFunction(string functionName, Func<int, int, int> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomIntegerFunction(string functionName, Func<int, int, int, int> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomIntegerFunction(string functionName, Func<int, int, int, int, int> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomDoubleFunction(string functionName, Func<double, double> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomDoubleFunction(string functionName, Func<double, double, double> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomDoubleFunction(string functionName, Func<double, double, double, double> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RegisterCustomDoubleFunction(string functionName, Func<double, double, double, double, double> method)
        {
            _customFunctions.Add(functionName, method);
        }

        public void RemoveFunction(string functionName)
        {
            bool found = _functions.Any(f => f.Name.Equals(functionName));

            if (found)
            {
                int ndx = 0;

                while (ndx < _functions.Count)
                {
                    if (_functions[ndx].Name.Equals(functionName))
                        break;
                    ndx++;
                }
                _functions.RemoveAt(ndx);
                return;
            }

            throw new NoSuchFunctionException();//StringResources_Designer + ": " + functionName);
        }

        public void AddFunction(string functionName, FunctionArgumentList argList, string expression)
        {
            var fc = new FunctionClass { Arguments = argList, Expression = expression, Name = functionName };
            var numClass = new NumberClass { NumberType = NumberClass.NumberTypes.Expression, Expression = expression };

            EvaluateFunction(numClass, fc);

            _functions.Add(fc);
        }

        public void RemoveAllVariables()
        {
            _variables.Clear();
        }

        public void RemoveAllFunctions()
        {
            _functions.Clear();
        }

        public void Reset()
        {
            RemoveAllFunctions();
            RemoveAllVariables();
            UnregisterAllCustomFunctions();
        }

        public void RemoveVariable(string varName)
        {
            if (_variables.ContainsKey(varName))
            {
                _variables.Remove(varName);
                return;
            }

            throw new Exception();//StringResources.Undefined_Variable + ": " + varName);
        }

        private void AddVariable(string varName, NumberClass valueType)
        {
            if (_variables.ContainsKey(varName))
            {
                throw new VariableAlreadyDefinedException();//StringResources.Variable_already_defined + ": " + varName);
            }

            if (valueType.NumberType == NumberClass.NumberTypes.Expression)
            {
                SimplificationReturnValue eeVal;

                eeVal = EvaluateExpression(valueType);

                if (eeVal.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                {
                    AddVariable(varName, eeVal.DoubleValue);
                    return;
                }
                if (eeVal.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                {
                    AddVariable(varName, eeVal.IntValue);
                    return;
                }
            }
            _variables.Add(varName, valueType);
        }

        public void AddVariable(string varName, string value)
        {
            var nc = new NumberClass { NumberType = NumberClass.NumberTypes.Expression, Expression = value };

            AddVariable(varName, nc);
        }

        public void AddVariable(string varName, double value)
        {
            var nc = new NumberClass { NumberType = NumberClass.NumberTypes.Float, FloatNumber = value };

            AddVariable(varName, nc);
        }

        public void AddVariable(string varName, int value)
        {
            var nc = new NumberClass { NumberType = NumberClass.NumberTypes.Integer, IntNumber = value };

            AddVariable(varName, nc);
        }

        private void EvaluateFunction(NumberClass expression, FunctionClass fc)
        {
            EvaluateFunction(expression, fc, new List<NumberClass> {
                               new NumberClass {NumberType = NumberClass.NumberTypes.Integer, IntNumber = 0}});
        }

        private SimplificationReturnValue EvaluateFunction(NumberClass expression, FunctionClass fc, IEnumerable<NumberClass> ncList2)
        {
            var parser = new Parser();
            var ncList = ncList2.ToList();

            foreach (var cfr in _customFunctions)
                parser.AddCustomFunction(cfr.Key, cfr.Value);

            foreach (var v in _variables)
            {
                if (v.Value.NumberType == NumberClass.NumberTypes.Float)
                {
                    parser.AddVariable(v.Key, v.Value.FloatNumber);
                }
                if (v.Value.NumberType == NumberClass.NumberTypes.Integer)
                {
                    parser.AddVariable(v.Key, v.Value.IntNumber);
                }
                if (v.Value.NumberType == NumberClass.NumberTypes.Expression)
                {
                    parser.AddVariable(v.Key, v.Value.Expression);
                }
            }

            foreach (var f in _functions)
            {
                parser.AddFunction(f.Name, f.Arguments, f.Expression);
            }

            int ndx = 0;
            foreach (var a in fc.Arguments)
            {
                NumberClass nc = ndx >= ncList.Count() ? new NumberClass { NumberType = NumberClass.NumberTypes.Integer, IntNumber = 0 } : ncList.ElementAt(ndx);

                if (nc.NumberType == NumberClass.NumberTypes.Float)
                {
                    try
                    {
                        parser.AddVariable(a, nc.FloatNumber);
                    } catch
                    {}
                }
                if (nc.NumberType == NumberClass.NumberTypes.Integer)
                {
                    try
                    {
                        parser.AddVariable(a, nc.IntNumber);
                    }catch
                    {
                    }
                }
                if (nc.NumberType == NumberClass.NumberTypes.Expression)
                    parser.AddVariable(a, nc.Expression);
                ndx++;
            }
            
            return parser.Simplify(expression.Expression);
        }

        protected void AddCustomFunction(string s, Delegate d)
        {
            _customFunctions.Add(s, d);
        }

        private SimplificationReturnValue EvaluateExpression(NumberClass expression)
        {
            var parser = new Parser();

            foreach (var cfh in _customFunctions)
                parser.AddCustomFunction(cfh.Key,cfh.Value);

            foreach (var v in _variables)
            {
                if (v.Value.NumberType == NumberClass.NumberTypes.Float)
                {
                    parser.AddVariable(v.Key, v.Value.FloatNumber);
                }
                if (v.Value.NumberType == NumberClass.NumberTypes.Integer)
                {
                    parser.AddVariable(v.Key, v.Value.IntNumber);
                }
                if (v.Value.NumberType == NumberClass.NumberTypes.Expression)
                {
                    parser.AddVariable(v.Key, v.Value.Expression);
                }
            }

            foreach (var f in _functions)
            {
                parser.AddFunction(f.Name, f.Arguments, f.Expression);
            }
            return parser.Simplify(expression.Expression);
        }

        public object SimplifyObject(string equation)
        {
            var retval = Simplify(equation);

            if (retval.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                return retval.IntValue;

            return retval.DoubleValue;
        }

        public int SimplifyInt(string equation, RoundingMethods roundMethod)
        {
            SimplificationReturnValue retval = Simplify(equation);

            if (retval.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                return retval.IntValue;

            if (roundMethod == RoundingMethods.RoundDown)
                return (int)Math.Floor(retval.DoubleValue);
            if (roundMethod == RoundingMethods.RoundUp)
                return (int)Math.Ceiling(retval.DoubleValue);
            if (roundMethod == RoundingMethods.Round)
                return (int)Math.Round(retval.DoubleValue);

            return (int)Math.Truncate(retval.DoubleValue);

        }

        public int SimplifyInt(string equation)
        {
            return SimplifyInt(equation, RoundingMethods.Round);
        }

        public double SimplifyDouble(string equation)
        {
            SimplificationReturnValue retval = Simplify(equation);

            if (retval.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                return retval.DoubleValue;

            return double.Parse(retval.IntValue.ToString());
        }
        public bool validateExpression(string expression)
        {
            // Find empty brackets
            // Find brackets without operands and operations
            // Operator without one operands(left or right side)
            // Dublicated operations
            var tp = new TokenParser();
            tp.InputString = expression;

            Token token = tp.GetToken();
            while (token != null)
            {
                System.Console.WriteLine("Name= "+token.TokenName + " Value="+ token.TokenValue);
                token = tp.GetToken();
            }
            return false;
        }
        public SimplificationReturnValue Simplify(string equation)
        {
            var retval = new SimplificationReturnValue { OriginalEquation = equation };
            validateExpression(equation);
            if (equation.Trim().StartsWith("-") || equation.Trim().StartsWith("+"))
                equation = "0" + equation;
            equation = equation.Replace("(+", "(0+");
            equation = equation.Replace("(-", "(0-");
            equation = equation.Replace("( +", "( 0+");
            equation = equation.Replace("( -", "( 0-");
            equation = equation.Replace(",-", ",0-");
            equation = equation.Replace(", -", ", 0-");
            equation = equation.Replace(",+", ",0+");
            equation = equation.Replace(", +", ", 0+");

            var tp = new TokenParser();

            foreach (var cf in _customFunctions)
            {
                tp.RegisterCustomFunction(cf.Key);
            }

            tp.InputString = equation;
            
            Token token = tp.GetToken();

            _operatorStack.Clear();
            _outputQueue.Clear();

            #region Filling _outputQueue
            while (token != null)
            {
                #region Sqare Root Token
                if (token.TokenName == TokenParser.Tokens.Sqrt)
                {
                    string expression = token.TokenValue.Substring(4, token.TokenValue.Length - 4);

                    SimplificationReturnValue rv = EvaluateExpression(new NumberClass
                                                                          {
                                                                              Expression = expression,
                                                                              NumberType = NumberClass.NumberTypes.Expression
                                                                          });


                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Sqrt(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Sqrt(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Sinus Token
                if (token.TokenName == TokenParser.Tokens.Sin)
                {
                    string expression = token.TokenValue.Substring(3, token.TokenValue.Length - 3);

                    SimplificationReturnValue rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Sin(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Sin(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Log Token
                if (token.TokenName == TokenParser.Tokens.Log)
                {
                    string expression = token.TokenValue.Substring(3, token.TokenValue.Length - 3);

                    SimplificationReturnValue rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Log(rv.IntValue, 10).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Log(rv.DoubleValue, 10).ToString();
                            break;
                    }
                }
                #endregion

                #region Log Natural Token
                if (token.TokenName == TokenParser.Tokens.LogN)
                {
                    string expression = token.TokenValue.Substring(4, token.TokenValue.Length - 4);

                    SimplificationReturnValue rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Log(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Log(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Tangens Token
                if (token.TokenName == TokenParser.Tokens.Tan)
                {
                    string expression = token.TokenValue.Substring(3, token.TokenValue.Length - 3);

                    var rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Tan(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Tan(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Absulute Value Token
                if (token.TokenName == TokenParser.Tokens.Abs)
                {
                    string expression = token.TokenValue.Substring(3, token.TokenValue.Length - 3);

                    var rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Abs(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Abs(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Cosinus Token
                if (token.TokenName == TokenParser.Tokens.Cos)
                {
                    string expression = token.TokenValue.Substring(3, token.TokenValue.Length - 3);

                    var rv =
                        EvaluateExpression(new NumberClass
                        {
                            Expression = expression,
                            NumberType = NumberClass.NumberTypes.Expression
                        });

                    token.TokenName = TokenParser.Tokens.Float;

                    switch (rv.ReturnType)
                    {
                        case SimplificationReturnValue.ReturnTypes.Integer:
                            token.TokenValue = Math.Cos(rv.IntValue).ToString();
                            break;
                        case SimplificationReturnValue.ReturnTypes.Float:
                            token.TokenValue = Math.Cos(rv.DoubleValue).ToString();
                            break;
                    }
                }
                #endregion

                #region Unknown Token
                if ((int)token.TokenName >= 100)
                {
                    int ndx1 = token.TokenValue.IndexOf("(");
                    string fn = token.TokenValue.Substring(0, ndx1);
                    string origExpression = token.TokenValue.Substring(ndx1);
                    string[] expressions = origExpression.Replace(",", "),(").Split(',');
                    bool found = false;

                    if (_customFunctions.Any(c => c.Key.Equals(fn)))
                        found = true;

                    if (found)
                    {
                        foreach (var ff in _customFunctions)
                        {
                            if (ff.Key.Equals(fn))
                            {
                                var p = new Parser();
                                foreach (var cfr in _customFunctions)
                                    p.AddCustomFunction(cfr.Key, cfr.Value);
                                foreach (var vr in _variables)
                                    p.AddVariable(vr.Key, vr.Value);
                                foreach (var vf in _functions)
                                    p.AddFunction(vf.Name, vf.Arguments, vf.Expression);

                                var ex = expressions.Select(p.Simplify).ToArray();

                                object funcRetval = null;

                                if (ff.Value.Method.ReturnType == typeof(int))
                                {
                                    var intParams = new object[ex.Length];
                                    int ndx = 0;

                                    foreach (var pp in ex)
                                    {
                                        if (pp.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                                            intParams[ndx] = (int)pp.DoubleValue;
                                        if (pp.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                                            intParams[ndx] = pp.IntValue;
                                        ndx++;
                                    }
                                    funcRetval = ff.Value.DynamicInvoke(intParams);
                                }
                                if (ff.Value.Method.ReturnType == typeof(double))
                                {
                                    var floatParams = new object[ex.Length];
                                    int ndx = 0;

                                    foreach (var pp in ex)
                                    {
                                        if (pp.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                                            floatParams[ndx] = pp.DoubleValue;
                                        if (pp.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                                            floatParams[ndx] = pp.IntValue;
                                        ndx++;
                                    }
                                    funcRetval = ff.Value.DynamicInvoke(floatParams);
                                }
                                if (ff.Value.Method.ReturnType==typeof(object))
                                {
                                    funcRetval = ff.Value.DynamicInvoke(expressions.Select(p.Simplify).ToArray());
                                }

                                //object funcRetval = ff.Value.DynamicInvoke(expressions.Select(p.Simplify).ToArray());

                                if (funcRetval is double)
                                {
                                    token.TokenValue = ((double)funcRetval).ToString();
                                    token.TokenName = TokenParser.Tokens.Float;
                                }
                                if (funcRetval is int)
                                {
                                    token.TokenValue = ((int)funcRetval).ToString();
                                    token.TokenName = TokenParser.Tokens.Integer;
                                }
                                if (funcRetval is SimplificationReturnValue)
                                {
                                    var srv = (SimplificationReturnValue)funcRetval;
                                    if (srv.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                                    {
                                        token.TokenValue = srv.IntValue.ToString();
                                        token.TokenName = TokenParser.Tokens.Integer;
                                    }
                                    if (srv.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                                    {
                                        token.TokenValue = srv.DoubleValue.ToString();
                                        token.TokenName = TokenParser.Tokens.Float;
                                    }
                                }
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        throw new NoSuchFunctionException();//StringResources.No_such_function_defined + ": " + fn);
                    }
                }
                #endregion

                #region Function Token
                if (token.TokenName == TokenParser.Tokens.Function)
                {
                    int ndx1 = token.TokenValue.IndexOf("(");
                    string fn = token.TokenValue.Substring(0, ndx1).Remove(0, 4);
                    string origExpression = token.TokenValue.Substring(ndx1);
                    string[] expressions = origExpression.Replace(",", "),(").Split(',');
                    bool found = false;
                    FunctionClass fun = null;

                    if (_functions.Any(c => c.Name.Equals(fn)))
                    {
                        found = true;
                    }

                    if (found)
                    {
                        foreach (var ff in _functions)
                            if (ff.Name.Equals(fn))
                                fun = ff;
                    }

                    if (!found)
                    {
                        throw new NoSuchFunctionException();//StringResources.No_such_function_defined + ": " + fn);
                    }

                    var parser = new Parser();
                    foreach (var cfh in _customFunctions)
                        parser.AddCustomFunction(cfh.Key, cfh.Value);

                    foreach (var v in _variables)
                    {
                        if (v.Value.NumberType == NumberClass.NumberTypes.Float)
                        {
                            parser.AddVariable(v.Key, v.Value.FloatNumber);
                        }
                        if (v.Value.NumberType == NumberClass.NumberTypes.Integer)
                        {
                            parser.AddVariable(v.Key, v.Value.IntNumber);
                        }
                        if (v.Value.NumberType == NumberClass.NumberTypes.Expression)
                        {
                            parser.AddVariable(v.Key, v.Value.Expression);
                        }
                    }

                    foreach (var f in _functions)
                    {
                        parser.AddFunction(f.Name, f.Arguments, f.Expression);
                    }
                    var expressionList = new List<NumberClass>();

                    foreach (var expression in expressions)
                    {
                        SimplificationReturnValue simRetval = parser.Simplify(expression);

                        var numClass = new NumberClass();
                        if (simRetval.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                        {
                            numClass.FloatNumber = simRetval.DoubleValue;
                            numClass.NumberType = NumberClass.NumberTypes.Float;
                        }
                        if (simRetval.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                        {
                            numClass.IntNumber = simRetval.IntValue;
                            numClass.NumberType = NumberClass.NumberTypes.Integer;
                        }
                        expressionList.Add(numClass);
                    }

                    if (fun != null)
                    {
                        var numClass = new NumberClass { NumberType = NumberClass.NumberTypes.Expression, Expression = fun.Expression };

                        SimplificationReturnValue sretval = parser.EvaluateFunction(numClass, fun, expressionList);

                        if (sretval != null && sretval.ReturnType == SimplificationReturnValue.ReturnTypes.Integer)
                        {
                            token.TokenName = TokenParser.Tokens.Integer;
                            token.TokenValue = sretval.IntValue.ToString();
                        }
                        if (sretval != null && sretval.ReturnType == SimplificationReturnValue.ReturnTypes.Float)
                        {
                            token.TokenName = TokenParser.Tokens.Float;
                            token.TokenValue = sretval.DoubleValue.ToString();
                        }
                    }
                }
                #endregion

                #region Variable Token
                if (token.TokenName == TokenParser.Tokens.Variable)
                {
                    if (_variables.ContainsKey(token.TokenValue))
                    {
                        var z = _variables[token.TokenValue];

                        if (z.NumberType == NumberClass.NumberTypes.Float)
                        {
                            token.TokenName = TokenParser.Tokens.Float;
                            token.TokenValue = z.FloatNumber.ToString();
                        }
                        else if (z.NumberType == NumberClass.NumberTypes.Integer)
                        {
                            token.TokenName = TokenParser.Tokens.Integer;
                            token.TokenValue = z.IntNumber.ToString();
                        }
                    }
                    else
                    {
                        throw new NoSuchVariableException();//StringResources.Undefined_Variable + ": " + token.TokenValue);
                    }
                }
                #endregion

                #region White space or NewLine Token
                if (token.TokenName == TokenParser.Tokens.Whitespace || token.TokenName == TokenParser.Tokens.Newline)
                {
                    token = tp.GetToken();
                    continue;
                }
                #endregion

                #region Integer or Float token
                if (token.TokenName == TokenParser.Tokens.Integer || token.TokenName == TokenParser.Tokens.Float)
                {
                    var nc = new NumberClass();

                    switch (token.TokenName)
                    {
                        case TokenParser.Tokens.Float:
                            nc.NumberType = NumberClass.NumberTypes.Float;
                            nc.FloatNumber = double.Parse(token.TokenValue.Replace('.', ','));
                            break;
                        case TokenParser.Tokens.Integer:
                            nc.NumberType = NumberClass.NumberTypes.Integer;
                            nc.IntNumber = int.Parse(token.TokenValue);
                            break;
                    }
                    _outputQueue.Enqueue(nc);
                }
                #endregion

                #region IsOperator Token
                if (IsOperator(token.TokenName))
                {
                    if (_operatorStack.Count > 0)
                    {
                        while (_operatorStack.Count > 0)
                        {
                            var op = _operatorStack.Peek(); //o2    

                            if (op == "(" || op == ")")
                                break;
                            if ((GetPrecedence(token.TokenName) <= GetPrecedence(op) &&
                                 IsLeftAssociative(token.TokenValue)) ||
                                !IsLeftAssociative(token.TokenValue) &&
                                GetPrecedence(token.TokenName) < GetPrecedence(op))
                            {
                                op = _operatorStack.Pop();
                                var nc = new NumberClass { NumberType = NumberClass.NumberTypes.Operator, Operator = op };

                                _outputQueue.Enqueue(nc);
                            }
                            else break;
                        }
                    }
                    _operatorStack.Push(token.TokenValue);
                }
                #endregion

                #region Lparen Token
                if (token.TokenName == TokenParser.Tokens.Lparen)
                    _operatorStack.Push(token.TokenValue);
                #endregion

                #region Rparen Token
                if (token.TokenName == TokenParser.Tokens.Rparen)
                {
                    if (_operatorStack.Count > 0)
                    {
                        var op = _operatorStack.Pop();

                        while (op != "(")
                        {
                            var nc = new NumberClass { Operator = op, NumberType = NumberClass.NumberTypes.Operator };

                            _outputQueue.Enqueue(nc);

                            if (_operatorStack.Count > 0)
                                op = _operatorStack.Pop();
                            else
                            {
                                throw new MismatchedParenthesisException();
                            }
                        }
                    }
                    else
                    {
                        throw new MismatchedParenthesisException();
                    }
                }
                #endregion

                token = tp.GetToken();
            }

            #endregion

            while (_operatorStack.Count > 0)
            {
                var op = _operatorStack.Pop();

                if (op == "(" || op == ")")
                {
                    throw new MismatchedParenthesisException();
                }

                var nc = new NumberClass { NumberType = NumberClass.NumberTypes.Operator, Operator = op };

                _outputQueue.Enqueue(nc);
            }

            bool floatAnswer = true;// _outputQueue.Any(v => v.NumberType == NumberClass.NumberTypes.Float);
            var intStack = new Stack<int>();
            int lastMemoryCell = 0;
            /*
             * Clearing stack before use it;
             */
            try
            {
                _pbStack.clearStack();
                if (floatAnswer || _outputQueue.Any(v => v.Operator == "/"))
                {
                    //var dblStack = new Stack<double>();

                    // Type{integer,float,operator, memory},{value/mem index}
                    var newStack = new Stack<StackData>();
                    String leftOperand, rightOperand;
                    int leftOperandInt, rightOperandInt;
                    foreach (var nc in _outputQueue)
                    {
                        if (nc.NumberType == NumberClass.NumberTypes.Integer)
                        {
                            newStack.Push(new StackData("int", nc.IntNumber));
                            continue;
                        }

                        if (nc.NumberType == NumberClass.NumberTypes.Float) 
                        {                         
                            newStack.Push(new StackData("float", nc.FloatNumber));
                            continue;
                        }

                        if (nc.NumberType == NumberClass.NumberTypes.Operator)
                        {

                            // Stack filling
                            // if left operand aren't memory cell and right aren't memory cell
                            if ((newStack.Peek().dataType != "mem") && (newStack.ElementAt(1).dataType != "mem"))
                            {
                                rightOperand = newStack.Pop().dataValue.ToString();
                                leftOperand = newStack.Pop().dataValue.ToString();
                                
                                _pbStack.PushCommand(nc.Operator, leftOperand, rightOperand);
                            }
                            else
                            {
                                // if left operand stored in memory
                                if (newStack.Peek().dataType == "mem")
                                {
                                    if (newStack.ElementAt(1).dataType != "mem")
                                    {
                                        leftOperandInt = (int)newStack.Pop().dataValue;
                                        rightOperand = newStack.Pop().dataValue.ToString();

                                        // use push with memory index for left operand
                                        _pbStack.PushCommand(nc.Operator, leftOperandInt, rightOperand);
                                    }
                                    else
                                    {
                                        leftOperandInt = (int)newStack.Pop().dataValue;
                                        rightOperandInt = (int)newStack.Pop().dataValue;
                                        // use push with memory index for left operand
                                        // TODO: Implement Push Command for KOP MEM MEM
                                        _pbStack.PushCommand(nc.Operator, leftOperandInt, rightOperandInt);
                                    }
                                }
                                else
                                {
                                    /* If right operand stored in memory */
                                    // Get left operand value
                                    leftOperand = newStack.Pop().dataValue.ToString();

                                    // Get right operand memory index
                                    rightOperandInt = (int)newStack.Pop().dataValue;
                                    
                                    
                                    // Use push with memory index for right operand
                                    _pbStack.PushCommand(nc.Operator, leftOperand, rightOperandInt);
                                }
                            }
                            //double val = DoMath(nc.Operator, dblStack.Pop(), dblStack.Pop());

                            lastMemoryCell = _pbStack.PeekCommand().MemoryCellUsed;
                            //dblStack.Push(-lastMemoryCell - 1);
                            newStack.Push(new StackData("mem", lastMemoryCell));
                        }
                    }

                    if (newStack.Count == 0)
                    {
                        throw new CouldNotParseExpressionException();
                    }
                    retval.DoubleValue = newStack.Pop().dataValue;
                    retval.ReturnType = SimplificationReturnValue.ReturnTypes.Float;
                }
                else
                {

                    foreach (var nc in _outputQueue)
                    {
                        if (nc.NumberType == NumberClass.NumberTypes.Integer)
                            intStack.Push(nc.IntNumber);
                        if (nc.NumberType == NumberClass.NumberTypes.Float)
                            intStack.Push((int)nc.FloatNumber);
                        if (nc.NumberType == NumberClass.NumberTypes.Operator)
                        {
                            // Stack filling
                            if (intStack.Count == 2)
                                _pbStack.PushCommand(nc.Operator, intStack.Pop().ToString(), intStack.Pop().ToString());
                            else
                                if (intStack.Count == 1)
                                    _pbStack.PushCommand(nc.Operator, intStack.Pop().ToString(), lastMemoryCell);
                            // Delete this and add stack filling
                            //int val = DoMath(nc.Operator, intStack.Pop(), intStack.Pop());
                            lastMemoryCell = _pbStack.PeekCommand().MemoryCellUsed;
                            //intStack.Push(val);
                        }
                    }

                    if (intStack.Count == 0)
                    {
                        throw new CouldNotParseExpressionException();
                    }
                    retval.IntValue = intStack.Pop();
                    retval.ReturnType = SimplificationReturnValue.ReturnTypes.Integer;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return retval;
        }

        private static double DoMath(string op, double val1, double val2)
        {
            if (op == "*")
                return val1 * val2;
            if (op == "/")
                return val2 / val1;
            if (op == "+")
                return val1 + val2;
            if (op == "-")
                return val2 - val1;
            if (op == "%")
                return val2 % val1;
            if (op == "^")
                return Math.Pow(val2, val1);

            return 0f;
        }

        private static int DoMath(string op, int val1, int val2)
        {
            if (op == "*")
                return val1 * val2;
            if (op == "/")
                return val2 / val1;
            if (op == "+")
                return val1 + val2;
            if (op == "-")
                return val2 - val1;
            if (op == "%")
                return val2 % val1;
            if (op == "^")
                return (int)Math.Pow(val2, val1);

            return 0;
        }

        private static bool IsLeftAssociative(string op)
        {
            return op == "*" || op == "+" || op == "-" || op == "/" || op == "%";
        }

        /// <summary>
        /// Return priority of operations
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static int GetPrecedence(TokenParser.Tokens token)
        {
            if (token == TokenParser.Tokens.Add || token == TokenParser.Tokens.Subtract)
                return 1;

            if (token == TokenParser.Tokens.Multiply || token == TokenParser.Tokens.Divide || token == TokenParser.Tokens.Modulus)
                return 2;

            if (token == TokenParser.Tokens.Exponent)
                return 3;

            if (token == TokenParser.Tokens.Lparen || token == TokenParser.Tokens.Rparen)
                return 4;

            return 0;
        }

        private static int GetPrecedence(string op)
        {
            if (op.Equals("+") || op.Equals("-"))
                return 1;

            if (op.Equals("*") || op.Equals("/") || op.Equals("%"))
                return 2;

            if (op.Equals("^"))
                return 3;

            if (op.Equals("(") || op.Equals(")"))
                return 4;

            return 0;
        }

        private static bool IsOperator(TokenParser.Tokens token)
        {
            return token == TokenParser.Tokens.Add || token == TokenParser.Tokens.Divide || token == TokenParser.Tokens.Exponent ||
                   token == TokenParser.Tokens.Modulus || token == TokenParser.Tokens.Multiply || token == TokenParser.Tokens.Subtract;
        }

        private class FunctionClass
        {
            public string Expression
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            public FunctionArgumentList Arguments
            {
                get;
                set;
            }
        }

        private class NumberClass
        {
            public enum NumberTypes
            {
                Float,
                Integer,
                Operator,
                Expression
            }

            public NumberTypes NumberType
            {
                get;
                set;
            }

            public int IntNumber
            {
                get;
                set;
            }

            public double FloatNumber
            {
                get;
                set;
            }

            public string Operator
            {
                get;
                set;
            }

            public string Expression
            {
                get;
                set;
            }
        }
    }
}
