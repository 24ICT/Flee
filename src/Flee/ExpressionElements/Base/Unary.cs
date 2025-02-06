﻿using Flee.PublicTypes;
using Flee.Resources;

namespace Flee.ExpressionElements.Base
{
    internal abstract class UnaryElement : ExpressionElement
    {

        protected ExpressionElement MyChild = default!;

        private Type? _myResultType;
        public void SetChild(ExpressionElement child)
        {
            MyChild = child;
            _myResultType = this.GetResultType(child.ResultType);

            if (_myResultType == null)
            {
                base.ThrowCompileException(CompileErrorResourceKeys.OperationNotDefinedForType, CompileExceptionReason.TypeMismatch, MyChild.ResultType.Name);
            }
        }

        protected abstract Type? GetResultType(Type childType);

        public override Type ResultType => _myResultType!;
    }

}
