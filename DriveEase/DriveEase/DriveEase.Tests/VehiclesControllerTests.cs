using Xunit;
using Microsoft.EntityFrameworkCore;
using DriveEase.Controllers;
using DAL;
using Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

public class VehiclesControllerTests
{
	private DriveEaseContext GetDbContext()
	{
		var options = new DbContextOptionsBuilder<DriveEaseContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		return new DriveEaseContext(options);
	}

	[Fact]
	public async Task Create_AddsVehicleSuccessfully()
	{
		var context = GetDbContext();
		var controller = new VehiclesController(context);

		var vehicle = new Vehicle
		{
			Make = "Toyota",
			Model = "Innova",
			Year = 2023,
			DailyRate = 2500,
			Status = "Available",
			PassengerCapacity = 7,
			EngineCapacity = "2000cc"
		};

		await controller.Create(vehicle);

		Assert.Equal(1, context.Vehicles.Count());
	}

	[Fact]
	public async Task Details_ReturnsNotFound_WhenVehicleDoesNotExist()
	{
		var context = GetDbContext();
		var controller = new VehiclesController(context);

		IActionResult result = await controller.Details(999);

		Assert.IsType<NotFoundResult>(result);
	}
	[Fact]
	public async Task Index_ReturnsViewWithVehicleList()
	{
		var context = GetDbContext();
		context.Vehicles.Add(new Vehicle
		{
			Make = "Honda",
			Model = "City",
			Year = 2022,
			DailyRate = 2000,
			Status = "Available",
			PassengerCapacity = 5,
			EngineCapacity = "1500cc"
		});
		context.SaveChanges();

		var controller = new VehiclesController(context);

		var result = await controller.Index();

		var viewResult = Assert.IsType<ViewResult>(result);
		var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Vehicle>>(viewResult.Model);
		Assert.Single(model);
	}
	[Fact]
	public async Task Edit_ReturnsNotFound_WhenIdInvalid()
	{
		var context = GetDbContext();
		var controller = new VehiclesController(context);

		var result = await controller.Edit(999);

		Assert.IsType<NotFoundResult>(result);
	}
	[Fact]
	public async Task DeleteConfirmed_RemovesVehicle()
	{
		var context = GetDbContext();

		var vehicle = new Vehicle
		{
			Make = "Ford",
			Model = "EcoSport",
			Year = 2021,
			DailyRate = 1800,
			Status = "Available",
			PassengerCapacity = 5,
			EngineCapacity = "1500cc"
		};

		context.Vehicles.Add(vehicle);
		context.SaveChanges();

		var controller = new VehiclesController(context);

		await controller.DeleteConfirmed(vehicle.VehicleId);

		Assert.Empty(context.Vehicles);
	}
}