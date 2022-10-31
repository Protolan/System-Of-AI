using TreeC4_5.Data;

var samples = SamplesReader.ReadFrom("C:\\Users\\alang\\Desktop\\Универ\\ИИ\\TreeC4_5\\System-Of-AI\\TreeC4_5\\RSP.csv");
var tree = new C4_5(samples).Build();
tree.PrintTree();