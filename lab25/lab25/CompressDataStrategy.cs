using System.Text;

namespace lab25;

public sealed class CompressDataStrategy : IDataProcessorStrategy
{
    public string Name => "Compress(SimpleRLE)";

    // Просте RLE (не “реальне” gzip, але для лабораторної ок)
    public string Process(string data)
    {
        data ??= "";
        if (data.Length == 0) return "";

        var sb = new StringBuilder();
        int count = 1;

        for (int i = 1; i <= data.Length; i++)
        {
            if (i < data.Length && data[i] == data[i - 1])
            {
                count++;
            }
            else
            {
                sb.Append(data[i - 1]);
                sb.Append(count);
                count = 1;
            }
        }

        return sb.ToString();
    }
}