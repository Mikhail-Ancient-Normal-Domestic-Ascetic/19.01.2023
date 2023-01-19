//class Program
//{
//    public static void Main()
//    {
//        Console.Write("первое число (левого диапазона) ");
//        int first = Convert.ToInt32(Console.ReadLine());
//        Console.Write("второе число (правого диапазона) ");
//        int second = Convert.ToInt32(Console.ReadLine());
//        Console.Write("число х ");
//        int x = Convert.ToInt32(Console.ReadLine());
//        var t = new Test(first, second);
//        Console.WriteLine(t.RangeCheck(x));
//        Console.ReadLine();
//    }

//    class Test
//    {
//        public Test(int first, int second)
//        {
//            this.first = first;
//            this.second = second;
//        }
//        int first;
//        int second;
//        public bool RangeCheck(int x)
//        {
//            return x >= first && x < second;
//        }
//    }
//}


public struct RationalNumber
{

    public int Numerator { get; private set; }
        public uint Denominator { get; private set; }
        public double Rational => ((double)Numerator) / ((double)Denominator);
        public RationalNumber(int Numerator, uint Denominator)
        {
            if (Denominator == 0)
                throw new Exception("Знаменатель не может быть равен нулю!");
            uint gcd = 1;
            if (Numerator == 0)
            {
                this.Numerator = 0;
                this.Denominator = 1;
                return;
            }
            if (Numerator > 0)
                gcd = GCD((uint)Numerator, Denominator);
            else
                gcd = GCD((uint)-Numerator, Denominator);
            this.Numerator = Numerator / ((int)gcd);
            this.Denominator = Denominator / gcd;
        }

        public static implicit operator double(RationalNumber r1) => r1.Rational;
        public static implicit operator RationalNumber(int r1) => new RationalNumber(r1, 1);

        public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber
                (
                    (int)(r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator),
                    r1.Denominator * r2.Denominator
                );
        }
        public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber
                (
                    (int)(r1.Numerator * r2.Numerator),
                     (uint)(r1.Denominator * r2.Denominator)
                );
        }
        public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        {
            return r1 + (-1 * r2);
        }
        public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        {
            return r1.Numerator * r2.Numerator < 0
                ? new RationalNumber((int)-r2.Denominator, (uint)-r2.Numerator)
                : new RationalNumber((int)r2.Denominator, (uint)r2.Numerator);
        }

        public static bool operator >(RationalNumber r1, RationalNumber r2)
        {
            return (r1 - r2).Numerator > 0;
        }
        public static bool operator <(RationalNumber r1, RationalNumber r2)
        {
            return (r1 - r2).Numerator < 0;
        }
        public static bool operator ==(RationalNumber r1, RationalNumber r2)
        {
            return (r1 - r2).Numerator == 0;
        }
        public static bool operator <=(RationalNumber r1, RationalNumber r2)
        {
            return !(r1 > r2);
        }
        public static bool operator >=(RationalNumber r1, RationalNumber r2)
        {
            return !(r1 < r2);
        }
        public static bool operator !=(RationalNumber r1, RationalNumber r2)
        {
            return !(r1 == r2);
        }
        public static uint GCD(uint A, uint B)
        {
            uint C = A;
            A = A > B ? A : B;
            B = C > B ? B : C;
            C = A % B;
            if (C == 0) return B;
            return GCD(B, C);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RationalNumber))
            {
                return false;
            }

            var number = (RationalNumber)obj;
            return Numerator == number.Numerator &&
                   Denominator == number.Denominator;
        }

        public override int GetHashCode()
        {
            var hashCode = -1534900553;
            hashCode = hashCode * -1521134295 + Numerator.GetHashCode();
            hashCode = hashCode * -1521134295 + Denominator.GetHashCode();
            return hashCode;
        }
}