using GenerateQuestsService.DataContracts.DataContracts;
using GenerateQuestsService.DataContracts.Enums;
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace GenerateQuestsService.Main.DeserializationHelpers
{
    public class StageJsonConverterHelper<T> : JsonConverter<T> where T : Stage
    {
        private readonly IEnumerable<Type> _types;

        public StageJsonConverterHelper()
        {
            //получаем все типы классов-наследников от Stage
            //они должны быть не абстрактными (иметь конструктор)
            var type = typeof(T);
            _types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
        }

        //переопределенный метод,
        //описывающий возможность этого конвертера принять этот тип
        //чтобы не было зацикливания - проверяем,
        //не является ли базовый тип Stage 
        //!typeToConvert.BaseType.Equals(typeof(Stage))
        public override bool CanConvert(Type typeToConvert) =>
            typeof(Stage).IsAssignableFrom(typeToConvert) 
            && !typeToConvert.BaseType.Equals(typeof(Stage));

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                //если не начало объекта
                if (reader.TokenType != JsonTokenType.StartObject)
                    throw new JsonException();
                //парсим в документ json
                using (var jsonDocument = JsonDocument.ParseValue(ref reader))
                {
                    //получаем название свойста, которое является дескриптором
                    string decriptorName = nameof(Stage.Type).ToLower();
                    //если нет такого свойства в json кидаем ошибку
                    if (!jsonDocument.RootElement.TryGetProperty(decriptorName, out var typeProperty))
                        throw new JsonException();

                    //процесс получения типа, к которому мы должны преобразовать
                    //сначала получаем значение enum и его строковое представление
                    var enumName = ((StageType)typeProperty.GetByte()).ToString();

                    var type = _types.FirstOrDefault(x => x.Name.Contains(enumName));
                    if (type == null)
                        throw new JsonException();
                    var jsonString = jsonDocument.RootElement.GetRawText();

                    //var jsonObject = (T)JsonSerializer.Deserialize(jsonString, type, options)!;
                    return (T)JsonSerializer.Deserialize(jsonString, type, options)!;
                }
            }  
            catch(Exception ex)
            {
                throw new NotSupportedException(ex.Message);
            }
         }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }
    }
}
