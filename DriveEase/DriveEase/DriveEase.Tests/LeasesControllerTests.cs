using Xunit;
using Microsoft.EntityFrameworkCore;
using DriveEase.Controllers;
using DAL;
using Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

public class LeasesControllerTests
{
	private DriveEaseContext GetDbContext()
	{
		var options = new DbContextOptionsBuilder<DriveEaseContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		return new DriveEaseContext(options);
	}

	[Fact]
	public async Task Index_ReturnsViewWithLeaseList()
	{
		var context = GetDbContext();

		var vehicle = new Vehicle
		{
			VehicleId = 1,
			Make = "Honda",
			Model = "City",
			Year = 2022,
			DailyRate = 2000,
			Status = "Available",
			PassengerCapacity = 5,
			EngineCapacity = "1500cc"
		};

		var customer = new Customer
		{
			CustomerId = 1,
			FirstName = "John",
			LastName = "Doe",
			Email = "john@test.com",
			PhoneNumber = "9999999999"
		};

		context.Vehicles.Add(vehicle);
		context.Customers.Add(customer);

		context.Leases.Add(new Lease
		{
			VehicleId = 1,
			CustomerId = 1,
			StartDate = DateTime.Today,
			EndDate = DateTime.Today.AddDays(3)
		});

		context.SaveChanges();

		var controller = new LeasesController(context);

		var result = await controller.Index();

		var viewResult = Assert.IsType<ViewResult>(result);
		var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Lease>>(viewResult.Model);

		Assert.Single(model);
	}

	[Fact]
	public async Task Details_ReturnsNotFound_WhenLeaseInvalid()
	{
		var context = GetDbContext();
		var controller = new LeasesController(context);

		var result = await controller.Details(999);

		Assert.IsType<NotFoundResult>(result);
	}

	[Fact]
	public async Task Create_AddsLeaseSuccessfully()
	{
		var context = GetDbContext();

		context.Vehicles.Add(new Vehicle
		{
			VehicleId = 1,
			Make = "Toyota",
			Model = "Innova",
			Year = 2023,
			DailyRate = 2500,
			Status = "Available",
			PassengerCapacity = 7,
			EngineCapacity = "2000cc"
		});

		context.Customers.Add(new Customer
		{
			CustomerId = 1,
			FirstName = "John",
			LastName = "Doe",
			Email = "john@test.com",
			PhoneNumber = "9999999999"
		});

		context.SaveChanges();

		var controller = new LeasesController(context);

		var lease = new Lease
		{
			VehicleId = 1,
			CustomerId = 1,
			StartDate = DateTime.Today,
			EndDate = DateTime.Today.AddDays(2)
		};

		await controller.Create(lease);

		Assert.Equal(1, context.Leases.Count());
	}

	[Fact]
	public async Task DeleteConfirmed_RemovesLease()
	{
		var context = GetDbContext();

		var lease = new Lease
		{
			VehicleId = 1,
			CustomerId = 1,
			StartDate = DateTime.Today,
			EndDate = DateTime.Today.AddDays(2)
		};

		context.Leases.Add(lease);
		context.SaveChanges();

		var controller = new LeasesController(context);

		await controller.DeleteConfirmed(lease.LeaseId);

		Assert.Empty(context.Leases);
	}
}