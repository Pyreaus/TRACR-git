namespace Bristows.TRACR.API.TESTDEV.LINQ;
public class LINQPractice
{
    public List<double> grades2 = new List<double> { 22, 32, 23, 53, 32 };
    public Dictionary<int, string> grades1 = new Dictionary<int, string>() { {1,"A"},{2,"B"},{3,"C"} };
    // Imperative programming ---------------------------------------
    public double ImperativeApproach()
    {
        double sum = 0;
        for (int i = 0; i < grades2.Count; i++) sum += grades2[i];
        return sum / grades2.Count;
    }
    // --------------------------------------------------------------
    // Declarative programming --------------------------------------
    public double DeclarativeApproach() => grades2.Count();
    // --------------------------------------------------------------

}