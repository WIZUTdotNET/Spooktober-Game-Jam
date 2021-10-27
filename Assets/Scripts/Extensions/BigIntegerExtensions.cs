using System.Numerics;

namespace Extensions
{
    public static class BigIntegerExtensions
    {
        private static int Precision = 3;
        private static readonly string[] UnitNames = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion", "Unvigintillion", "Duovigintillion", "Trevigintillion", "Quattuorvigintillion", "Quinvigintillion", "Sexvigintillion", "Septenvigintillion", "Octovigintillion", "Novemvigintillion", "Trigintillion", "Untrigintillion", "Duotrigintillion", "Googol", "Tretrigintillion", "Quattuortrigintillion", "Quintrigintillion", "Sextrigintillion", "Septentrigintillion", "Octotrigintillion", "Novemtrigintillion" };

        public static string ToHumanString(this BigInteger value)
        {
            if (value.IsZero)
                return "0.000";
            var representation = value.ToString();
            int unitIndex = (representation.Length - Precision - 7) / 3;
            if (representation.Length <= Precision)
            {
                return "0." + new string('0', Precision - representation.Length) + representation;
            }
            else if (representation.Length <= 6 + Precision)
            {
                return representation.Insert(representation.Length - Precision, ".");
            }
            else if (unitIndex >= 0 && unitIndex < UnitNames.Length)
            {
                return representation.Substring(0, 1 + Precision).Insert((representation.Length - Precision - 1) % 3 + 1, ".")
                    + " "
                    + UnitNames[unitIndex];
            }
            else
            {
                return representation.Substring(0, 1 + Precision).Insert(1, ".") + "E" + (representation.Length - Precision - 1).ToString();
            }
        }
    }

}