using NAI3_Network.Models;

namespace NAI3_Network.Services;

public static class DataLoader
{
    public static IEnumerable<LanguageSet> Load(string dirPath)
    {
        return Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories).ToList()
            .Select(file => new LanguageSet(Directory.GetParent(file).Name).Absorb(File.ReadAllText(file).ToLower()));
    }
}