namespace TreeC4_5.Data;

public class C4_5
{
    private readonly Sample[] _samples;

    public C4_5(Sample[] samples) => _samples = samples;

    public void Build()
    {
        
    }

    private double Entropy(Sample[] samples)
    {
        var classes = samples.Select(s => s.Class).Distinct();
        double result = 0;
        foreach (var @class in classes)
        {
            var temp = samples.Count(s => s.Class == @class) / (double) samples.Length;
            result += temp * Math.Log2(temp);
        }
        return -result;
    }

    private double ConditionalEntropy(Sample[] samples, Sign sign)
    {
        var samplesWithSign = samples.ToDictionary(sa => sa.Signs.First(si => si.Name == sign.Name));
        var signValues = samplesWithSign.Select(s => s.Key.Value).Distinct();
        double result = 0;
        foreach (var signValue in signValues)
        {
            var filteredSample = samplesWithSign.Where(sa => sa.Key.Value == signValue);
            var temp = filteredSample.Count() / (double) samples.Length;
            result += temp * Entropy(filteredSample.Select(sa => sa.Value).ToArray());
        }
        return result;
    }
    
    

    private double GainRation(Sample[] samples, Sign sign) => (Entropy(samples) - ConditionalEntropy(samples, sign)) / SplitInfo(samples, sign);

    private double SplitInfo(Sample[] samples, Sign sign)
    {
        var samplesWithSign = samples.ToDictionary(sa => sa.Signs.First(si => si.Name == sign.Name));
        var signValues = samplesWithSign.Select(s => s.Key.Value).Distinct();
        double result = 0;
        foreach (var signValue in signValues)
        {
            var filteredSample = samplesWithSign.Where(sa => sa.Key.Value == signValue);
            var temp = filteredSample.Count() / (double) samples.Length;
            result += temp * Math.Log2(temp);
        }
        return -result;

    }
}