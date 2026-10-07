using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Chicago311Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ServiceRequestsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetRequests()
    {
        // Get the 100 most recent Chicago 311 requests.
        var url =
            "https://data.cityofchicago.org/resource/v6vf-nfxy.json" +
            "?$limit=100&$order=created_date DESC";

        // Call the Chicago 311 API.
        var requests = await _httpClient.GetFromJsonAsync<object[]>(url);

        // Return the Chicago 311 data to our frontend.
        return Ok(requests);
    }

    [HttpGet("{srNumber}")]
    public async Task<IActionResult> GetRequest(string srNumber)
    {
        // Search for one service request by its number.
        var url =
            "https://data.cityofchicago.org/resource/v6vf-nfxy.json" +
            $"?sr_number={Uri.EscapeDataString(srNumber)}";

        // Call the Chicago 311 API.
        var requests = await _httpClient.GetFromJsonAsync<object[]>(url);

        // Return 404 if the request does not exist.
        if (requests == null || requests.Length == 0)
        {
            return NotFound(new { message = "Service request not found" });
        }

        // Return the first matching request.
        return Ok(requests[0]);
    }
}