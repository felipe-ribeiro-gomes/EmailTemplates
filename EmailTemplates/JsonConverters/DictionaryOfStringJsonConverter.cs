using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmailTemplates.JsonConverters
{
    public class DictionaryOfStringJsonConverter : JsonConverter<IDictionary<string, string>>
    {
        public override IDictionary<string, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException($"Precisa ser um dicionário para o atributo funcionar");

            var dictionary = new Dictionary<string, string>();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException($"Dentro da JSON Array precisa ter um objeto. Formato \"key\": \"value\"");

                var key = string.Empty;
                var value = string.Empty;
                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                        key = reader.GetString();
                    if (reader.TokenType == JsonTokenType.String)
                        value = reader.GetString();
                }
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<string, string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
