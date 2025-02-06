﻿using System.Globalization;
using Flee.ExpressionElements.Base.Literals;

using Flee.InternalTypes;

namespace Flee.ExpressionElements.Literals.Integral
{
    internal class Int64LiteralElement : IntegralLiteralElement
    {
        private long _myValue;
        private const string MinValue = "9223372036854775808";

        private readonly bool _myIsMinValue;
        public Int64LiteralElement(long value)
        {
            _myValue = value;
        }

        private Int64LiteralElement()
        {
            _myIsMinValue = true;
        }

        public static Int64LiteralElement? TryCreate(string image, bool isHex, bool negated)
        {
            if (negated == true & image == MinValue)
            {
                return new Int64LiteralElement();
            }
            else if (isHex == true)
            {
                long value = default!;

                if (long.TryParse(image, NumberStyles.AllowHexSpecifier, null, out value) == false)
                {
                    return null;
                }
                else if (value >= 0 & value <= long.MaxValue)
                {
                    return new Int64LiteralElement(value);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                long value = default!;

                if (long.TryParse(image, out value) == true)
                {
                    return new Int64LiteralElement(value);
                }
                else
                {
                    return null;
                }
            }
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            EmitLoad(_myValue, ilg);
        }

        public void Negate()
        {
            if (_myIsMinValue == true)
            {
                _myValue = long.MinValue;
            }
            else
            {
                _myValue = -_myValue;
            }
        }

        public override Type ResultType => typeof(long);
    }
}
