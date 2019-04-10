namespace Microservices.Services.Core.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public int CreatedOn { get; set; }
        public int UserId { get; set; }
    }
}