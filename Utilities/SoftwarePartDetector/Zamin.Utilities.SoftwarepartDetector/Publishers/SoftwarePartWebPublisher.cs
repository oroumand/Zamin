using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Zamin.Utilities.SoftwarePartDetector.DataModel;
using Zamin.Utilities.SoftwarePartDetector.Options;

namespace Zamin.Utilities.SoftwarePartDetector.Publishers;

public class SoftwarePartWebPublisher : ISoftwarePartPublisher
{
    private readonly HttpClient httpClient;
    private readonly SoftwarePartDetectorOptions _softwarePartDetectorOptions;
    public SoftwarePartWebPublisher(HttpClient httpClient, IOptions<SoftwarePartDetectorOptions> softwarePartDetectorOption)
    {
        this.httpClient = httpClient;
        _softwarePartDetectorOptions = softwarePartDetectorOption.Value;

    }
    public async Task Publish(SoftwarePart softwarePart)
    {
        httpClient.BaseAddress = new Uri(_softwarePartDetectorOptions.DestinationServiceBaseAddress);

        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new { SoftwarePart = softwarePart}),
                                                    Encoding.UTF8,
                                                    "application/json");

        await httpClient.PostAsync(_softwarePartDetectorOptions.DestinationServicePath, httpContent);

    }
}
