namespace Material.Creator;

public static class Extensions
{
    public static string NameWithoutExt(this FileInfo file)
    {
        return Path.GetFileNameWithoutExtension(file.Name);
    }
}
