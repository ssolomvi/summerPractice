namespace summer_practice_domain.IntegralCalculation;

public abstract class IntegralCalculationRuleRectangle : IIntegralCalculation
{
    public abstract string MethodName { get; }

    protected abstract double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval,
        double intervalWidth);

    public KeyValuePair<double, int> IntegralCalculation(IIntegralCalculation.Integrand integrand, double lowerBound, double highBound, double eps)
    {
        IIntegralCalculation.CheckBoundsAndEps(ref lowerBound, ref highBound, ref eps);
        double prevResult, result = 0;
        int iterationNumber = 1;
        do
        {
            int numberOfPartitions = 1 << iterationNumber;
            double widthOfPartition = (highBound - lowerBound) / numberOfPartitions;
            
            prevResult = result;
            result = 0;

            for (int j = 0; j < numberOfPartitions; j++)
            {
                result += MethodArgument(integrand, lowerBound, j, widthOfPartition);
            }

            result *= widthOfPartition;
            ++iterationNumber;
        } while (Math.Abs(prevResult - result) >= eps);

        return new KeyValuePair<double, int>(result, iterationNumber);
    }
}

