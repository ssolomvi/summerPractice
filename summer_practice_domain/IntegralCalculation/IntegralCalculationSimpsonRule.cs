namespace summer_practice_domain.IntegralCalculation;

public class IntegralCalculationSimpsonRule : IIntegralCalculation
{
    public string? MethodName { get; }
    
    public KeyValuePair<double, int> IntegralCalculation(IIntegralCalculation.Integrand integrand, double lowerBound, double highBound, double eps)
    {
        IIntegralCalculation.CheckBoundsAndEps(ref lowerBound, ref highBound, ref eps);
        double prevResult, result = 0;
        int iterationNumber = 1;
        
        do
        {
            int numberOfPartitions = 1 << (iterationNumber + 1);
            double widthOfPartition = (highBound - lowerBound) / numberOfPartitions;
            
            prevResult = result;
            result = (integrand(lowerBound) + integrand(highBound)) / 3 * widthOfPartition;

            double resPart1 = 0, resPart2 = 0;
            int j = 0;
            for (j = 0; j < (numberOfPartitions >> 1); j++)
            {
                resPart1 += integrand(lowerBound + j * 2 * widthOfPartition);
                resPart2 += integrand(lowerBound + (j * 2 - 1) * widthOfPartition);
            }
            resPart2 += integrand(lowerBound + (j * 2 - 1) * widthOfPartition);
            result += resPart1 / 3 * 2 * widthOfPartition;
            result += resPart2 / 3 * 4 * widthOfPartition; 
            
            ++iterationNumber;
        } while (Math.Abs(prevResult - result) >= eps);

        return new KeyValuePair<double, int>(result, iterationNumber);
    }

    public IntegralCalculationSimpsonRule()
    {
        MethodName = "Simpson rule method";
    }
}
