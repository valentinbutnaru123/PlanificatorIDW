using System.Collections.Generic;

namespace Domain.Core
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<PresentationTag> PresentationTags { get; set; }
    }
}