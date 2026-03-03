using System.Text;

namespace lab25;

public sealed class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Name => "Encrypt(Base64)";

    public string Process(string data)
    {
        data ??= "";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
    }
}