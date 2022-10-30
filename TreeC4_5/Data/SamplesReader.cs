using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace TreeC4_5.Data
{
    public static class SamplesReader
    {
        public static Sample[] ReadFrom(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<RockScissorsPaperSample>();
            return records.Select(r => r.GetSample()).ToArray();
        }
    }
}