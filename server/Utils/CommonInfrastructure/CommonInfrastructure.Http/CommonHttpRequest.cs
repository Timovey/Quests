
using System.Text.Json.Serialization;

namespace CommonInfrastructure.Http
{
    /// <summary>
    /// Общий вид HTTP запроса
    /// </summary>
    public class CommonHttpRequest
    {
        public CommonHttpRequest()
        {
            RequestId = Guid.NewGuid();
        }
        [JsonIgnore]
        public Guid? RequestId { get; set; }

        [JsonIgnore]
        public string? RequestUserId { get; set; } = null;

        [JsonIgnore]
        public string? RequestUserName { get; set; } = null;
    }

}
