namespace Microservices.Services.Core.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
    }
}