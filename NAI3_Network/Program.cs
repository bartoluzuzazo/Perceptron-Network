using NAI3_Network.Models;
using NAI3_Network.Services;

var alpha = double.Parse(args[0]);
var expectedAccuracy = double.Parse(args[1]);

const string testPath = "../../../TestSet";
const string trainingPath = "../../../TrainingSet";

var datasets = DataLoader.Load(trainingPath);
var trainingSet = datasets.Select(set => set.Assignable()).ToList();

var languageTestSet = DataLoader.Load(testPath);
var testSet = languageTestSet.Select(set => set.Assignable());

var labels = trainingSet.Select(Observation => Observation.DecisionAttribute).Distinct();

var random = new Random();
var weights = new List<double>();
for (int i = 0; i < trainingSet.First().Attributes.Count(); i++)
{
    weights.Add(-5 + random.NextDouble() * 10);
}

var layer = new NetworkLayer()
{
    Perceptrons = labels.Select(label => new Perceptron()
    {
        Label = label,
        LearningRate = alpha,
        Threshold = -5 + random.NextDouble() * 10,
        Weights = weights
    })
};

var oldWeights = layer.Perceptrons;

for (var i = 0; i < 50; i++)
{
    var correct = 0;

    Console.WriteLine($"Epoch: {i+1}");

    trainingSet = trainingSet.OrderBy(o => random.Next()).ToList();

    layer.FeedData(trainingSet);

    testSet.ToList().ForEach(tr =>
    {
        // Console.WriteLine("Correct: " + tr.DecisionAttribute);
        var dec = layer.Assign(tr);
        // Console.WriteLine("Answer: " + dec + "\n");
        if (dec == tr.DecisionAttribute) correct++;
    });
    var accuracy = (double)correct / testSet.Count();
    Console.WriteLine($"{accuracy} ({accuracy*100}%)");
    
    if (accuracy >= expectedAccuracy) break;
    
    oldWeights.ToList().ForEach(p => 
    { 
        if (p.Weights.SequenceEqual(layer.GetByLabel(p.Label).Weights)) Console.WriteLine("dupa");
    });
}

Console.WriteLine("Training complete.");

while (true)
{
    var line = Console.ReadLine();
    var newSet = new LanguageSet().Absorb(line).Assignable();
    Console.WriteLine(layer.Assign(newSet));
}
