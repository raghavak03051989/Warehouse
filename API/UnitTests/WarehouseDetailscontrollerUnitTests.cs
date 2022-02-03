using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
namespace API.UnitTests
{
    [TestFixture]
    public class WarehouseDetailscontrollerUnitTests
    {
        private WarehouseDetailscontroller _warehouseDetailscontroller;
        private Mock<DataContext> mockContext;

        [SetUp]
        public void SetUp()
        {
           var data = new List<Vehicle>
            {
                new Vehicle { Id=1, Make="Volkswage",Model="Jetta III",YearModel=1995,Price = 12947.52,IsLicensed=true,DateAdded=new DateTime(2018,09,18),WarehouseId=1 },
                new Vehicle { Id=2, Make="Chevrolet",Model="Corvette",YearModel=2004,Price=20019.64,IsLicensed=true,DateAdded=new DateTime(2018,01,27),WarehouseId=1},
                new Vehicle {Id=3,Make="Ford",Model="Expeditio EL",YearModel=2008,Price=27323.42,IsLicensed=false,DateAdded= new DateTime(2018,07,03),WarehouseId=1}
            }.AsQueryable();

           var mockSet = new Mock<DbSet<Vehicle>>();
           mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(data.Provider);
           mockContext = new Mock<DataContext>();
           mockContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        }

        [Test]
        public void GetAllVehiclesTest()
        {
            _warehouseDetailscontroller = new WarehouseDetailscontroller(mockContext.Object);
            // Act
            var allVehicles = _warehouseDetailscontroller.GetVehicles();
            // Assert
            Assert.IsTrue(allVehicles.Value.Count() > 0);
        }
    }
}