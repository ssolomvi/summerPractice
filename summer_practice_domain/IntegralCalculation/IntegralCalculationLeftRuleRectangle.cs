namespace summer_practice_domain.IntegralCalculation;

public class IntegralCalculationLeftRuleRectangle : IntegralCalculationRuleRectangle
{
    public IntegralCalculationLeftRuleRectangle()
    {
        MethodName = "Left rule rectangle method";
    }
    public override string MethodName { get; }

    protected override double MethodArgument(IIntegralCalculation.Integrand integrand, double lowerBound, int interval, double intervalWidth)
    {
        return integrand(lowerBound + interval * intervalWidth);
    }
}
