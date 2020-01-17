using Application.Managers;
using Domain.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.Persistence;
using Planificator.Tests.PresentationTestData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Persistence.Tests
{
    public class PresentationRepositoryTests
    {
        [Fact]
        public async Task GetAllTags_returns_all_tags_from_one_presentationAsync()
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
                    var testData = new PresentationRepositoryTestsData();

                    await service.AddPresentation(testData.presentationTags);

                    context.SaveChanges();

                    Assert.Equal(testData.presentationTags.Count(), context.PresentationTags.Count());
                    Assert.Equal(1, context.Presentations.Count());
                    Assert.Equal(testData.tags.Count(), context.Tags.Count());

                    Assert.Equal(testData.presentationTags, context.PresentationTags);
                    Assert.Equal(testData.presentation.ToString(), context.Presentations.Single().ToString());
                    Assert.Equal(testData.tags, context.Tags);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetAllTags_returns_empty_if_thereAreNoTags()
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

                    var service = new PresentationRepository(context);

                    Assert.Empty(service.GetAllTagsNames(0));
                    Assert.Empty(service.GetAllTagsNames(1));
                    Assert.Empty(service.GetAllTagsNames(2));
                    Assert.Empty(service.GetAllTagsNames(3));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetAllPresentations_returns_empty_if_thereAreNoPresentations()
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

                    var query = new PresentationRepository(context);

                    Assert.Empty(query.GetAllPresentations());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetAllPresentations_returns_all_presentationsAsync()
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

                    var testData1 = new PresentationRepositoryTestsData();
                    var testData2 = new PresentationRepositoryTestsData();
                    var testData3 = new PresentationRepositoryTestsData();

                    List<Presentation> presentations = new List<Presentation>();
                    presentations.Add(testData1.presentation);
                    presentations.Add(testData2.presentation);
                    presentations.Add(testData3.presentation);

                    await service.AddPresentation(testData1.presentationTags);
                    await service.AddPresentation(testData2.presentationTags);
                    await service.AddPresentation(testData3.presentationTags);

                    context.SaveChanges();

                    Assert.Equal(context.Presentations.Count(), query.GetAllPresentations().Count());
                    Assert.Equal(presentations, query.GetAllPresentations());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetPresentationsCount_returns_presentations_countAsync()
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

                    var testData1 = new PresentationRepositoryTestsData();
                    var testData2 = new PresentationRepositoryTestsData();
                    var testData3 = new PresentationRepositoryTestsData();

                    List<Presentation> presentations = new List<Presentation>();
                    presentations.Add(testData1.presentation);
                    presentations.Add(testData2.presentation);
                    presentations.Add(testData3.presentation);

                    await service.AddPresentation(testData1.presentationTags);
                    await service.AddPresentation(testData2.presentationTags);
                    await service.AddPresentation(testData3.presentationTags);

                    context.SaveChanges();

                    Assert.Equal(presentations.Count(), query.GetPresentationCount());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}