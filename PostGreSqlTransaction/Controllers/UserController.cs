using Microsoft.AspNetCore.Mvc;

namespace PostGreSqlTransaction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public UserController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }



        //        using(var unitOfWork = new UnitOfWork())
        //using(var transaction = new unitOfWork.BeginTransaction())
        //{
        //     try
        //     {
        //         unitOfWork.Users.Add(new User(... User One ...))
        //         unitOfWork.Save();

        //         unitOfWork.Addresses(new Address(... Address For User One ...))
        //         unitOfWork.Save();

        //         unitOfWork.Users.Add(new User(... User Two...))
        //         unitOfWork.Save();

        //         unitOfWork.Addresses(new Address(... Address For User Two...))
        //         unitOfWork.Save();
        //         transaction.Commit();
        //     }
        //     catch (Exception)
        //{
        //    transaction.Rollback();
        //}

        //}
    }
}
