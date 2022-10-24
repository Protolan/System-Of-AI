namespace TreeC4_5.Data;

public class Sample
{
    public Sign[] Signs { get; }
    public string Class { get; }

    public Sample(Sign[] signs, string @class)
    {
        Signs = signs;
        Class = @class;
    }

    public void Print()
    {
        foreach (var sign in Signs)
        {
            Console.Write(sign.Value + " ");
        }
        Console.Write(Class);
    }
}