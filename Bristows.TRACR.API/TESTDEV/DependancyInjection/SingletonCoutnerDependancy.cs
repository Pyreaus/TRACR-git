namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
public interface ISingletonCounterDependancy : ICounterDependancy {}
public class SingletonCounterDependancy : CounterDependancyBase, ISingletonCounterDependancy
{
    public override int CurrentCounter { get; protected set; } = 0;
    public override int Increment() => ++CurrentCounter;
}