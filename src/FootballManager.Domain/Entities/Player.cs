using FootballManager.Domain.ValueObjects;

namespace FootballManager.Domain.Entities
{
    public class Player : EntityBase
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public double Height { get; set; }

        public EmailAddress EmailAddress { get; set;}

        public long? TeamId { get; set; }

        public Team Team { get; set; }
    }
}
