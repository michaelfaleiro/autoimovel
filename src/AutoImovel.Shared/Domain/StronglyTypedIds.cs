using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoImovel.Shared.Domain;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<InvestidorId>))]
public readonly record struct InvestidorId(Guid Value)
{
    public static InvestidorId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

[JsonConverter(typeof(StronglyTypedIdJsonConverter<AporteId>))]
public readonly record struct AporteId(Guid Value)
{
    public static AporteId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

[JsonConverter(typeof(StronglyTypedIdJsonConverter<VeiculoId>))]
public readonly record struct VeiculoId(Guid Value)
{
    public static VeiculoId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

[JsonConverter(typeof(StronglyTypedIdJsonConverter<AlocacaoLastroId>))]
public readonly record struct AlocacaoLastroId(Guid Value)
{
    public static AlocacaoLastroId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

[JsonConverter(typeof(StronglyTypedIdJsonConverter<FechamentoLucroId>))]
public readonly record struct FechamentoLucroId(Guid Value)
{
    public static FechamentoLucroId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class StronglyTypedIdJsonConverter<T> : JsonConverter<T> where T : struct
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guid = reader.GetGuid();
        return (T)Activator.CreateInstance(typeof(T), guid)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var prop = typeof(T).GetProperty("Value");
        if (prop is not null)
            writer.WriteStringValue(((Guid)prop.GetValue(value)!).ToString("D"));
    }
}
