namespace Web.Contracts.Common;
public sealed record class CodeListDto<TKey>(TKey Key, string Text);

public sealed record class CodeListDto(string Key, string Text);
