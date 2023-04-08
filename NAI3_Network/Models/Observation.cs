using NAI3_Network.Interfaces;

namespace NAI3_Network.Models;

public class Observation : IAssignable
{
    public IEnumerable<double> Attributes { get; set; }
    public string? DecisionAttribute { get; set; }

    public void Print()
    {
        Attributes.ToList().ForEach(a => Console.Write(a + "; "));
        Console.WriteLine(DecisionAttribute);
    }
}