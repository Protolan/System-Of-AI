namespace TreeC4_5.Data
{
    public class C4_5
    {
        private readonly Sample[] _samples;
        private readonly Dictionary<string, HashSet<string>> SignsValues;
        public C4_5(Sample[] samples)
        {
            _samples = samples;
            SignsValues = GetSignsValues();
        }

        public Tree Build()
        {
            Console.WriteLine("BeginBuilding");
            // foreach (var value in SignsValues)
            // {
            //     Console.Write(value.Key + ": ");
            //     foreach (var values in SignsValues[value.Key]) 
            //         Console.Write(values + ", ");
            //     Console.WriteLine();
            // }
            return new Tree
            {
                Root = CalculateNode(_samples, new HashSet<string>())
            };
        }

        private Tree.Node CalculateNode(Sample[] sample, HashSet<string> notInc)
        {
            var nodeName = FindPrioritySign(_samples, notInc);
            var node = new Tree.Node();
            if (nodeName == null) return node;
            notInc.Add(nodeName);
            node.Value = nodeName;
            node.Transitions = new List<Tree.NodeTransition>();
            foreach (var value in SignsValues[nodeName])
            {
                var transition = new Tree.NodeTransition();
                transition.TransitionValue = value;
                transition.Node = CalculateNode(SamplesWithSignValue(sample, nodeName, value), notInc.ToHashSet());
                // notInc.Add(transition.Node.Value);
                node.Transitions.Add(transition);
            }

            return node;
        }

        private Dictionary<string, HashSet<string>> GetSignsValues()
        {
            var allValues = new Dictionary<string, HashSet<string>>();
            var allSigns = _samples.Select(sa => sa.Signs);
            foreach (var sign in allSigns)
            {
                foreach (var si in sign)
                {
                    if (!allValues.ContainsKey(si.Name)) 
                        allValues.Add(si.Name, new HashSet<string>());
                    allValues[si.Name].Add(si.Value);
                }
            }
            return allValues;
        }
        
        public string? FindPrioritySign(Sample[] samples, HashSet<string> notIn)
        {
            return SignsValues.Keys.Where(si => !notIn.Contains(si)).MaxBy(si => GainRation(samples, si));
        }

        private static Sample[] SamplesWithSignValue(Sample[] samples, string signName, string signValue) => 
            samples.Where(sa => sa.Signs.Any(si => si.Name == signName && si.Value == signValue)).ToArray();

        private static double Entropy(Sample[] samples)
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

        private static double ConditionalEntropy(Sample[] samples, string signName)
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
        

        private static double GainRation(Sample[] samples, string signName) => (Entropy(samples) - ConditionalEntropy(samples, signName)) / SplitInfo(samples, signName);

        private static double SplitInfo(Sample[] samples, string signName)
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