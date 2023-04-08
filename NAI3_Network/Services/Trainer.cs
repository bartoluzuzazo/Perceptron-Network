using NAI3_Network.Interfaces;
using NAI3_Network.Models;

namespace NAI3_Network.Services;

public static class Trainer
{
    public static void Train(ITrainable perceptron, IEnumerable<IAssignable> trainingSet)
    {
        trainingSet.ToList().ForEach(observation =>
        {
            var y = perceptron.CalculateNet(observation)>0;
            perceptron.AdjustWeights(observation.Attributes, perceptron.Label==observation.DecisionAttribute?1:0, y?1:0);
            perceptron.AdjustThreshold(perceptron.Label==observation.DecisionAttribute?1:0, y?1:0);
        });
    }

    public static double Test(ITrainable perceptron, IEnumerable<IAssignable> testSet, string expectedClass, string alternativeClass)
    {
        var correctCount = 0;
        var expectedCount = 0;
        var alternativeCount = 0;
        testSet.ToList().ForEach(observation =>
        {
            var assigned = perceptron.Assign(observation, expectedClass, alternativeClass);
            var isCorrect = observation.DecisionAttribute == assigned;
            
            if (isCorrect) correctCount++;
            if (isCorrect && assigned == expectedClass) expectedCount++;
            if (isCorrect && assigned == alternativeClass) alternativeCount++;
        });
        var expectedTotalCount = testSet.Count(o => o.DecisionAttribute == expectedClass);
        var accTotal = (double)correctCount / testSet.Count();
        var accExpClass = (double)expectedCount / expectedTotalCount;
        var accAltClass = (double)alternativeCount / (testSet.Count() - expectedTotalCount);
        Console.WriteLine($"Total accuracy: {accTotal} ({(int)(accTotal*100)}%)");
        Console.WriteLine($"{expectedClass} accuracy: {accExpClass} ({(int)(accExpClass*100)}%)");
        Console.WriteLine($"{alternativeClass} accuracy: {accAltClass} ({(int)(accAltClass*100)}%)");
        
        return accTotal;
    }
    
}