
namespace Demo.API.Models
{
    public record Employee
    {
        public int Id { get; init; }
        public string fullname { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public int age { get; set; }
        public string birthdate { get; set; } = string.Empty;

    }
}