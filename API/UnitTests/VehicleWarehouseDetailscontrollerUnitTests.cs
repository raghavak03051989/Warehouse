using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
namespace API.UnitTests
{
    [TestFixture]
    public class VehicleWarehouseDetailscontrollerUnitTests
    {
        private VehicleWarehouseDetailscontroller _vehicleWarehouseDetailscontroller;
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

            var warehousedata = new List<Warehouse>
            {
                new Warehouse{ Id=1,Name="Warehouse A",Latitude=40,Longitude=60 }
            }.AsQueryable();
 
           var mockSet = new Mock<DbSet<Vehicle>>();
           mockSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(data.Provider);

            var mockSetB = new Mock<DbSet<Warehouse>>();
            mockSetB.As<IQueryable<Warehouse>>().Setup(m => m.Provider).Returns(warehousedata.Provider);

           mockContext = new Mock<DataContext>();
           mockContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
           mockContext.Setup(m => m.Warehouses).Returns(mockSetB.Object);
        }

        [Test]
        public void GetAllVehiclesTest()
        {
            _vehicleWarehouseDetailscontroller = new VehicleWarehouseDetailscontroller(mockContext.Object);
            // Act
            var vehicle = _vehicleWarehouseDetailscontroller.GetVehicleWarehouse(1);
            // Assert
            Assert.AreEqual(vehicle.Value.Name,"Warehouse A");
        }
    }
}