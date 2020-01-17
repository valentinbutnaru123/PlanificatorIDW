using Domain.Core;
using PlanificatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanificatorMVC.Mappers
{
    public class PresentationViewModelMapper : IPresentationViewModelMapper
    {
        public ICollection<PresentationTag> MapToPresentationTag(PresentationViewModel presentationViewModel, SpeakerProfile presentationOwner)
        {
            ICollection<string> tags = presentationViewModel.Tags.Split(" ");

            Presentation presentation = new Presentation()
            {
                Title = presentationViewModel.Title,
                ShortDescription = presentationViewModel.ShortDescription,
                LongDescription = presentationViewModel.LongDescription,
                PresentationOwner = presentationOwner
            };

            ICollection<PresentationTag> presentationTags = new List<PresentationTag>();

            foreach (var tagName in tags)
            {
                PresentationTag presentationTag = new PresentationTag()
                {
                    Presentation = presentation,
                    Tag = new Tag() { TagName = tagName }
                };
                presentationTags.Add(presentationTag);
            }
            return presentationTags;
        }

        public IEnumerable<PresentationViewModel> MapFromPresentations(IEnumerable<Presentation> presentations)
        {
            List<PresentationViewModel> presentationViewModels = new List<PresentationViewModel>();

            if (presentations.Count() == 0)
                return null;


            foreach (Presentation presentation in presentations)
            {
                presentationViewModels.Add
                (
                    new PresentationViewModel
                    {

                        PresentationId = presentation.PresentationId,
                        Title = presentation.Title,
                        ShortDescription = presentation.ShortDescription,
                        LongDescription = presentation.LongDescription,
                        Tags = makeTagsString(presentation.PresentationTags.Select(a => a.Tag).ToList())
                        //Tags = presentation.PresentationTags.Select(a => a.Tag.TagName).ToList().ToString().Replace(" ", "#").Insert(0, "#")
                    }
                );
            }
            return presentationViewModels;
        }

        private string makeTagsString(IEnumerable<Tag> tags)
        {
            string stringOfTags = "";
            foreach (Tag tag in tags)
            {
                stringOfTags = String.Concat(stringOfTags, " #", tag.TagName);
            }
            stringOfTags.Remove(0, 1);
            return stringOfTags;
        }


        public PresentationViewModel MapPresentationToPresentationViewModel(Presentation presentation)
        {
            var presentationViewModel = new PresentationViewModel
            {
                PresentationId = presentation.PresentationId,
                Title = presentation.Title,
                ShortDescription = presentation.ShortDescription,
                LongDescription = presentation.LongDescription,
                Tags = makeTagsString(presentation.PresentationTags.Select(a => a.Tag).ToList())
            };

            return presentationViewModel;
        }

    }
}