namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
internal abstract class CounterDependancyBase<TL>
{
    public abstract int CurrentCounter { get; protected set; }
    private readonly ILogger<TL> _logger;
    protected CounterDependancyBase(ILogger<TL> logger) => _logger = logger;
    public abstract int Increment();
    public virtual void NonAbstractBaseMethod1() => Console.WriteLine("");
    public virtual void NonAbstractBaseMethod2() => Console.WriteLine("");
    public virtual Dictionary<string[], int[]> NonAbstractBaseMethod4() => Maps.ArraytMap;
    public virtual Dictionary<List<string>, List<int>> NonAbstractBaseMethod3() => Maps.ListtMap;

}

internal static class Maps
{
    public static readonly Dictionary<string[], int[]> ArraytMap = new()
    {
        {new string[]{"1","A"}, new int[]{4,32,5,4354,234,24,54}},
        {new string[]{"2","B"}, new int[]{54,332,34,543,2,12,35,43}}
    };
    public static readonly Dictionary<List<string>, List<int>> ListtMap = new()
    {
        {new List<string>(){"1","A"}, new List<int>(){4,32,5,4354,234,24,54}},
        {new List<string>(){"2","B"}, new List<int>(){54,332,34,543,2,12,35,43}}
    };
}
