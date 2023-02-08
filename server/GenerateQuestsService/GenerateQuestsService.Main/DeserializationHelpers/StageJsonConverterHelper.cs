using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.DataContracts.Stages;
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace GenerateQuestsService.Main.DeserializationHelpers
{
    public class StageJsonConverterHelper<T> : JsonConverter<T> where T : BaseStage
    {
        private readonly IEnumerable<Type> _types;

        public StageJsonConverterHelper()
        {
            var type = typeof(T);
            _types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
        }

        enum TypeDiscriminator
        {
            A = 1,
            B = 2
        }
        public override bool CanConvert(Type typeToConvert) =>
            typeof(Base).IsAssignableFrom(typeToConvert);

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                string decriptorName = nameof(Base.Type).ToLower();
                if (!jsonDocument.RootElement.TryGetProperty(decriptorName, out var typeProperty))
                    throw new JsonException();

                foreach(var y in _types)
                {

                }
                var type = _types.FirstOrDefault(x => x.Name == typeProperty.GetString());
                if (type == null)
                    throw new JsonException();
                var jsonString = jsonDocument.RootElement.GetRawText();
                var jsonObject = (T)JsonSerializer.Deserialize(jsonString, type, options);
                return jsonObject;
            }
            //if (reader.TokenType != JsonTokenType.StartObject)
            //{
            //    throw new JsonException();
            //}
            //string? propertyName = null;
            //while (reader.Read())
            //{
            //    if (reader.TokenType == JsonTokenType.PropertyName)
            //    {
            //        propertyName = reader.GetString();
            //        if (propertyName == "type")
            //        {
            //            break;
            //        }
            //    }
            //}
            //if(propertyName == null) {
            //    throw new JsonException("Нет типа");
            //}
            //else
            //{
            //    reader.Read();
            //    if (reader.TokenType != JsonTokenType.Number)
            //    {
            //        throw new JsonException();
            //    }

            //    TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            //    Base person = typeDiscriminator switch
            //    {
            //        TypeDiscriminator.A => new A(),
            //        TypeDiscriminator.B => new B(),
            //        _ => throw new JsonException()
            //    };

            //    return new Base();
         }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }
    }
}
