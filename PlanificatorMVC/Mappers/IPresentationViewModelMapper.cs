using Domain.Core;
using PlanificatorMVC.Models;
using System.Collections.Generic;

namespace PlanificatorMVC.Mappers
{
    public interface IPresentationViewModelMapper
    {

        PresentationViewModel MapPresentationToPresentationViewModel(Presentation presentation);

        IEnumerable<PresentationViewModel> MapFromPresentations(IEnumerable<Presentation> presentations);

        ICollection<PresentationTag> MapToPresentationTag(PresentationViewModel presentationViewModel, SpeakerProfile presentationOwner);
    }
}