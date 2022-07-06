using System;
using System.Threading.Tasks;
using Moq;
using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;
using PostGreSqlTransaction.Repositories;
using Xunit;

namespace PostgreSqlUnitTest;

public class UserServiceTest
{
    // private readonly Mock<IUserRepository> _userRepo = new Mock<IUserRepository>();
    // private readonly Mock<IAccountRepository> _accountRepo = new Mock<IAccountRepository>();
    private readonly TransContext _context = new TransContext();
    private readonly UnitOfWork _unitOfWork;

    public UserServiceTest()
    {
        _unitOfWork = new UnitOfWork(_context);
        // _unitOfWork = new UnitOfWork(_context , _userRepo.Object, _accountRepo.Object);
    }

    [Fact]
    public async Task GetUserById_ShouldBeReturnUser_WhenUserExisted()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var userName = "hoa";
        var birthday = "1999/01/23";
        var address = "67 Thanh Luong 17";

        // Act
        var user = new User
        {
            Id = userId,
            Name = userName,
           // DateOfBirth = DateTime.Parse(birthday),
            Address = address
        };
        _unitOfWork.Users.CreateUser(user);
         _unitOfWork.Save();

        // Assert
        Assert.Equal(user.Id, userId);
    }
}