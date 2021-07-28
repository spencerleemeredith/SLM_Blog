﻿using System.Threading.Tasks;
using FluentAssertions;
using LinkDotNet.Blog.TestUtilities;
using LinkDotNet.Infrastructure.Persistence.InMemory;
using Xunit;

namespace LinkDotNet.Blog.UnitTests.Infrastructure.Persistence.InMemory
{
    public class ProfileRepositoryTests
    {
        private readonly ProfileRepository profileRepository;

        public ProfileRepositoryTests()
        {
            profileRepository = new ProfileRepository();
        }

        [Fact]
        public async Task ShouldSaveAndRetrieveAllEntries()
        {
            var item1 = new ProfileInformationEntryBuilder().WithContent("key1").Build();
            var item2 = new ProfileInformationEntryBuilder().WithContent("key2").Build();
            await profileRepository.StoreAsync(item1);
            await profileRepository.StoreAsync(item2);

            var items = await profileRepository.GetAllAsync();

            items[0].Content.Should().Be("key1");
            items[1].Content.Should().Be("key2");
        }

        [Fact]
        public async Task ShouldDelete()
        {
            var item1 = new ProfileInformationEntryBuilder().WithContent("key1").Build();
            var item2 = new ProfileInformationEntryBuilder().WithContent("key2").Build();
            await profileRepository.StoreAsync(item1);
            await profileRepository.StoreAsync(item2);

            await profileRepository.DeleteAsync(item1.Id);

            var items = await profileRepository.GetAllAsync();
            items.Should().HaveCount(1);
            items[0].Id.Should().Be(item2.Id);
        }

        [Fact]
        public async Task NoopOnDeleteWhenEntryNotFound()
        {
            var item = new ProfileInformationEntryBuilder().WithContent("key1").Build();
            await profileRepository.StoreAsync(item);

            await profileRepository.DeleteAsync("SomeIdWhichHopefullyDoesNotExist");

            (await profileRepository.GetAllAsync()).Should().HaveCount(1);
        }
    }
}