using NAI3_Network.Interfaces;

namespace NAI3_Network.Models;

public class LanguageSet : Dictionary<char, double>
{
    public string? Label { get; set; }
    public LanguageSet(string? label = null)
    {
        Label = label;
        for(int i = 'a'; i<='z'; i++)
        {
            Add((char)i, 0);
        }
    }

    public LanguageSet Absorb(string searched)
    {
        searched = searched.ToLower();
        foreach (var (key, value) in this)
        {
            this[key] = searched.Count(l => l == key);
        }

        var total = this.Sum(p => p.Value);
        this.ToList().ForEach(p => this[p.Key] = p.Value/total);
        
        return this;
    }

    public IEnumerable<double> Vector()
    {
        return this.Select(pair => pair.Value);
    }

    public IAssignable Assignable()
    {
        return new Observation()
        {
            Attributes = Vector(),
            DecisionAttribute = Label,
        };
    }

}