namespace PlanificatorMVC.Models
{
    public class PresentationViewModel
    {
        public int PresentationId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Tags { get; set; }
        public string AssignedSpeakersEmails { get; set; }
    }
}