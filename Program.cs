using System;

namespace ExamCS1
{
    interface IFunction
    {
        double Value(double v);
    }

    class CompareFunctions
    {
        private readonly IFunction f;
        private readonly IFunction g;

        public CompareFunctions(IFunction f, IFunction g)
        {
            this.f = f ?? throw new ArgumentNullException(nameof(f));
            this.g = g ?? throw new ArgumentNullException(nameof(g));
        }

        public double Diff(double x0, double x1)
        {
            double maxDifference = double.MinValue; // на випадок нескінченності в результатах функцій

            for (double x = x0; x <= x1; x += 0.01)
            {
                double difference = Math.Abs(f.Value(x) - g.Value(x));
                maxDifference = Math.Max(maxDifference, difference);
            }

            return maxDifference;
        }
    }

    static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            IFunction iter1_F1, iter1_F2;
            int choices = 3;
            for (int i = 0; i < choices; i++)
            {
                switch (i)
                {
                    case 0:
                        iter1_F1 = new SelfAdd();
                        iter1_F2 = new SelfMultiply();
                        break;
                    case 1:
                        iter1_F1 = new SumSquare();
                        iter1_F2 = new SumSquareRoot();
                        break;
                    case 2:
                        iter1_F1 = new SumSin();
                        iter1_F2 = new SumCos();
                        break;
                    default:
                        throw new InvalidOperationException("Обрано неіснуючу комбінацію функцій.");
                }

                CompareFunctions comparer = new(iter1_F1, iter1_F2);
                double delta = comparer.Diff(0, 1);

                Console.WriteLine($"\"Відстань\" між функціями становить {delta}.");
            }
        }
    }
    class SelfAdd : IFunction
    {
        public double Value(double v)
        {
            return v + v;
        }
    }

    class SelfMultiply : IFunction
    {
        public double Value(double v)
        {
            return v * v;
        }
    }

    class SumSquare : IFunction
    {
        public double Value(double v)
        {
            return Math.Pow((v + v),2);
        }
    }
    class SumSquareRoot : IFunction
    {
        public double Value(double v)
        {
            return Math.Sqrt(v + v);
        }
    }

    class SumSin : IFunction
    {
        public double Value(double v)
        {
            return Math.Sin(v + v);
        }
    }
    class SumCos : IFunction
    {
        public double Value(double v)
        {
            return Math.Cos(v + v);
        }
    }
}