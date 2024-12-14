using ComputerECommerce.Data;
using NUnit.Framework;

namespace ComputerECommerce.Tests;

[TestFixture]
public class LoginTests
{
    private readonly LoginModel _loginModel;
    private readonly DataContext _context;
    private readonly LoginModel.InputModel _input;
    private readonly string _userRole;
    public LoginTests()
    {
        _loginModel = new LoginModel(_context);
        _input = new LoginModel.InputModel();
        _userRole = LoginModel.UserRole;
    }
    [Test]
    public static void AdminLoginCorrect()
    {
        var test = new LoginTests();
        var userrole = test._userRole;
        test._loginModel.Input = new LoginModel.InputModel
        {
           Username = "admin@admin.com",
           Password = "admin"
        };

        var result = test._loginModel.OnPost();

        Assert.That(userrole, Is.EqualTo("Admin"));
    }
}