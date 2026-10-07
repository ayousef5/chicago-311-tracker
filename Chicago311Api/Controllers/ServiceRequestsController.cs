using Chicago311Api.Data;
using Chicago311Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chicago311Api.Controllers;

// Handles HTTP requests for Chicago 311 service requests.
[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    // Gives us access to the MySQL database.
    private readonly AppDbContext _context;

    // Gets the database context from ASP.NET Core.
    public ServiceRequestsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /api/ServiceRequests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetRequests()
    {
        // Get the 100 most recent requests.
        var requests = await _context.ServiceRequests
            .OrderByDescending(request => request.CreatedDate)
            .Take(100)
            .ToListAsync();

        // Return the requests as JSON.
        return Ok(requests);
    }

    // GET: /api/ServiceRequests/SR26-02042784
    [HttpGet("{srNumber}")]
    public async Task<ActionResult<ServiceRequest>> GetRequest(string srNumber)
    {
        // Find the request using its primary key.
        var request = await _context.ServiceRequests
            .FindAsync(srNumber);

        // Return 404 if the request does not exist.
        if (request == null)
        {
            return NotFound();
        }

        // Return the request as JSON.
        return Ok(request);
    }
}