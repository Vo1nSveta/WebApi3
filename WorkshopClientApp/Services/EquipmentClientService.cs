using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorkshopClientApp.Data;
using WorkshopClientApp.Models;

namespace WorkshopClientApp.Services;

public class EquipmentClientService
{
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new()

    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };        

    public EquipmentClientService(HttpClient httpClient)

    {
        _httpClient = httpClient;
    }

    public async Task<List<Equipment>> GetEquipmentAll()
    {
        var response = await _httpClient.GetAsync("/Equipment");
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<List<Equipment>>(responseText, _jsonOptions);

        return responseData;
    }

    public async Task AddEquipment(EquipmentAddDto request)
    {
        var response = await _httpClient.PutAsJsonAsync("/Equipment", request);

        return;
    }

    public async Task RemoveEquipment(int id)
    {

        var response = await _httpClient.DeleteAsync($"Equipment?equipmentId={id}");

        return;
    }

    public async Task<Equipment> GetEquipmentById(int id)
    {
        var response = await _httpClient.GetAsync($"/Equipment/{id}");
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<Equipment>(responseText, _jsonOptions);

        return responseData;
    }

    public async Task EditEquipment(EquipmentEditDto request)
    {
        var response = await _httpClient.PatchAsJsonAsync("/Equipment", request);

        return;
    }
        public async Task<List<Equipment>> SearchEquipments(string name)
    {
        var response = await _httpClient.PostAsync($"/Equipment?name={name}", null);
        var responseText = await response.Content.ReadAsByteArrayAsync();

        var responseData = JsonSerializer.Deserialize<List<Equipment>>(responseText, _jsonOptions);

        return responseData;
    }

    
}
