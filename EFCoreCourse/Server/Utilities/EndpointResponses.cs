namespace EFCoreCourse.Server.Utilities
{
    public class EndpointResponses
    {
        public class ResponseWithSimpleMessage
        {
            public string Message { get; set; } 
            public static ResponseWithSimpleMessage Create(string message) => new() { Message = message };
        }
    }
}
