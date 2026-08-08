namespace LoLAnalyzer.Models;

public class UserSelection
{
    public LaneRole SelectedLane { get; set; } = LaneRole.Mid;
    public string ResponseLanguage { get; set; } = "English";

    public string MyChampion { get; set; } = "";
    public string OpponentChampion { get; set; } = "";

    public string MyDuoChampion { get; set; } = "";
    public string OpponentDuoChampion { get; set; } = "";
}