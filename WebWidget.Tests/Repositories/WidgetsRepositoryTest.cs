namespace WebWidget.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using WebWidget.DataAccess;
    using WebWidget.DataAccess.Repositories;
    using WebWidget.DTO.Models;
    using WebWidget.Tests.Helpers;

    [TestClass]
    public class WidgetsRepositoryTest
    {
        [TestMethod]
        public async Task GetAll()
        {
            // Arrange
            var mockContext = new Mock<WidgetContext>();
            var mockSet = GetMockSet(GetData());
            mockContext.Setup(m => m.Widgets).Returns(mockSet.Object);

            // Act
            var repo = new WidgetRepository(mockContext.Object);
            var result = await repo.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task Get()
        {
            const int widgetId = 1;
            var data = GetData();

            // Arrange            
            var mockSet = GetMockSet(data);
            mockSet.Setup(m => m.FindAsync(widgetId)).Returns(Task.FromResult(data.FirstOrDefault(w => w.Id == widgetId)));

            var mockContext = new Mock<WidgetContext>();
            mockContext.Setup(c => c.Widgets).Returns(mockSet.Object);

            // Act
            var repo = new WidgetRepository(mockContext.Object);
            var result = await repo.Get(widgetId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(widgetId, result.Id);
        }

        [TestMethod]
        public async Task Create()
        {
            var data = GetData();
            var newWidget = new Widget { Id = 4, Name = "Widget 4", Description = "For testing", Price = 44.99 };

            // Arrange            
            var mockSet = GetMockSet(data);
            mockSet.Setup(m => m.Add(It.IsAny<Widget>())).Callback<Widget>((w) =>
            {
                var tmpData = data.ToList();
                tmpData.Add(w);
                data = tmpData.AsQueryable();
            });

            var mockContext = new Mock<WidgetContext>();
            mockContext.Setup(m => m.Widgets).Returns(mockSet.Object);
            
            // Act
            var repo = new WidgetRepository(mockContext.Object);
            var result = await repo.Create(newWidget);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newWidget.Id, result.Id);

            // Check that each method was only called once.
            mockSet.Verify(m => m.Add(It.IsAny<Widget>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task Update()
        {
            var data = GetData();
            var mockSet = GetMockSet(data);
            var updatedWidget = new Widget { Id = data.ElementAt(0).Id, Name = "Updated Widget 1", Description = "Updated widget", Price = 999.99 };

            // Arrange
            var mockContext = new Mock<WidgetContext>();
            mockContext.Setup(m => m.SetModifiedState(It.IsAny<Widget>()));

            // Act
            var repo = new WidgetRepository(mockContext.Object);
            var result = await repo.Update(updatedWidget);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);

            // Check that each method was only called once.
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task Delete()
        {
            var data = GetData();
            const int widgetId = 1;

            // Arrange            
            var mockSet = GetMockSet(data);
            mockSet.Setup(m => m.FindAsync(widgetId)).Returns(Task.FromResult(data.FirstOrDefault(w => w.Id == widgetId)));
            mockSet.Setup(m => m.Remove(It.IsAny<Widget>()));

            var mockContext = new Mock<WidgetContext>();
            mockContext.Setup(m => m.Widgets).Returns(mockSet.Object);

            // Act
            var repo = new WidgetRepository(mockContext.Object);
            await repo.Delete(widgetId);

            // Assert
            // Check that each method was only called once.
            mockSet.Verify(m => m.Remove(It.IsAny<Widget>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private Mock<DbSet<Widget>> GetMockSet(IQueryable<Widget> data)
        {
            var mockSet = new Mock<DbSet<Widget>>();
            mockSet.As<IDbAsyncEnumerable<Widget>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Widget>(data.GetEnumerator()));

            mockSet.As<IQueryable<Widget>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Widget>(data.Provider));

            mockSet.As<IQueryable<Widget>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Widget>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Widget>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        private IQueryable<Widget> GetData()
        {
            return new List<Widget>()
            {
                new Widget() { Id = 1, Name = "Widget 1", Description = "For testing", Price = 1.0 },
                new Widget() { Id = 2, Name = "Widget 2", Description = "For testing", Price = 2.0 },
                new Widget() { Id = 3, Name = "Widget 3", Description = "For testing", Price = 3.0 },
            }
            .AsQueryable();
        }
    }
}
