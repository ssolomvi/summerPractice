namespace summer_practice_domain.IntegralCalculation;

public class IntegralCalculationMiddleRuleRectangle : IntegralCalculationRuleRectangle
{
    public override string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + interval * intervalWidth + intervalWidth / 2);
    }
    
    public IntegralCalculationMiddleRuleRectangle()
    {
        MethodName = "Middle rectangle method";
    }
}
