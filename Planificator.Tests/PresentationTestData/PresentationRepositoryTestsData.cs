using Domain.Core;
using System;
using System.Collections.Generic;

namespace Planificator.Tests.PresentationTestData
{
    public class PresentationRepositoryTestsData
    {
        public PresentationRepositoryTestsData()
        {
            foreach (Tag tag in tags)
            {
                presentationTags.Add(new PresentationTag { Tag = tag, Presentation = presentation });
            }
        }

        public List<Tag> tags = new List<Tag>()
        {
                new Tag { TagName = "AA" },
                new Tag { TagName = "BB" },
                new Tag { TagName = "CC" }
        };

        public Presentation presentation = new Presentation
        {
            Title = "Test",
            LongDescription = "Test",
            ShortDescription = "Test",
            PresentationOwner = new SpeakerProfile { SpeakerId = Guid.NewGuid().ToString(), FirstName = "a", LastName = "b", Email = "c", Bio = "b", PhotoPath = "TEST" }
        };

        public List<PresentationTag> presentationTags = new List<PresentationTag>();
    }
}