﻿using System.Data;
using Dapper;

namespace TinyHelpers.Dapper.TypeHandlers;

public class StringArrayTypeHandler : SqlMapper.TypeHandler<string[]>
{
    private readonly string separator;

    public StringArrayTypeHandler(string separator = ";")
    {
        this.separator = separator;
    }

    public override string[] Parse(object value)
    {
        var content = value.ToString()!;
        return content.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }

    public override void SetValue(IDbDataParameter parameter, string[] value)
    {
        var content = string.Join(separator, value);
        parameter.Value = content;
    }

    public static void Configure(string separator = ";")
        => SqlMapper.AddTypeHandler(new StringArrayTypeHandler(separator));
}
