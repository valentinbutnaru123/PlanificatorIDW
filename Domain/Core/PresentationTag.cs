namespace Domain.Core
{
    public class PresentationTag
    {
        public int PresentationId { get; set; }
        public virtual Presentation Presentation { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}