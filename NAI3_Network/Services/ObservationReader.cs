using NAI3_Network.Interfaces;
using NAI3_Network.Models;

namespace NAI3_Network.Services;

public static class ObservationReader
{
    public static List<IAssignable> Read(string path)
    {
        var dataSet = File.ReadLines(path);
        var observations = new List<IAssignable>();
        dataSet.ToList().ForEach(line =>
        {
            var lineContent = line.Split(";").ToList();
            if (lineContent.Any(e => e.Trim() == ""))
            {
                return;
            }

            observations.Add(new Observation
            {
                Attributes = lineContent.GetRange(0, lineContent.Count - 1).Select(x => Double.Parse(x.Replace(".", ","))).ToList(),
                DecisionAttribute = lineContent.Last(),
            });
        });
        return observations;
    }
}