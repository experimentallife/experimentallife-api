using System.Text;

namespace Project.Common.Extention;

public static partial class ExtObject
{
  public static byte[] ReadToBytes(this Stream stream)
  {
    stream.Seek(0, SeekOrigin.Begin);
    byte[] bytes = new byte[stream.Length];
    stream.Read(bytes, 0, bytes.Length);
    stream.Seek(0, SeekOrigin.Begin);

    return bytes;
  }

  public static string ReadToString(this Stream stream)
  {
    return ReadToString(stream, Encoding.UTF8);
  }

  public static string ReadToString(this Stream stream, Encoding encoding)
  {
    if (stream.CanSeek)
    {
      stream.Seek(0, SeekOrigin.Begin);
    }

    string resStr = string.Empty;
    resStr = new StreamReader(stream, encoding).ReadToEnd();

    if (stream.CanSeek)
    {
      stream.Seek(0, SeekOrigin.Begin);
    }

    return resStr;
  }
}