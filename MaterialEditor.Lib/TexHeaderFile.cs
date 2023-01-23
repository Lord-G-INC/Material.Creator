namespace MaterialEditor.Lib;

public record struct TexHeaderFile
{
    [JsonIgnore]
    private JArray Array;

    public List<TexHeader> Headers;

    public int Length => Headers.Count;

    public TexHeader this[int index]
    {
        get => Headers[index];
        set => Headers[index] = value;
    }

    public static implicit operator TexHeaderFile((List<TexHeader> Headers, JArray Array) tup) =>
        new() { Array = tup.Array, Headers = tup.Headers};

    public static implicit operator TexHeaderFile(TEX1 tex)
    {
        return TexHeader.GetTexHeaders(JsonConvert.SerializeObject(tex.Textures));
    }

    public override string ToString()
    {
        Array = JArray.FromObject(Headers);
        return Array.ToString(Formatting.Indented);
    }
}