using stashh_codegen;

if (FileSystem.OutputDirectoryExists())
{
    throw new Exception("Output directory already exists!");
}

var codes = CodeGenerator.Generate().ToList();

await FileSystem.WriteCodesToFile(codes);
await QRGenerator.Generate(codes);

