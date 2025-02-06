using Flee.ExpressionElements.Base.Literals;
using Flee.InternalTypes;
using System.Globalization;


namespace Flee.ExpressionElements.Literals.Integral
{
    internal class UInt64LiteralElement : IntegralLiteralElement
    {
        private readonly ulong _myValue;
        public UInt64LiteralElement(string image, NumberStyles ns)
        {
            try
            {
                _myValue = ulong.Parse(image, ns);
            }
            catch (OverflowException)
            {
                base.OnParseOverflow(image);
            }
        }

        public UInt64LiteralElement(ulong value)
        {
            _myValue = value;
        }

        public override void Emit(FleeILGenerator ilg, IServiceProvider services)
        {
            EmitLoad(Convert.ToInt64(_myValue), ilg);
        }

        public override Type ResultType => typeof(ulong);
    }
}
