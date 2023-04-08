using NAI3_Network.Interfaces;

namespace NAI3_Network.Models;

public class Perceptron : ITrainable
{
    public string Label { get; init; }
    public double LearningRate { get; init; }

    public IEnumerable<double> Weights { get; set; }

    public double Threshold { get; set; }

    public void AdjustWeights(IEnumerable<double> X, double d, double y)
    {
        Weights = Weights.Zip(X, (w, x) => w + (d - y) * x*LearningRate);
    }

    public void AdjustThreshold(double d, double y)
    {
        Threshold = Threshold - (d - y) * LearningRate;
    }

    public double CalculateNet(IAssignable assignable)
    {
        if (assignable.Attributes.Count() != Weights.Count())
            throw new Exception("Provided vector has invalid amount of elements");
        return assignable.Attributes.Zip(Weights, (x, w) => x * w).Sum() - Threshold;
    }

    public double NormalizedNet(IAssignable assignable)
    {
        if (assignable.Attributes.Count() != Weights.Count())
            throw new Exception("Provided vector has invalid amount of elements");
        
        var lenW = Math.Sqrt(Weights.Select(w => w * w).Sum());
        var normalizedW = Weights.Select(w => w / lenW);
        
        var lenX = Math.Sqrt(assignable.Attributes.Select(x => x * x).Sum());
        var normalizedX = assignable.Attributes.Select(x => x / lenX);
        var re = normalizedX.Zip(normalizedW, (x, w) => x * w).Sum();
        return re;
    }
    
    public string Assign(IAssignable observation, string expectedClass, string alternativeClass)
    {
        return CalculateNet(observation) > 0 ? expectedClass : alternativeClass;
    }
}