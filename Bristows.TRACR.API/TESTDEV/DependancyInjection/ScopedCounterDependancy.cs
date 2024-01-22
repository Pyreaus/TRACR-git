namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
internal interface IScopedCounterDependancy : ICounterDependancy {}
internal sealed class ScopedCounterDependancy : CounterDependancyBase<ScopedCounterDependancy>, IScopedCounterDependancy
{
    public override int CurrentCounter { get; protected set; } = 0;
    public ScopedCounterDependancy(ILogger<ScopedCounterDependancy> logger) : base(logger) {}
    public override int Increment() => ++CurrentCounter;
}

//builder.Services.AddScoped<IScopedCounterDependancy, ScopedCounterDependancy>();