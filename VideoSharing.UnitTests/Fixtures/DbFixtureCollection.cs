// <copyright file="DbFixtureCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.UnitTests.Fixtures;

/// <summary>
/// Represents the database fixture collection.
/// </summary>
[CollectionDefinition(nameof(DbFixtureCollection))]
public class DbFixtureCollection : ICollectionFixture<DbFixture>
{
}