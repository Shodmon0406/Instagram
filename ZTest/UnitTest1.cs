// using System.Net.Http.Headers;
// using Domain.Dtos.UserDto;
// using Domain.Responses;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Newtonsoft.Json;
//
// namespace Test;
//
// public class UserTest
// {
//     public class YourTestClass(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
//
//     {
//         /*private readonly HttpClient _client;
//
//         public YourTestClass()
//         {
//             _client = new HttpClient()
//             {
//                 BaseAddress = new Uri("http://localhost:5207")
//             };
//         }*/
//
//         [Fact]
//         public async void YourTest()
//         {
//             var client = factory.CreateClient();
//
//             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
//             
//             var response = await client.GetAsync("/User/get-users");
//
//             var content = response.Content.ReadAsStringAsync();
//
//             var result = content.Result;
//
//             var res = JsonConvert.DeserializeObject<PagedResponse<List<GetUserDto>>>(result);
//
//             response.EnsureSuccessStatusCode();
//         }
//     }
// }