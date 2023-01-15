namespace CommonInfrastructure.Http
{
    public class CommonHttpResponse
    {
        public bool Success { get; set; }

        public string[] Errors { get; set; }
    }
    public class CommonHttpResponse<T> : CommonHttpResponse
    {
        public T Data { get; set; }
    }
}
