using Application.Data;
using Application.Posts.Create;
using Application.Posts.Get;
using Domain.Entities.Posts;
using Moq;
using NUnit.Framework;

namespace Application.Test.UnitTests
{
    [TestFixture]
    public class PostTests
    {
        private CreatePostCommandHandler postCreateCommandHandler;

        private GetPostQueryHandler getPostHandler;

        private SkipAndLimitPostQueryHandler skipAndLimitPostQueryHandler;

        private IUnitOfWork unitOfWork;

        private IPostRepository postRepository;


        [SetUp]
        public void Setup()
        {
            Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();

            mockPostRepository.Setup(t => t.Add(It.IsAny<Post>()));

            mockPostRepository.Setup(t => t.GetAllASync()).ReturnsAsync(
                new List<Post>
                {
                    new Post(Guid.Parse("4b00bb02-d740-44e4-8a9f-e495eeea695e"), "Title1", "Description1"),
                    new Post(Guid.Parse("968897b2-878d-4b5d-8092-0e0dce3ce449"), "Title2", "Description2")
                }
            );
            mockPostRepository.Setup(t => t.GetByIdASync(
                It.Is<Guid>((guid) =>
                    guid == Guid.Parse("4b00bb02-d740-44e4-8a9f-e495eeea695e"))))
                .ReturnsAsync(new Post(
                    Guid.Parse("4b00bb02-d740-44e4-8a9f-e495eeea695e"), "Title1", "Description1")
                );

            postRepository = mockPostRepository.Object;

            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(t => t.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            unitOfWork = mockUnitOfWork.Object;
            postCreateCommandHandler = new CreatePostCommandHandler(mockPostRepository.Object, mockUnitOfWork.Object);

            getPostHandler = new GetPostQueryHandler(mockPostRepository.Object);

            skipAndLimitPostQueryHandler = new SkipAndLimitPostQueryHandler(mockPostRepository.Object);
        }

        [Test]
        public async Task CreateAPost()
        {
            var command = new CreatePostCommand("Test1", "Test2", DateTime.Now);

            await postCreateCommandHandler.Handle(command, CancellationToken.None);

            Mock.Get(unitOfWork).Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Mock.Get(postRepository).Verify(x => x.Add(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public async Task GetAPostById()
        {
            var request = new GetPostQuery(Guid.Parse("4b00bb02-d740-44e4-8a9f-e495eeea695e"));

            var result = await getPostHandler.Handle(request, CancellationToken.None);

            Assert.That(result.Id, Is.EqualTo(request.postId));

            Mock.Get(postRepository).Verify(x => x.GetByIdASync(request.postId), Times.Once);
        }

        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(0, 10, 2)]
        [TestCase(10, 10, 0)]
        [TestCase(-1, -1, 0)]
        public async Task SkipAndLimitTest(int skip, int limit, int expectedPostNumber)
        {
            var request = new SkipAndLimitPostQuery(skip, limit);

            var result = await skipAndLimitPostQueryHandler.Handle(request, CancellationToken.None);

            Assert.That(result.Posts.Count, Is.EqualTo(expectedPostNumber));
        }



    }
}