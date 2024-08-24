// using Microsoft.AspNetCore.Mvc.Testing;
//
// namespace Test.Tools;
//
// public class Startup
// {
//     class Starter : WebApplicationFactory<Program>
//     {
//         private const string BaseUrl = "http://localhost:5207";
//
//         public HttpClient HttpClient { get; set; }
//
//         public Starter()
//         {
//             ClientOptions.BaseAddress = new Uri(BaseUrl);
//
//             HttpClient = CreateClient();
//             
//             
//         }
//     }
// }