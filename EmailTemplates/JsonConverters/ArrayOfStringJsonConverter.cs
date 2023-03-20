using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmailTemplates.JsonConverters
{
    public class ArrayOfStringJsonConverter : JsonConverter<ICollection<string>>
    {
        public override ICollection<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException($"Precisa ser um dicionário para o atributo funcionar");

            var list = new List<string>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var value = string.Empty;
                if (reader.TokenType == JsonTokenType.String)
                    list.Add(reader.GetString());
            }
            return list;
        }

        public override void Write(Utf8JsonWriter writer, ICollection<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
