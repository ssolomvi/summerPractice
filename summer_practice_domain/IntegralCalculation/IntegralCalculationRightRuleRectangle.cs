namespace summer_practice_domain.IntegralCalculation;

public class IntegralCalculationRightRuleRectangle : IntegralCalculationRuleRectangle
{
    public IntegralCalculationRightRuleRectangle()
    {
        MethodName = "Right rule rectangle method";
    }
    
    public override string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + (interval + 1) * intervalWidth);
    }
}
