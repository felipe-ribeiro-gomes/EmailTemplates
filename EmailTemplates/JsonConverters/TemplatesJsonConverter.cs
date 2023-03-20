using EmailTemplates.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmailTemplates.JsonConverters
{
    public class TemplatesJsonConverter : JsonConverter<ICollection<Db.Template>>
    {
        public override ICollection<Db.Template> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException($"Não é uma array de templates");

            var collection = new List<Db.Template>();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException($"Está fora do formato de template");

                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    var id = string.Empty;
                    if (reader.TokenType == JsonTokenType.PropertyName)
                        id = reader.GetString();

                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                        {
                            JsonSerializer.Deserialize<>(reader)
                        }
                    }

                    var template = new Db.Template();

                    collection.Add(new Db.Template());
                }

                //var key = string.Empty;
                //var value = string.Empty;
                //while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                //{
                //    if (reader.TokenType == JsonTokenType.PropertyName)
                //        key = reader.GetString();
                //    if (reader.TokenType == JsonTokenType.String)
                //        value = reader.GetString();
                //}
            }

            return collection;
        }

        public override void Write(Utf8JsonWriter writer, ICollection<Db.Template> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
