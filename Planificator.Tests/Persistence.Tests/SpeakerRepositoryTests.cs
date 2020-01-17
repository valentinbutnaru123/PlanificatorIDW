using Application.Managers;
using Domain.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Persistence.Tests
{
    public class SpeakerRepositoryTests
    {
        [Fact]
        public async Task GetAllSpeakerProfile_returns_all_speaker_profilesAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            List<SpeakerProfile> speakerProfiles = new List<SpeakerProfile>
            {
                new SpeakerProfile
                {
                    SpeakerId = Guid.NewGuid().ToString(),
                    FirstName = "Test FN",
                    LastName = "Test LN",
                    Email = "test@test.test",
                    Bio = "Test Bio",
                    Company = "Test Compnay",
                    PhotoPath = "testPath.jpg"
                },
                new SpeakerProfile
                {
                    SpeakerId = Guid.NewGuid().ToString(),
                    FirstName = "Test2 FN",
                    LastName = "Test2 LN",
                    Email = "test2@test.test",
                    Bio = "Test2 Bio",
                    Company = "Test2 Compnay",
                    PhotoPath = "test2Path.jpg"
                }
            };

            try
            {
                var options = new DbContextOptionsBuilder<PlanificatorDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new PlanificatorDbContext(options))
                {
                    context.Database.EnsureCreated();

                    var service = new SpeakerManager(context);
                    var query = new SpeakerRepository(context);

                    foreach (SpeakerProfile speakerProfile in speakerProfiles)
                    {
                        await service.AddSpeakerProfileAsync(speakerProfile);
                    }

                    context.SaveChanges();
                    Assert.Equal(speakerProfiles, await query.GetAllSpeakersProfilesAsync());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task GetAllSpeakerProfile_ReturnsEmpty_IfSpeakerProfileEntity_IsEmptyAsync()
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

                    var query = new SpeakerRepository(context);

                    Assert.Empty(await query.GetAllSpeakersProfilesAsync());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}