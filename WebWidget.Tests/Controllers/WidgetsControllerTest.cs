namespace WebWidget.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;
    using WebWidget.Controllers;
    using WebWidget.DataAccess.Repositories;
    using WebWidget.DTO.Models;

    [TestClass]
    public class WidgetsControllerTest
    {
        [TestMethod]
        public async Task GetWidgets()
        {
            // Arrange
            var mockRepo = new Mock<IWidgetRepository>();
            mockRepo.Setup(m => m.GetAll()).Returns(Task.FromResult(new List<Widget>() {
                new Widget() { Id = 1, Name = "Widget 1", Description = "For testing", Price = 1.0 },
                new Widget() { Id = 2, Name = "Widget 2", Description = "For testing", Price = 2.0 },
                new Widget() { Id = 3, Name = "Widget 3", Description = "For testing", Price = 3.0 },
            }));

            WidgetsController controller = new WidgetsController(mockRepo.Object);

            // Act
            var result = await controller.GetWidgets() as OkNegotiatedContentResult<List<Widget>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Count());
        }

        [TestMethod]
        public async Task GetWidget()
        {
            const int widgetId = 1;

            // Arrange
            var mockRepo = new Mock<IWidgetRepository>();
            mockRepo.Setup(m => m.Get(widgetId)).Returns(Task.FromResult(new Widget { Id = widgetId, Name = "Widget 1", Description = "For testing", Price = 1.0 }));
            WidgetsController controller = new WidgetsController(mockRepo.Object);

            // Act
            var result = await controller.GetWidget(widgetId) as OkNegotiatedContentResult<Widget>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Content.Id);
        }

        [TestMethod]
        public async Task PostWidget()
        {
            const int widgetId = 4;

            // Arrange
            var mockRepo = new Mock<IWidgetRepository>();            
            mockRepo.Setup(x => x.Create(It.IsAny<Widget>())).Returns(Task.FromResult(new Widget { Id = widgetId }));
            var controller = new WidgetsController(mockRepo.Object);

            // Act
            var newWidget = await controller.PostWidget(new Widget { Id = widgetId, Name = "Widget 4", Description = "For testing", Price = 20.99 });
            var result = newWidget as CreatedAtRouteNegotiatedContentResult<Widget>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("WebWidgetApi", result.RouteName);
            Assert.AreEqual(widgetId, result.RouteValues["id"]);
        }

        [TestMethod]
        public async Task PutWidget()
        {
            const int widgetId = 2;

            // Arrange
            var mockRepo = new Mock<IWidgetRepository>();
            mockRepo.Setup(x => x.Update(It.IsAny<Widget>())).Returns(Task.FromResult(true));
            var controller = new WidgetsController(mockRepo.Object);

            // Act
            var updatedWidget = await controller.PutWidget(widgetId, new Widget { Id = widgetId, Name = "Updated Widget 2", Price = 100.99 });
            var result = updatedWidget as OkNegotiatedContentResult<Widget>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Content.Id);
        }

        [TestMethod]
        public async Task DeleteWidget()
        {
            const int widgetId = 1;

            // Arrange
            var mockRepo = new Mock<IWidgetRepository>();
            mockRepo.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(new Widget { Id = widgetId }));
            var controller = new WidgetsController(mockRepo.Object);

            // Act
            var deletedWidget = await controller.DeleteWidget(widgetId);
            var result = deletedWidget as OkNegotiatedContentResult<Widget>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Content.Id);
        }
    }
}
