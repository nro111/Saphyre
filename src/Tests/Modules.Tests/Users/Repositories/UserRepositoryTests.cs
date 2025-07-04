using Application.Interfaces;
using Contracts;
using DataAccess.Context;
using DataAccess.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Shared;

public class UserRepositoryTests
{
    private SaphyreContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<SaphyreContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(c => c[It.IsAny<string>()]).Returns(string.Empty);

        return new SaphyreContext(options, mockConfig.Object);
    }

    private UserDTO CreateStubUser()
    {
        return new UserDTO
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            Phone = "5551234567",
            AddressLine1 = "123 Main St",
            AddressLine2 = "Apt 4B",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704",
            DateOfBirth = new DateTime(1990, 5, 21)
        };
    }

    private User CreateStubUserEntity(Guid id)
    {
        return new User
        {
            Id = id,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            Phone = "5551234567",
            AddressLine1 = "123 Main St",
            AddressLine2 = "Apt 4B",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704",
            DateOfBirth = new DateTime(1990, 5, 21)
        };
    }

    private UserRepository CreateRepository(SaphyreContext context)
    {
        var mockLogger = new Mock<ILogger<IUserRepository>>();
        return new UserRepository(context, mockLogger.Object);
    }

    [Fact]
    public async Task Create_Adds_User()
    {
        using var context = CreateInMemoryContext();
        var repo = CreateRepository(context);
        var userDto = CreateStubUser();

        var result = await repo.Create(userDto);
        await context.SaveChangesAsync();

        Assert.True(result.Success);
        Assert.NotNull(await context.Set<User>().FindAsync(userDto.Id));
    }

    [Fact]
    public async Task Get_Returns_User_When_Found()
    {
        using var context = CreateInMemoryContext();
        var userDto = CreateStubUser();

        // Seed
        context.Set<User>().Add(new User
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Phone = userDto.Phone,
            AddressLine1 = userDto.AddressLine1,
            AddressLine2 = userDto.AddressLine2,
            City = userDto.City,
            State = userDto.State,
            PostalCode = userDto.PostalCode,
            DateOfBirth = userDto.DateOfBirth
        });
        await context.SaveChangesAsync();

        var repo = CreateRepository(context);
        var result = await repo.Get(userDto.Id);

        Assert.True(result.Success);
        Assert.Equal(userDto.Email, result.Value.Email);
    }

    [Fact]
    public async Task Delete_Removes_User_When_Found()
    {
        using var context = CreateInMemoryContext();
        var userDto = CreateStubUser();

        // Seed
        context.Set<User>().Add(CreateStubUserEntity(userDto.Id));
        await context.SaveChangesAsync();

        var repo = CreateRepository(context);
        var result = await repo.Delete(userDto.Id);
        await context.SaveChangesAsync();

        Assert.True(result.Success);
        Assert.Null(await context.Set<User>().FindAsync(userDto.Id));
    }

    [Fact]
    public async Task GetAll_Returns_Users()
    {
        using var context = CreateInMemoryContext();

        context.Set<User>().AddRange(
            CreateStubUserEntity(new Guid()),
            CreateStubUserEntity(new Guid())
        );
        await context.SaveChangesAsync();

        var repo = CreateRepository(context);
        var result = await repo.GetAll();

        Assert.True(result.Success);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task Update_Fails_When_NotFound()
    {
        using var context = CreateInMemoryContext();
        var repo = CreateRepository(context);

        var userDto = CreateStubUser();
        var result = await repo.Update(userDto);

        Assert.False(result.Success);
        Assert.Equal(OperationStatus.NotFound, result.Status);
    }
}
