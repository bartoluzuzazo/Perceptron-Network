using NAI3_Network.Interfaces;
using NAI3_Network.Services;

namespace NAI3_Network.Models;

public class NetworkLayer
{
    public IEnumerable<ITrainable> Perceptrons { get; set; }

    public void FeedData(IEnumerable<IAssignable> trainingSet)
    {
        var trained = Perceptrons.ToList();
        trained.ForEach(perceptron => Trainer.Train(perceptron, trainingSet));
        Perceptrons = trained;
    }

    public string Assign(IAssignable observation)
    {
        var outs = Perceptrons.Select(perceptron => new KeyValuePair<string,double>(perceptron.Label, perceptron.NormalizedNet(observation))).ToList();
        // outs.ToList().ForEach(p=>Console.WriteLine($"{p.Key}: {p.Value}"));
        return outs.Aggregate((p1, p2) => p1.Value>p2.Value?p1:p2).Key;
    }

    public ITrainable? GetByLabel(string label)
    {
        return Perceptrons.FirstOrDefault(p => p.Label == label);
    }
}