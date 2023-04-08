using NAI3_Network.Interfaces;
using NAI3_Network.Models;

namespace NAI3_Network.Services;

public static class CLI
{
    public static void Init(ITrainable perceptron, string expectedClass, string alternativeClass)
    {
        while (true)
        {
            try
            {
                var line = Console.ReadLine();
                if (line.ToLower().Contains("exit")) break;
                var numbers = line.Split(";").ToList().Select(str => Double.Parse(str.Replace(".", ","))).ToList();
                var newObs = new Observation()
                {
                    Attributes = numbers,
                };

                Console.WriteLine(perceptron.Assign(newObs, expectedClass, alternativeClass));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}