using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core
{
    public class SpeakerProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SpeakerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string PhotoPath { get; set; }
        public string Company { get; set; }
        public virtual ICollection<PresentationSpeaker> PresentationSpeakers { get; set; }
        public virtual ICollection<Presentation> OwnedPresentations { get; set; }
    }
}