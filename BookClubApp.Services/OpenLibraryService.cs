using BookClubApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class OpenLibraryService:IOpenLibraryService
    {
        private readonly HttpClient _httpClient;
        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> SearchBooks(string query)
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentException("Query cannot be null or empty", nameof(query));

            var url = $"https://openlibrary.org/search.json?q={query}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
