using MyProject.Application;
using MyProject.Infrastructure;

namespace MyProject.Tests;

public class IntegrationTests
{
    [Fact]
    public void Save_And_Load_Works()
    {
        var file = Path.GetTempFileName();

        var repository = new JsonLibraryRepository(file);

        var service = new LibraryService(repository);

        service.AddBook("Test", "Author");

        service.Save();

        var newRepository = new JsonLibraryRepository(file);

        Assert.Single(newRepository.GetBooks());
    }
}