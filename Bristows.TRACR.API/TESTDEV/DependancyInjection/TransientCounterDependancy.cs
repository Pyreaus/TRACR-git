namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
public interface ITransientCounterDependancy : ICounterDependancy {}
public class TransientCounterDependancy : CounterDependancyBase, ITransientCounterDependancy
{
    public override int CurrentCounter { get; protected set; } = 0;
    public override int Increment() => ++CurrentCounter;
}