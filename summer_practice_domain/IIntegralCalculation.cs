namespace summer_practice_domain;

public interface IIntegralCalculation
{
    public string? MethodName
    {
        get;
    }

    public delegate double Integrand(double x);

    public double IntegralCalculation(Integrand integrand, double lowerBound, double highBound, double eps);
}

public abstract class IntegralCalculationRuleRectangle : IIntegralCalculation
{
    public string? MethodName { get; }

    protected abstract double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval,
        double intervalWidth);

    public double IntegralCalculation(IIntegralCalculation.Integrand integrand, double lowerBound, double highBound, double eps)
    {
        double prevResult, result = 0;
        int iterationNumber = 1;
        do
        {
            prevResult = result;
            result = 0;
            
            int numberOfPartitions = 1 << iterationNumber;
            double widthOfPartition = (highBound - lowerBound) / numberOfPartitions;
            
            for (int j = 0; j < numberOfPartitions; j++)
            {
                result += MethodArgument(integrand, lowerBound, j, widthOfPartition);
            }

            result *= widthOfPartition;
            ++iterationNumber;
        } while (Math.Abs(prevResult - result) >= eps);

        return result;
    }
}

public class IntegralCalculationLeftRuleRectangle : IntegralCalculationRuleRectangle
{
    public IntegralCalculationLeftRuleRectangle()
    {
        MethodName = "Left rule rectangle method";
    }
    public new string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + interval * intervalWidth);
    }
}

public class IntegralCalculationRightRuleRectangle : IntegralCalculationRuleRectangle
{
    public IntegralCalculationRightRuleRectangle()
    {
        MethodName = "Right rule rectangle method";
    }
    
    public new string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + (interval + 1) * intervalWidth);
    }
}

public class IntegralCalculationMiddleRuleRectangle : IntegralCalculationRuleRectangle
{
    public new string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + interval * intervalWidth + intervalWidth / 2);
    }
    
    public IntegralCalculationMiddleRuleRectangle()
    {
        MethodName = "Middle rectangle method";
    }
}

public class IntegralCalculationTrapezoidal : IIntegralCalculation
{
    public string? MethodName { get; }
    
    public double IntegralCalculation(IIntegralCalculation.Integrand integrand, double lowerBound, double highBound, double eps)
    {
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

        return result;
    }

    public IntegralCalculationTrapezoidal()
    {
        MethodName = "Trapezoidal rule method";
    }
}

public class IntegralCalculationSimpsonRule : IIntegralCalculation
{
    public string? MethodName { get; }
    
    public double IntegralCalculation(IIntegralCalculation.Integrand integrand, double lowerBound, double highBound, double eps)
    {
        double prevResult, result = 0;
        int iterationNumber = 1;
        do
        {
            int numberOfPartitions = 1 << (iterationNumber + 1);
            double widthOfPartition = (highBound - lowerBound) / numberOfPartitions;
            
            prevResult = result;
            result = (integrand(lowerBound) + integrand(highBound)) / 3 * widthOfPartition;

            double resPart1 = 0, resPart2 = 0;
            for (int j = 0; j < (numberOfPartitions >> 1); j++)
            {
                resPart1 += integrand(lowerBound + j * 2 * widthOfPartition);
            }

            result += resPart1 / 3 * 2 * widthOfPartition;

            for (int j = 0; j <= (numberOfPartitions >> 1); j++)
            {
                resPart2 += integrand(lowerBound + (j * 2 - 1) * widthOfPartition);
            }

            result += resPart2 / 3 * 4 * widthOfPartition; 
            
            ++iterationNumber;
        } while (Math.Abs(prevResult - result) >= eps);

        return result;
    }

    public IntegralCalculationSimpsonRule()
    {
        MethodName = "Simpson rule method";
    }
}
