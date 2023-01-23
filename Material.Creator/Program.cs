using Material.Creator;
using MaterialEditor.Lib;
using System.Diagnostics;

FileInfo[] valid = args.Select(x => new FileInfo(x)).Where(x => x.Exists).ToArray();

FileInfo[] materials = valid.Where(x => x.Name.Contains("materials")).ToArray();
FileInfo[] texs = valid.Where(x => x.Name.Contains("tex_headers")).ToArray();
MaterialFile[] matfiles = materials.Select(x => new MaterialFile(x)).ToArray();
TexHeaderFile[] texheaders = texs.Select(TexHeader.GetTexHeaders).ToArray();
MaterialFile mats = new()
{
    Materials = new()
};
TexHeaderFile headers = new()
{
    Headers = new()
};

string buf = string.Empty;

for (int i = 0; i < matfiles.Length; i++)
{
    var name = materials[i].NameWithoutExt();
    var idx = name.IndexOf('_');
    name = name[..idx];
    var mat = matfiles[i];
    buf = $"Select material(s) from {name}.\n";
    string[] names = mat.Materials.Select(x => x.Name).ToArray();
    for (int n = 0; n < names.Length; n++)
    {
        bool last = n == names.Length - 1;
        string term = last ? "" : "\n";
        buf += $"[{n}] {names[n]}{term}";
    }
    Console.WriteLine(buf);
    var line = Console.ReadLine() ?? string.Empty;
    int[] nums = line.Where(char.IsNumber).Select(x => int.Parse(x.ToString())).ToArray();
    foreach (var num in nums)
    {
        if (num > -1 && num < mat.Materials.Count)
            mats.Materials.Add(mat.Materials[num]);
    }
}

Trace.WriteLine("");

for (int i = 0; i < texs.Length; i++)
{
    var name = texs[i].NameWithoutExt();
    var idx = name.IndexOf('_');
    name = name[..idx];
    var tex = texheaders[i];
    buf = $"Select a TexHeader from {name}.\n";
    var names = tex.Headers.Select(x => x.Name).ToArray();
    for (int n = 0; n < names.Length; n++)
    {
        bool last = n == names.Length - 1;
        string term = last ? "" : "\n";
        buf += $"[{n}] {names[n]}{term}";
    }
    Console.WriteLine(buf);
    var line = Console.ReadLine() ?? string.Empty;
    int[] nums = line.Where(char.IsNumber).Select(x => int.Parse(x.ToString())).ToArray();
    foreach (var num in nums)
    {
        if (num > -1 && num < tex.Headers.Count)
            headers.Headers.Add(tex.Headers[num]);
    }
}

Trace.WriteLine("");

if (mats.Materials.Count > 0)
{
    Console.WriteLine("Type in a name for the materials.json file to be output.");
    var l = Console.ReadLine();
    string res = string.IsNullOrWhiteSpace(l) ? "out_materials.json" : l;
    FileInfo omat = new(res);
    File.WriteAllText(omat.FullName, mats.ToString());
}
if (headers.Headers.Count > 0)
{
    Console.WriteLine("Type in a name for the tex_headers.json file to be output.");
    var l = Console.ReadLine();
    string res = string.IsNullOrWhiteSpace(l) ? "out_tex_headers.json" : l;
    FileInfo tout = new(res);
    File.WriteAllText(tout.FullName, headers.ToString());
}