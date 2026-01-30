namespace WeatherLogger.Domain.Entities.Log
{
    public class ApiLog: BaseEntity
    {
        public string Endpoint { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public int StatusCode { get; set; }
    }
}
