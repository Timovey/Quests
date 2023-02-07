
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerSchema(ReadOnly = true)]
        public Guid? RequestId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? RequestUserId { get; set; } = null;

        [SwaggerSchema(ReadOnly = true)]
        public string? RequestUserName { get; set; } = null;
    }

}
