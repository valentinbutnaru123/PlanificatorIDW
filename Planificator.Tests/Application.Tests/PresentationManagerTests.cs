using Application.Managers;
using Domain.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.Persistence;
using Planificator.Tests.PresentationTestData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Application.Tests
{
    public class PresentationManagerTests
    {
        [Fact]
        public async Task AddPresentation_writes_to_databaseAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PlanificatorDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new PlanificatorDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var service = new PresentationManager(context);
                    var query = new PresentationRepository(context);

                    var testData = new PresentationRepositoryTestsData();

                    await service.AddPresentation(testData.presentationTags);

                    context.SaveChanges();

                    List<string> tagsNames = testData.tags.Select(tag => tag.TagName).ToList();

                    Assert.Equal(tagsNames.Count, query.GetAllTagsNames(testData.presentation.PresentationId).Count());
                    Assert.Equal(tagsNames, query.GetAllTagsNames(testData.presentation.PresentationId));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task AssignSpeakerToPresentation_writes_to_databaseAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            SpeakerProfile speaker = new SpeakerProfile
            {
                SpeakerId = Guid.NewGuid().ToString(),
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Email = "test@email.net",
                Bio = "My test bio",
                Company = "Test company",
                PhotoPath = "Test Path"
            };

            try
            {
                var options = new DbContextOptionsBuilder<PlanificatorDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new PlanificatorDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var presentationService = new PresentationManager(context);
                    var speakerServie = new SpeakerManager(context);

                    var testData = new PresentationRepositoryTestsData();

                    await speakerServie.AddSpeakerProfileAsync(speaker);
                    await presentationService.AddPresentation(testData.presentationTags);
                    await presentationService.AssignSpeakerToPresentationAsync(speaker, testData.presentation);

                    context.SaveChanges();

                    Assert.Equal(1, context.PresentationSpeakers.Count());
                    Assert.Equal(speaker, context.PresentationSpeakers.Single().SpeakerProfile);
                    Assert.Equal(testData.presentation, context.PresentationSpeakers.Include(p => p.Presentation).Single().Presentation);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}