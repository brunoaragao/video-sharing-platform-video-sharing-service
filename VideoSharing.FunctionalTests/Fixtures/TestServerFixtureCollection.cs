// <copyright file="TestServerFixtureCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AluraChallenge.VideoSharingPlatform.Services.VideoSharing.FunctionalTests;

/// <summary>
/// Represents the test server fixture collection.
/// </summary>
[CollectionDefinition(nameof(TestServerFixtureCollection))]
public class TestServerFixtureCollection : ICollectionFixture<TestServerFixture>
{
}