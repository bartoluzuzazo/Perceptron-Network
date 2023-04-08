namespace NAI3_Network.Interfaces;

public interface IAssignable
{
    public IEnumerable<double> Attributes { get; set; }
    public string? DecisionAttribute { get; set; }
}