using Application.Answers.Create;
using Application.Answers.Delete;
using Application.Answers.Get;
using Application.Data;
using Domain.Entities.Answers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UnitTests
{
    [TestFixture]
    public class AnswerTests
    {
        private IAnswerRepository answerRepository;

        private IUnitOfWork unitOfWork;

        private CreateAnswerCommandHandler createAnswerCommandHandler;

        private DeleteCommandHandler deleteAnswerCommandHandler;

        private GetAnswersFromPostQueryHandler getAnswersFromPostQueryHandler;


        [SetUp]
        public void Setup()
        {
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(t => t.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            unitOfWork = mockUnitOfWork.Object;

            Mock<IAnswerRepository> mockAnswerRepository = new Mock<IAnswerRepository>();

            mockAnswerRepository.Setup(x => x.Add(It.IsAny<Answer>()));

            mockAnswerRepository.Setup(x => x.Update(It.IsAny<Answer>()));

            mockAnswerRepository.Setup(x => x.Delete(It.IsAny<Guid>()));

            mockAnswerRepository.Setup(x => x.GetAllFromPost(It.Is<Guid>((guid) => guid == Guid.Parse("ea675197-da72-42d4-9b00-22eb9bd22197")))).Returns(
            
                new List<Answer>() {
                    new Answer(Guid.Parse("0edeff2c-8f6a-4fe2-87b1-d619beebf8a1"), "Answer1"),
                    new Answer(Guid.Parse("e1837423-f414-4797-88a8-d9bad8b59be6"), "Answer2"),
                    new Answer(Guid.Parse("a09216de-b685-4ba7-9e7a-b168afdb7c12"), "Answer3"),
                }
            );
            answerRepository = mockAnswerRepository.Object;


            createAnswerCommandHandler = new CreateAnswerCommandHandler(mockAnswerRepository.Object, null, unitOfWork);
            getAnswersFromPostQueryHandler = new GetAnswersFromPostQueryHandler(mockAnswerRepository.Object);
            deleteAnswerCommandHandler = new DeleteCommandHandler(mockAnswerRepository.Object, mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetAnswerToPostTest()
        {
            var query = new GetAnswersFromPostQuery(Guid.Parse("ea675197-da72-42d4-9b00-22eb9bd22197"), 0, 10);

            var result = await getAnswersFromPostQueryHandler.Handle(query, CancellationToken.None);

            Assert.That(result.AnswerResponses.Count, Is.EqualTo(3));


            Mock.Get(answerRepository).Verify(x => x.GetAllFromPost(It.IsAny<Guid>()), Times.Once);

            //Mock.Get(unitOfWork).Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());



        }
    }
}
