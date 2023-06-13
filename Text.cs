namespace ParseDontValidate;

public class Text
{
    private readonly string value;

    private Text(string value)
    {
        this.value = value;
    }
    public static Option<Text> TryParse(string textCandidate)
    {
        return string.IsNullOrWhiteSpace(textCandidate) 
            ? F.None 
            : F.Some(new Text(textCandidate));
    }

    public override string ToString()
    {
        return this.value;
    }
}