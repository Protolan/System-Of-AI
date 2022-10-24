using CsvHelper.Configuration.Attributes;

namespace TreeC4_5.Data;

public class RockScissorsPaperSample: ISampleProvider
{
    public string PLAYER1_1 { get; set; }
    public string PLAYER2_1 { get; set; }
    public string PLAYER1_2 { get; set; }
    public string PLAYER2_2 { get; set; }
    public string PLAYER1_3 { get; set; }
    public string PLAYER2_3 { get; set; }
    public string CLASS { get; set; }
    
    
    public Sample GetSample()
    {
        var signs = new []
        {
            new Sign(nameof(PLAYER1_1), PLAYER1_1),
            new Sign(nameof(PLAYER2_1), PLAYER2_1),
            new Sign(nameof(PLAYER1_2), PLAYER1_2),
            new Sign(nameof(PLAYER2_2), PLAYER2_2),
            new Sign(nameof(PLAYER1_3), PLAYER1_3),
            new Sign(nameof(PLAYER2_3), PLAYER2_3),
        };
        return new Sample(signs, CLASS);
    }
}