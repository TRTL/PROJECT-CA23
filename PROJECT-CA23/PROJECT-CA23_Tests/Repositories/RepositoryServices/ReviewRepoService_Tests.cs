using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Repositories.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROJECT_CA23.Models.Enums;

namespace PROJECT_CA23.Repositories.RepositoryServices_Tests
{
    [TestClass()]
    public class ReviewRepoService_Tests
    {
        [TestMethod()]
        public async Task AddReviewIfNeeded_AddsReview_WhenUserMediaHasNoReviewButHasReviewData()
        {
            // Arrange
            var reviewRepoMock = new Mock<IReviewRepository>();
            var reviewService = new ReviewRepoService(reviewRepoMock.Object);
            var userMedia = new UserMedia { UserMediaId = 1, Review = null };
            var updateRequest = new UpdateUserMediaRequest { UserMediaId = 1, UserMediaStatus = "Finished", UserRating = "FourStars", ReviewText = "Great Movie!", Note = "test notes" };

            // Act
            var result = await reviewService.AddReviewIfNeeded(userMedia, updateRequest);

            // Assert
            reviewRepoMock.Verify(x => x.CreateAsync(It.IsAny<Review>()), Times.Once());
            Assert.AreEqual(EUserRating.FourStars, result.Review.UserRating);
            Assert.AreEqual("Great Movie!", result.Review.ReviewText);
        }
    }
}