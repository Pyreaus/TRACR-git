namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;

internal interface ICounterDependancy
{
    int Increment();
    int CurrentCounter { get; }
    void NonAbstractBaseMethod1();
    void NonAbstractBaseMethod2();
}