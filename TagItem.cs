using System.Collections.Generic;

public class TagItem
{
    public string group { get; set; }

    public string tag { get; set; }

    public List<CharacterItem> characters { get; set; }

    public List<FixedTagItem> Fixedtags { get; set; }
}

public class CharacterItem
{
    public string tag { get; set; }
}

public class FixedTagItem
{
    public string tag { get; set; }
}