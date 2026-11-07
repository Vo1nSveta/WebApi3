using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorkshopClientApp.Data;
using WorkshopClientApp.Models;

namespace WorkshopClientApp.Services;

public class WorkshopClientService
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new()

    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };        

    public WorkshopClientService(HttpClient httpClient)

    {
        _httpClient = httpClient;
    }

    public async Task<List<Workshop>> GetWorkshopAll()
    {
        var response = await _httpClient.GetAsync("/Workshop");
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<List<Workshop>>(responseText, _jsonOptions);

        return responseData;
    }

    public async Task AddWorkshop(WorkshopAddDto request)
    {
        var response = await _httpClient.PutAsJsonAsync("/Workshop", request);

        return;
    }

    public async Task Remove(int id)
    {

        var response = await _httpClient.DeleteAsync($"Workshop?workshopId={id}");

        return;
    }

    public async Task<Workshop> GetWorkshopById(int id)
    {
        var response = await _httpClient.GetAsync($"/Workshop/{id}");
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<Workshop>(responseText, _jsonOptions);

        return responseData;
    }

    public async Task Edit(WorkshopEditDto request)
    {
        var response = await _httpClient.PatchAsJsonAsync("/Workshop", request);

        return;
    }
        public async Task<List<Workshop>> SearchWorkshops(string nameWorkshop, string groupWorkshop)
    {
        var response = await _httpClient.PostAsync($"/Workshop?nameWorkshop={nameWorkshop}&groupWorkshop={groupWorkshop}", null);
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<List<Workshop>>(responseText, _jsonOptions);

        return responseData;
    }

    public async Task<List<Equipment>> GetEquipmentAll()
    {
        var response = await _httpClient.GetAsync("/Equipment");
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<List<Equipment>>(responseText, _jsonOptions);

        return responseData;
    }

}
