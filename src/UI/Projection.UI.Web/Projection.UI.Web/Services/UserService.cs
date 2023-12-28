using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Projection.UI.Web.Services;

public class UserService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "/connect/userinfo";

    public async Task<UserDetailsDto> GetUserDetailsAsync(string accessToken)
    {
        // Set the access token in the Authorization header
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Make a request to the UserInfo endpoint
        var response = await httpClient.GetAsync("connect/userinfo");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDetailsDto>(content);
        }

        // Handle error scenarios
        return null;
    }
}

public class UserDetailsDto
{
    public string Sub { get; set; } // Subject - User ID
    public string Name { get; set; } // User Name
    // Add other properties as needed
}