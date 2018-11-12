using System;

namespace TypeCast.Converters
{
    public sealed class BoolCheckboxConverter : DataTypeConverterBase<int, bool>
    {
        public override bool Create(int input, Action<object> contextAction = null)
        {
            return input == 1;
        }

        public override int Serialise(bool input)
        {
            return input ? 1 : 0;
        }
    }
}