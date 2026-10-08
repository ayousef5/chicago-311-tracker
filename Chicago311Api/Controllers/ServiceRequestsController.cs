using Chicago311Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chicago311Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestsController : ControllerBase
{
    // The Chicago 311 API uses snake_case names (sr_number) and sends numbers as
    // strings, so read it into our ServiceRequest model. The frontend then gets
    // the same camelCase JSON (srNumber) it always has.
    private static readonly JsonSerializerOptions ChicagoJson = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

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
        var requests = await _httpClient.GetFromJsonAsync<ServiceRequest[]>(url, ChicagoJson);

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
        var requests = await _httpClient.GetFromJsonAsync<ServiceRequest[]>(url, ChicagoJson);

        // Return 404 if the request does not exist.
        if (requests == null || requests.Length == 0)
        {
            return NotFound(new { message = "Service request not found" });
        }

        // Return the first matching request.
        return Ok(requests[0]);
    }
}