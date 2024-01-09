using RemnantKit.Saves;

namespace RemnantKit.Tools.UnSav;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var filePath = args[0];
        var output = filePath + ".un";

        await using var outFile = File.Open(output,
            new FileStreamOptions
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
                Options = FileOptions.SequentialScan,
            });
        await CompressedSaveReader.DecompressAsync(args[0], outFile);
    }
}
