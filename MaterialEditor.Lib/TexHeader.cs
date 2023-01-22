using static SuperBMDLib.Materials.BinaryTextureImage;

namespace MaterialEditor.Lib
{
    public record struct TexHeader
    {
        public string Name;
        public TextureFormats Format;
        public int AlphaSetting;
        public WrapModes WrapS;
        public WrapModes WrapT;
        public PaletteFormats PaletteFormat;
        public int MipMap;
        public bool EdgeLOD;
        public bool BiasClamp;
        public int MaxAniso;
        public FilterMode MinFilter;
        public FilterMode MagFilter;
        public double MinLOD;
        public double MaxLOD;
        public double LodBias;

        public static TexHeaderFile GetTexHeaders(JArray arr) => (new(arr.ToObject<TexHeader?[]>()), arr);

        public static TexHeaderFile GetTexHeaders(string text) => GetTexHeaders(JArray.Parse(text));

        public static TexHeaderFile GetTexHeaders(StreamReader sr) => GetTexHeaders(sr.ReadToEnd());

        public static TexHeaderFile GetTexHeaders(FileInfo file) => GetTexHeaders(File.ReadAllText(file.FullName));
    }
}
