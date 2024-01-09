using System.Diagnostics;
using System.IO.Compression;
using JetBrains.Annotations;

namespace RemnantKit.Saves;

[UsedImplicitly]
internal record struct FileHeader(Memory<byte> Crc32, int DecompressedSize, int Unknown)
{
    internal static FileHeader Read(BinaryReader reader) =>
        new(reader.ReadBytes(count: 4), reader.ReadInt32(), reader.ReadInt32());
}

internal enum CompressionMethod : byte
{
    Zlib = 3,
}

[UsedImplicitly]
internal record struct ChunkHeader(
    Memory<byte> HeaderTag,
    long ChunkSize,
    CompressionMethod CompressionMethod,
    long CompressedSize1,
    long DecompressedSize1,
    long CompressedSize2,
    long DecompressedSize2)
{
    internal static ChunkHeader Read(BinaryReader reader) =>
        new(reader.ReadBytes(count: 8),
            reader.ReadInt64(),
            (CompressionMethod)reader.ReadByte(),
            reader.ReadInt64(),
            reader.ReadInt64(),
            reader.ReadInt64(),
            reader.ReadInt64());
}

[PublicAPI]
public class CompressedSaveReader
{
    [PublicAPI]
    public static Stream Decompress(string path, Stream destination)
    {
        using var fileStream = File.Open(path,
            new FileStreamOptions
            {
                Access = FileAccess.Read,
                Mode = FileMode.Open,
                Options = FileOptions.SequentialScan,
            });

        using var reader = new BinaryReader(fileStream);

        _ = FileHeader.Read(reader);

        while (fileStream.Position < fileStream.Length)
        {
            var chunkHeader = ChunkHeader.Read(reader);

            Debug.Assert(chunkHeader.CompressedSize1 == chunkHeader.CompressedSize2);
            Debug.Assert(chunkHeader.DecompressedSize1 == chunkHeader.DecompressedSize2);

            switch (chunkHeader.CompressionMethod)
            {
                case CompressionMethod.Zlib:
                {
                    using var zs = new ZLibStream(fileStream,
                        CompressionMode.Decompress,
                        leaveOpen: true);
                    zs.CopyTo(destination);
                    break;
                }
            }
        }

        return destination;
    }

    [PublicAPI]
    [MustUseReturnValue]
    public static Task DecompressAsync(string path, Stream destination) =>
        Task.Run(() => Decompress(path, destination));
}
