using System.ComponentModel.DataAnnotations;
using ComputerECommerce.Data;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using ComputerECommerce.Models;

namespace ComputerECommerce.Tests;

[TestFixture]
public class LoginTests
{
    private DataContext _context;
    private LoginModel _loginModel;
    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "postgres_db")
            .Options;

        _context = new DataContext(options);

        _context.Users.AddRange(
            new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                Email = "admin@admin.com",
                Password = "admin",
                Role = "Admin"
            },
            new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                Email = "user@user.com",
                Password = "user",
                Role = "User"
            }
        );

        _context.SaveChanges();

        _loginModel = new LoginModel(_context);
    }
    [Test]
    public void AdminLoginCorrect()
    {
        _loginModel.Input = new LoginModel.InputModel
        {
            Username = "admin@admin.com",
            Password = "admin"
        };

        var result = _loginModel.OnPost();
        var userrole = LoginModel.UserRole;

        Assert.That(userrole, Is.EqualTo("Admin"));
    }
    [Test]
    public void UserLoginCorrect()
    {
        _loginModel.Input = new LoginModel.InputModel
        {
            Username = "user@user.com",
            Password = "user"
        };

        var result = _loginModel.OnPost();
        var userrole = LoginModel.UserRole;

        Assert.That(userrole, Is.EqualTo("User"));
    }
    [Test]
    public void AdminLoginInCorrect()
    {
        _loginModel.Input = new LoginModel.InputModel
        {
            Username = "admin@admin.com",
            Password = "wrongPassAdmin" // wrong password
        };

        var result = _loginModel.OnPost();

        Assert.That(_loginModel.ModelState[string.Empty]!.Errors[0].ErrorMessage, Is.EqualTo("Invalid login attempt."));
    }
    [Test]
    public void UserLoginInCorrect()
    {
        _loginModel.Input = new LoginModel.InputModel
        {
            Username = "user@user.com",
            Password = "wrongPassUser" // wrong password
        };

        var result = _loginModel.OnPost();

        Assert.That(_loginModel.ModelState[string.Empty]!.Errors[0].ErrorMessage, Is.EqualTo("Invalid login attempt."));
    }
    [Test]
    public void EmptyCredientialsLogin()
    {
        _loginModel.Input = new LoginModel.InputModel
        {
            Username = "",
            Password = ""
        };

        var result = _loginModel.OnPost();

        Assert.That(_loginModel.ModelState[string.Empty]!.Errors[0].ErrorMessage, Is.EqualTo("Invalid login attempt."));
    }
}