using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeProjectExist_Executed_ReturnThreeProjectsViewModels()
        {
            // Arrange
            var projects = new List<ProjectListDTO>
            {
                new ProjectListDTO(1, "Projeto 1", "Descrição 1", DateTime.Now),
                new ProjectListDTO(2, "Projeto 2", "Descrição 2", DateTime.Now),
                new ProjectListDTO(3, "Projeto 3", "Descrição 3", DateTime.Now)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAll(It.IsAny<string>())).ReturnsAsync(projects);

            var getAllProjectsQueryHandler = new GetAllProjectsQuery("");
            var getAllProjectsCommandHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var projectViewModelsList = await getAllProjectsCommandHandler.Handle(getAllProjectsQueryHandler, CancellationToken.None); 


            // Assert
        }
    }
}
