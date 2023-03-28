//15 var
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Drawing;

internal class Program
{
    private static double PreDichotomy(double a, double b, double e)
    {
        return (int)(Math.Truncate(Math.Log2((b - a)/ e)));
    }
    private static double Functinon(double x)
    {
        return 3*Math.Pow(x,2) - Math.Pow(Math.Cos(Math.PI * x), 2);
    }
    private static void Dichotomy() 
    {
        double epsilon = 1e-4;
        double a = 0;
        double b = Math.PI / 6;
        Console.WriteLine($"Апріорна оцінка = {PreDichotomy(a, b, epsilon)}");
        double currentX = (a + b) / 2;
        double previousX = 0;
        int n = 1;
        Console.WriteLine($"{"N",2} {"A",25} {"B",20} {"Xn",20} {"Xn-1",20}");
        do
        {
            Console.WriteLine($"{n,2} {a,25} {b,20} {currentX,20} {previousX,20}");
            if (Functinon(a) * Functinon(currentX) < 0)
            {
                b = currentX;
            }
            else
            {
                a = currentX;
            }
            previousX = currentX;
            currentX = (a + b) / 2;
            n++;
        }
        while (Math.Abs(currentX - previousX) >= 2 * epsilon);
        Console.WriteLine($"The X = {currentX}");
        Console.WriteLine($"F(X) = {Functinon(currentX)}");
    }
    private static double FunctinonFirstDerative(double x)
    {
        return 6 * x + Math.PI * Math.Sin(2 * x * Math.PI);
    }
    private static double Fi(double x)
    {
        return Math.Abs(Math.Acos(x * Math.Sqrt(3)) / Math.PI);
    }
    private int PreIterationsNewton(double q, double e)
    {
        double x = Math.PI / 36;
        double loged = Math.Log2(Math.Log(x / e) / Math.Log(1 / q) + 1);
        return (int)(Math.Truncate(loged) + 1);
    }
    private static int PreIterationsFPI(double x, double q, double e)
    {
        double logX = Math.Log( Math.Abs(Fi(x) - x)/((1 - q) * e));
        double logQ = Math.Log(1 / q);
        return (int)(Math.Truncate(logX / logQ) + 1);
    }
    private static void FixedPointIteration()
    {
        
        double epsilon = 1e-4;
        double q = Math.PI / 12;
        double flag = ((1 - q) / q) * epsilon;
        double previousX = Math.PI / 12;
        double currentX = Fi(previousX);
        Console.WriteLine($"The curX = {currentX}");
        Console.WriteLine($"preX = {previousX}");
        Console.WriteLine($"Апріорна оцінка = {PreIterationsFPI(previousX, q, epsilon)}");
        int count = 1;
        while(Math.Abs(currentX - previousX) >= flag)
        {
            previousX = currentX;
            currentX = Fi(previousX);
            Console.WriteLine($"The curX = {currentX}");
            Console.WriteLine($"preX = {previousX}");
            count++;
        }
        Console.WriteLine($"The X = {currentX}");
        Console.WriteLine($"F(X) = {Functinon(currentX)}");
        Console.WriteLine($"Апостеріорна оцінка = {count}");
    }
    private static void NewtonMethod()
    {
        double epsilon = 1e-4;
        double previousX = Math.PI / 12;
        double q = 0.45;
        Console.WriteLine($"Апріорна оцінка = {PreIterationsFPI(previousX, q, epsilon)}");
        double currentX = previousX - Functinon(previousX) / FunctinonFirstDerative(previousX);
        Console.WriteLine($"The curX = {currentX}");
        Console.WriteLine($"preX = {previousX}");
        int count = 1;
        while(Math.Abs(currentX - previousX) >= epsilon)
        {
            previousX = currentX;
            currentX = previousX - Functinon(previousX) / FunctinonFirstDerative(previousX);
            Console.WriteLine($"The curX = {currentX}");
            Console.WriteLine($"preX = {previousX}");
        }
        Console.WriteLine($"The X = {currentX}");
        Console.WriteLine($"F(X) = {Functinon(currentX)}");
        Console.WriteLine($"Апостеріорна оцінка = {count}");

    }
    private static void Main(string[] args)
    {
        Dichotomy();
        FixedPointIteration();
        NewtonMethod();
    }
}








