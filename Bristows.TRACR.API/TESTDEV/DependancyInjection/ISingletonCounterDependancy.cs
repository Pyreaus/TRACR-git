namespace Bristows.TRACR.API.TESTDEV.DependancyInjection;
internal interface ISingletonCounterDependancy : ICounterDependancy {}
internal sealed class SingletonCounterDependancy : CounterDependancyBase<SingletonCounterDependancy>, ISingletonCounterDependancy
{
    private readonly IConfiguration _configuration;
    public override int CurrentCounter { get; protected set; } = 0;
    public SingletonCounterDependancy(ILogger<SingletonCounterDependancy> logger, IConfiguration configuration) : base(logger)
    {
        _configuration = configuration;
    }
    public override int Increment() => ++CurrentCounter;
    public override void NonAbstractBaseMethod2() 
    {
        ++CurrentCounter;
        if (CurrentCounter < 5) {
            Console.WriteLine($"Key - {_configuration["API:APIkey"]}"); 
        } else 
        {
            Console.WriteLine($"Exceeded - {CurrentCounter}");
        }
    }
}

// builder.Services.AddSingleton<ISingletonCounterDependancy, SingletonCounterDependancy>();