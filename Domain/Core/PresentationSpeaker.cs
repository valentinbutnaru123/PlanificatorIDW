namespace Domain.Core
{
    public class PresentationSpeaker
    {
        public int PresentationId { get; set; }
        public virtual Presentation Presentation { get; set; }
        public string SpeakerId { get; set; }
        public virtual SpeakerProfile SpeakerProfile { get; set; }
    }
}