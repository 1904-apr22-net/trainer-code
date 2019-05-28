using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using DogMvc.ApiModels;
using DogMvc.Models;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace DogMvc.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        public HttpClient HttpClient { get; }

        public IConfiguration Configuration { get; }

        public DogController(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
            Configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        // "where T : class" - this is a generic constraint. something to know about.
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
        private async Task<HttpResponseMessage> SendApiRequestAsync<T>(
            string path, HttpMethod method = null, T body = null) where T : class
        {
            var url = $"{Configuration["DogRestService:BaseUrl"]}{path}";
            var request = new HttpRequestMessage(method ?? HttpMethod.Get, url);
            var token = await GetBearerTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (body != null)
            {
                request.Content = new ObjectContent<T>(body, new JsonMediaTypeFormatter());
            }

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            return response;
        }

        private Task<HttpResponseMessage> SendApiRequestAsync(string path, HttpMethod method = null) =>
            SendApiRequestAsync<object>(path, method, null);

        // all this auth and httpclient code really belongs in a different
        // class; some repository or service class
        // reference: https://github.com/Azure-Samples/active-directory-dotnet-webapp-webapi-openidconnect-aspnetcore
        private async Task<string> GetBearerTokenAsync()
        {
            if (TempData.Peek("DogBearerToken") is string token)
            {
                return token;
            }

            IConfigurationSection adConfig = Configuration.GetSection("AzureAd");
            IConfigurationSection apiConfig = Configuration.GetSection("DogRestService");
            var authority = $"{adConfig["Instance"]}{adConfig["TenantId"]}";

            var authContext = new AuthenticationContext(authority);
            var credential = new ClientCredential(adConfig["ClientId"], apiConfig["ClientSecret"]);

            var userObjectId = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var userId = new UserIdentifier(userObjectId, UserIdentifierType.UniqueId);

            AuthenticationResult result = await authContext.AcquireTokenSilentAsync(
                apiConfig["ClientId"], credential, userId);
            var newToken = result.AccessToken;

            TempData["DogBearerToken"] = newToken;

            return newToken;
        }

        // GET: Dog
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response;
            try
            {
                response = await SendApiRequestAsync(Configuration["DogRestService:DogsPath"]);
            }
            catch (AdalSilentTokenAcquisitionException)
            {
                return new ChallengeResult(AzureADDefaults.AuthenticationScheme);
            }

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            // deserialize from JSON
            IEnumerable<Dog> dogs = await response.Content.ReadAsAsync<IEnumerable<Dog>>();

            IEnumerable<DogViewModel> model = dogs.Select(Mapper.Map);

            return View(model);
        }

        // GET: Dog/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await SendApiRequestAsync($"{Configuration["DogRestService:DogsPath"]}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            // deserialize from JSON
            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            return View(model);
        }

        // GET: Dog/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            if (User.IsInRole("Administrator"))
            {
                HttpResponseMessage response = await SendApiRequestAsync(Configuration["DogRestService:AccountsPath"]);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }

                // deserialize from JSON
                IEnumerable<Account> accounts = await response.Content.ReadAsAsync<IEnumerable<Account>>();

                var model = new DogViewModel
                {
                    Accounts = accounts.Select(a => new SelectListItem(a.Name, a.Email))
                };

                return View(model);
            }

            return View();
        }

        // POST: Dog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create(DogViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string email;
                if (User.IsInRole("Administrator"))
                {
                    email = viewModel.OwnerEmail;
                }
                else
                {
                    email = User.FindFirst(ClaimTypes.Email).Value;
                }

                var dog = new Dog
                {
                    Name = viewModel.Name,
                    Breed = viewModel.Breed,
                    Owner = new Account { Email = email }
                };

                HttpResponseMessage response = await SendApiRequestAsync(
                    Configuration["DogRestService:DogsPath"], HttpMethod.Post, dog);

                if (!response.IsSuccessStatusCode)
                {
                    // could check specifically for 400 and look inside
                    // body for the model errors, to then add to the ModelState here.
                    ModelState.AddModelError("", "Invalid data");
                    return View(viewModel);
                }

                // i don't actually need the response body

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Dog/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await SendApiRequestAsync($"{Configuration["DogRestService:DogsPath"]}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            HttpResponseMessage response2 = await SendApiRequestAsync(Configuration["DogRestService:AccountsPath"]);

            if (!response2.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            // deserialize from JSON
            IEnumerable<Account> accounts = await response2.Content.ReadAsAsync<IEnumerable<Account>>();

            if (!User.IsInRole("Administrator"))
            {
                if (dog.Owner.Email == User.FindFirst(ClaimTypes.Email).Value)
                {
                    accounts = accounts.Where(a => a.Email == dog.Owner.Email);
                }
                else
                {
                    return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
                }
            }

            model.Accounts = accounts.Select(a => new SelectListItem(a.Name, a.Email));

            return View(model);
        }

        // POST: Dog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit(int id, DogViewModel viewModel)
        {
            try
            {
                var dog = new Dog
                {
                    Name = viewModel.Name,
                    Breed = viewModel.Breed,
                    Owner = new Account()
                };

                if (User.IsInRole("Administrator"))
                {
                    dog.Owner.Email = viewModel.OwnerEmail;
                }
                else
                {
                    dog.Owner.Email = User.FindFirst(ClaimTypes.Email).Value;
                }

                HttpResponseMessage response = await SendApiRequestAsync(
                    $"{Configuration["DogRestService:DogsPath"]}/{id}", HttpMethod.Put, dog);

                // if the user is not allowed to edit this dog, he will be denied by the API
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
                    }
                    // could check specifically for 400 and look inside
                    // body for the model errors, to then add to the ModelState here.
                    ModelState.AddModelError("", "Invalid data");
                    return View(viewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred");
                return View(viewModel);
            }
        }

        // GET: Dog/Delete/5
        [Authorize(Roles = "Administrator, Moderator")] // must be one of these roles
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await SendApiRequestAsync($"{Configuration["DogRestService:DogsPath"]}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            return View(model);
        }

        // POST: Dog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await SendApiRequestAsync(
                    $"{Configuration["DogRestService:DogsPath"]}/{id}", HttpMethod.Delete);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }
    }
}
