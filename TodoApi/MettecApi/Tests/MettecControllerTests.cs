using Xunit;
using Microsoft.EntityFrameworkCore;
using MettecApi.Controllers;
using MettecApi.Data;
using MettecApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MettecApi.Tests
{
    public class MettecControllerTests
    {
        private MettecContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MettecContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new MettecContext(options);
        }

        [Fact] // Test dodawania nowego zadania
        public async Task CreateTodo_ShouldAddItemToDatabase()
        {
            var context = GetDbContext();
            var controller = new MettecController(context);
            var newItem = new MettecItem
            {
                Title = "Testowy tytuł",
                Description = "Testowy opis",
                IsDone = false
            };

            var result = await controller.CreateTodo(newItem);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var item = Assert.IsType<MettecItem>(createdAtActionResult.Value);
            Assert.Equal("Testowy tytuł", item.Title);
            Assert.Single(context.Todos);
        }

        [Fact] // Test pobierania wszystkich zadań
        public async Task GetTodos_ShouldReturnAllItems()
        {
            var context = GetDbContext();
            context.Todos.Add(new MettecItem { Title = "Zadanie 1", Description = "Opis zadania", IsDone = false });
            context.Todos.Add(new MettecItem { Title = "Zadanie 2", Description = "Opis zadania", IsDone = true });
            await context.SaveChangesAsync();

            var controller = new MettecController(context);

            var result = await controller.GetTodos();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<MettecItem>>>(result);
            var todos = Assert.IsAssignableFrom<IEnumerable<MettecItem>>(actionResult.Value);
            Assert.Equal(2, todos.Count());
        }

        [Fact] // Test aktualizacji statusu zadania
        public async Task UpdateTodoStatus_ShouldChangeIsDoneValue()
        {
            var context = GetDbContext();
            var item = new MettecItem { Title = "Test", Description = "Opis", IsDone = false };
            context.Todos.Add(item);
            await context.SaveChangesAsync();

            var controller = new MettecController(context);
            var updatedItem = new MettecItem { IsDone = true };

            var result = await controller.UpdateTodoStatus(item.Id, updatedItem);

            Assert.IsType<NoContentResult>(result);
            var updated = await context.Todos.FindAsync(item.Id);
            Assert.True(updated.IsDone);
        }

        [Fact] // Test usuwania zadania
        public async Task DeleteTodo_ShouldRemoveItemFromDatabase()
        {
            var context = GetDbContext();
            var item = new MettecItem { Title = "Do usunięcia", Description = "Opis", IsDone = false };
            context.Todos.Add(item);
            await context.SaveChangesAsync();
            var controller = new MettecController(context);
            var result = await controller.DeleteTodo(item.Id);
            Assert.IsType<NoContentResult>(result);
            var deletedItem = await context.Todos.FindAsync(item.Id);
            Assert.Null(deletedItem);
        }
    }
}
