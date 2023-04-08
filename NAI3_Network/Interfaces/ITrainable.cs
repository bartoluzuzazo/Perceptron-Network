namespace NAI3_Network.Interfaces;

public interface ITrainable
{
    public string Label { get; init; }
    public double LearningRate { get; init; }
    public IEnumerable<double> Weights { get; set; }
    public double Threshold { get; set; }
    public void AdjustWeights(IEnumerable<double> X, double d, double y);
    public void AdjustThreshold(double d, double y);
    public double CalculateNet(IAssignable assignable);
    public string Assign(IAssignable observation, string expectedClass, string alternativeClass);
    public double NormalizedNet(IAssignable assignable);
}