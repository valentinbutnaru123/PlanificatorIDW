using System.Collections.Generic;

namespace Domain.Core
{
    public class Presentation
    {
        public int PresentationId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public virtual ICollection<PresentationTag> PresentationTags { get; set; }
        public virtual ICollection<PresentationSpeaker> PresentationSpeakers { get; set; }
        public virtual SpeakerProfile PresentationOwner { get; set; }
    }
}