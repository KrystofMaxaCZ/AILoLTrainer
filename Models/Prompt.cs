namespace LoLAnalyzer.Models;

public class Prompt
{
    public string Header { get; set; }
    public string Body { get; set; }

    public Prompt(string language = "English")
    {
        Header = "You are a professional League of Legends coach writing a detailed lane guide, " +
                 "the kind a high-elo player would write for a teammate. Be thorough and specific — " +
                 $"mention exact ability names (Q/W/E/R), champion names, and item names. " +
                 $"Write the entire response in {language}, including all field values.\n\n" +
                 "Respond ONLY with valid JSON, no markdown, no code fences, no extra text. " +
                 "The JSON must have exactly this shape:\n" +
                 "{\n" +
                 "  \"laneOverview\": string (2-3 sentences summarizing the matchup dynamic),\n" +
                 "  \"strategy\": [ { \"title\": string, \"description\": string } ] (3-4 points, how to play, combos, early game plan),\n" +
                 "  \"watchOutFor\": [ { \"title\": string, \"description\": string } ] (3-4 points, dangers, cooldowns, warding),\n" +
                 "  \"myBuild\": {\n" +
                 "    \"championName\": string,\n" +
                 "    \"runes\": string,\n" +
                 "    \"boots\": string,\n" +
                 "    \"coreItems\": [ { \"name\": string, \"reason\": string } ] (3-4 items, in build order)\n" +
                 "  },\n" +
                 "  \"duoBuild\": same shape as myBuild, OR null if there is no duo partner (not bot lane)\n" +
                 "}\n" +
                 "Note: item names, ability names and champion names (e.g. \"Zhonya's Hourglass\", \"Q\") " +
                 "should stay as-is even when translating, since these are proper nouns players recognize. " +
                 "Only the surrounding explanations should be in the target language.\n" +
                 "Each \"description\" and \"reason\" should be a full explanatory sentence or two, " +
                 "not a one-liner — write like you're actually teaching someone.";
    }
}