using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projection.UI.Web.Services;

public class UserService(HttpClient httpClient)
{
    public async Task<UserDetailsDto> GetUserDetailsAsync(string id)
    {
        // Make a request to the UserInfo endpoint
        var response = await httpClient.GetAsync($"account/userinformation/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserDetailsDto>(content)!;

            return result;
        }

        // Handle error scenarios
        return null;
    }
}

public class UserDetailsDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get;set; }
}