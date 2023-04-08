using NAI3_Network.Models;
using NAI3_Network.Services;

var alpha = Double.Parse(args[0]);
var testPath = "../../../TestSets";

var datasets = DataLoader.Load("../../../TrainingSet");
var trainingSet = datasets.Select(set => set.Assignable());

var languageTestSet = DataLoader.Load(testPath);
var testSet = languageTestSet.Select(set => set.Assignable());

var labels = trainingSet.Select(Observation => Observation.DecisionAttribute).Distinct();

var random = new Random();
var weights = new List<double>();
for (int i = 0; i < trainingSet.First().Attributes.Count(); i++)
{
    weights.Add(-5 + random.NextDouble() * 10);
}

var Layer = new NetworkLayer()
{
    Perceptrons = labels.Select(label => new Perceptron()
    {
        Label = label,
        LearningRate = alpha,
        Threshold = -5 + random.NextDouble() * 10,
        Weights = weights
    })
};

for (int i = 0; i < 50; i++)
{
    Console.WriteLine($"{i}\n");
    
    trainingSet = trainingSet.OrderBy(o => random.Next()).ToList();
    
    Layer.FeedData(trainingSet);

    testSet.ToList().ForEach(tr =>
    {
        Console.WriteLine("Correct: " + tr.DecisionAttribute);
        var dec = Layer.Assign(tr);
        Console.WriteLine("Answer: " + dec + "\n");
    });
}