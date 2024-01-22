namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
internal interface ITransientCounterDependancy : ICounterDependancy {} 
internal sealed class TransientCounterDependancy : CounterDependancyBase<TransientCounterDependancy>, ITransientCounterDependancy
{
    public override int CurrentCounter { get; protected set; } = 0;
    public TransientCounterDependancy(ILogger<TransientCounterDependancy> logger) : base(logger) {}
    public override int Increment() => ++CurrentCounter;
}

//builder.Services.AddTransient<ITransientCounterDependancy, TransientCounterDependancy>(); 