namespace TreeC4_5.Data
{
    public class C4_5
    {
        private readonly Sample[] _samples;

        public C4_5(Sample[] samples) => _samples = samples;

        public void Build()
        {
            var root = FindPrioritySign(_samples, new List<string>());
        }

        public Sign FindPrioritySign(Sample[] samples, List<string> notIn) => 
            samples[0].Signs.Where(p => !notIn.Contains(p.Name)).OrderByDescending(i => GainRation(samples, i.Name)).First();

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

        private double ConditionalEntropy(Sample[] samples, string signName)
        {
            var samplesWithSign = samples.ToDictionary(sa => sa.Signs.First(si => si.Name == signName));
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
    
    

        private double GainRation(Sample[] samples, string signName) => (Entropy(samples) - ConditionalEntropy(samples, signName)) / SplitInfo(samples, signName);

        private double SplitInfo(Sample[] samples, string signName)
        {
            var samplesWithSign = samples.ToDictionary(sa => sa.Signs.First(si => si.Name == signName));
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
}