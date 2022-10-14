namespace Portfolio.WebApi.Commons
{
    public class Auditable:BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
