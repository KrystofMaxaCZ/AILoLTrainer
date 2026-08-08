namespace LoLAnalyzer.Models;

// A single bullet point with a bold title and explanation — e.g.
// Title: "Level 2 spike", Description: "Push the wave and fight for it first..."
public class TipPoint
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

// A single item in the build order, with a short reason why it's picked
public class ItemPick
{
    public string Name { get; set; } = "";
    public string Reason { get; set; } = "";
}

public class ChampionBuild
{
    public string ChampionName { get; set; } = "";
    public string Runes { get; set; } = "";
    public string Boots { get; set; } = "";
    public List<ItemPick> CoreItems { get; set; } = new();
}

public class AiTrainingResponse
{
    public string LaneOverview { get; set; } = "";       // short intro, 2-3 sentences
    public List<TipPoint> Strategy { get; set; } = new(); // "How to play"
    public List<TipPoint> WatchOutFor { get; set; } = new();
    public ChampionBuild MyBuild { get; set; } = new();
    public ChampionBuild? DuoBuild { get; set; }           // null unless Bot lane
}