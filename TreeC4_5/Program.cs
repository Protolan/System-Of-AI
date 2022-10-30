using TreeC4_5.Data;

var samples = SamplesReader.ReadFrom("C:\\Users\\alang\\Desktop\\Учеба\\СИИ\\System-Of-AI\\TreeC4_5\\RSP.csv");
var algo = new C4_5(samples);
var root = algo.FindPrioritySign(samples, new List<string> {"PLAYER2_1"});

Console.WriteLine(root.Name);
