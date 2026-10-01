using System.Collections.Generic;

public class TagItem
{
    public string group { get; set; }              // "Fixed" or "Tag" (only on group headers)
    public string tag { get; set; }                // "#Something" (only on leaf items)
    public List<TagItem> tags { get; set; }        // children (groups only)
    public List<CharacterItem> characters { get; set; } // characters (Tag items only)
}

public class CharacterItem
{
    public string tag { get; set; }
}