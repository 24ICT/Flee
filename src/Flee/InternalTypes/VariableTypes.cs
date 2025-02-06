using Flee.PublicTypes;

namespace Flee.InternalTypes
{
    internal interface IVariable
    {
        IVariable Clone();
        Type VariableType { get; }
        object ValueAsObject { get; set; }
    }

    internal interface IGenericVariable<T>
    {
        object GetValue();
    }

    internal class DynamicExpressionVariable<T> : IVariable, IGenericVariable<T>
    {
        private IDynamicExpression _myExpression = default!;
        public IVariable Clone()
        {
            DynamicExpressionVariable<T> copy = new DynamicExpressionVariable<T>();
            copy._myExpression = _myExpression;
            return copy;
        }

        public object GetValue()
        {
            return (T)_myExpression.Evaluate();
        }

        public object ValueAsObject
        {
            get { return _myExpression; }
            set { _myExpression = (IDynamicExpression)value; }
        }

        public Type VariableType => _myExpression.Context.Options.ResultType ?? default!;
    }

    internal class GenericExpressionVariable<T> : IVariable, IGenericVariable<T>
    {
        private IGenericExpression<T> _myExpression = default!;
        public IVariable Clone()
        {
            GenericExpressionVariable<T> copy = new GenericExpressionVariable<T>();
            copy._myExpression = _myExpression;
            return copy;
        }

        public object GetValue()
        {
            return _myExpression.Evaluate() ?? default!;
        }

        public object ValueAsObject
        {
            get { return _myExpression; }
            set { _myExpression = (IGenericExpression<T>)value; }
        }

        public Type VariableType => _myExpression.Context.Options.ResultType ?? default!;
    }

    internal class GenericVariable<T> : IVariable, IGenericVariable<T>
    {


        public object MyValue = default!;
        public IVariable Clone()
        {
            GenericVariable<T> copy = new GenericVariable<T> { MyValue = MyValue };
            return copy;
        }

        public object GetValue()
        {
            return MyValue;
        }

        public Type VariableType => typeof(T);

        public object ValueAsObject
        {
            get { return MyValue; }
            set
            {
                if (value == null)
                {
                    MyValue = default!;
                }
                else
                {
                    MyValue = value;
                }
            }
        }
    }

}
