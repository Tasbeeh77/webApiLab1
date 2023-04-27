using webApiDay1.Validation;

namespace webApiDay1.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        [DateInPast]
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        public string Type { get; set; }
    }
}
