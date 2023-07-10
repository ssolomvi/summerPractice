namespace summer_practice_domain.IntegralCalculation;

public class IntegralCalculationTrapezoidal : IIntegralCalculation
{
    public string? MethodName { get; }
    
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
            result = integrand(lowerBound) * widthOfPartition / 2 + integrand(highBound) * widthOfPartition / 2;

            for (int j = 1; j < numberOfPartitions; j++)
            {
                result += integrand(lowerBound + j * widthOfPartition) * widthOfPartition;
            }

            ++iterationNumber;
        } while (Math.Abs(prevResult - result) >= eps);

        return new KeyValuePair<double, int>(result, iterationNumber);
    }

    public IntegralCalculationTrapezoidal()
    {
        MethodName = "Trapezoidal rule method";
    }
}
