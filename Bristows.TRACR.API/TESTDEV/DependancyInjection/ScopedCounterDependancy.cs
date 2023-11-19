namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
public interface IScopedCounterDependancy : ICounterDependancy {}
public class ScopedCounterDependancy : CounterDependancyBase, IScopedCounterDependancy
{
    public override int CurrentCounter { get; protected set; } = 0;
    public override int Increment() => ++CurrentCounter;
}