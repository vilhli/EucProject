namespace Web.Contracts.Common;
public sealed record class FileDto
{
	public string FileName { get; set; } = default!;

	public byte[] Content { get; set; } = [];
}
