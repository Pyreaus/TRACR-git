namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
public abstract class CounterDependancyBase
{
    public abstract int Increment();
    public virtual void NonAbstractBaseMethod1() => Console.WriteLine("success 1");
    public virtual void NonAbstractBaseMethod2() => Console.WriteLine("success 2");
    public abstract int CurrentCounter { get; protected set; }
}