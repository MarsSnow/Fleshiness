using System.IO;

public static class ProtobufSerializeUtility 
{
    public static string SerializeObjectToStr(object obj)
    {
        byte[] bytesArray = SerializeObject(obj);
        return bytesArray.ToString();
    }

    public static T DeserializeObjectByStr<T>(string sourcStr)
    {
        byte[] bytesArray = System.Text.Encoding.Default.GetBytes(sourcStr);
        return DeserializeObject<T>(bytesArray);
    }

    public static byte[] SerializeObject(object obj)
    {
        byte[] bodys;
        using (MemoryStream ms = new MemoryStream())
        {
            ProtobufMng.instance.Serializer.Serialize(ms, obj);
            bodys = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(bodys, 0, bodys.Length);
            ms.Close();
        }
        return bodys;
    }

    public static T DeserializeObject<T>(byte[] bodys)
    {
        T t;
        using (MemoryStream ms = new MemoryStream(bodys))
        {
            t = (T)ProtobufMng.instance.Serializer.Deserialize(ms, null, typeof(T));
            ms.Close();
        }
        return t;
    }

}
