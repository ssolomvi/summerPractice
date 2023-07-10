namespace summer_practice_domain.IntegralCalculation;

public interface IIntegralCalculation
{
    public string? MethodName
    {
        get;
    }

    public delegate double Integrand(double x);

    public static void CheckBoundsAndEps(ref double lowerBound, ref double upperBound, ref double eps)
    {
        if (lowerBound > upperBound)
        {
            (lowerBound, upperBound) = (upperBound, lowerBound);
        }

        if (eps < 0)
        {
            eps = -eps;
        }
    }

    public KeyValuePair<double, int> IntegralCalculation(Integrand integrand, double lowerBound, double highBound, double eps);
}
