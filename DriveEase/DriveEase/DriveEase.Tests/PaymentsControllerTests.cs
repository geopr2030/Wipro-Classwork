using Xunit;
using Microsoft.EntityFrameworkCore;
using DriveEase.Controllers;
using DAL;
using Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PaymentsControllerTests
{
	private DriveEaseContext GetDbContext()
	{
		var options = new DbContextOptionsBuilder<DriveEaseContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		return new DriveEaseContext(options);
	}

	[Fact]
	public async Task Index_ReturnsViewWithPaymentList()
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

		context.Payments.Add(new Payment
		{
			LeaseId = lease.LeaseId,
			Amount = 5000,
			PaymentDate = DateTime.Today
		});

		context.SaveChanges();

		var controller = new PaymentsController(context);

		var result = await controller.Index();

		var viewResult = Assert.IsType<ViewResult>(result);
		var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Payment>>(viewResult.Model);

		Assert.Single(model);
	}

	[Fact]
	public async Task Details_ReturnsNotFound_WhenPaymentInvalid()
	{
		var context = GetDbContext();
		var controller = new PaymentsController(context);

		var result = await controller.Details(999);

		Assert.IsType<NotFoundResult>(result);
	}

	[Fact]
	public async Task Create_AddsPaymentSuccessfully()
	{
		var context = GetDbContext();

		// Create Vehicle
		var vehicle = new Vehicle
		{
			VehicleId = 1,
			Make = "Honda",
			Model = "City",
			Year = 2023,
			DailyRate = 2000,
			Status = "Available",
			PassengerCapacity = 5,
			EngineCapacity = "1500cc"
		};

		context.Vehicles.Add(vehicle);

		// Create Lease linked to Vehicle
		var lease = new Lease
		{
			LeaseId = 1,
			VehicleId = 1,
			CustomerId = 1,
			StartDate = DateTime.Today,
			EndDate = DateTime.Today.AddDays(2),
			Type = "Daily"
		};

		context.Leases.Add(lease);
		context.SaveChanges();

		var controller = new PaymentsController(context);

		var payment = new Payment
		{
			LeaseId = 1
		};

		var result = await controller.Create(payment);

		Assert.IsType<RedirectToActionResult>(result);
		Assert.Single(context.Payments);
	}

	[Fact]
	public async Task DeleteConfirmed_RemovesPayment()
	{
		var context = GetDbContext();

		var payment = new Payment
		{
			LeaseId = 1,
			Amount = 4000,
			PaymentDate = DateTime.Today
		};

		context.Payments.Add(payment);
		context.SaveChanges();

		var controller = new PaymentsController(context);

		await controller.DeleteConfirmed(payment.PaymentId);

		Assert.Empty(context.Payments);
	}
}