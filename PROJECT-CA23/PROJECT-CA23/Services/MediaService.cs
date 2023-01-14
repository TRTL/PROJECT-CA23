using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Api;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Services.IServices;
using System.Net.Http;

namespace PROJECT_CA23.Services
{
    public class MediaService : IMediaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serviceUrl;
        private readonly string _mykey;
        private readonly IMediaAdapter _mediaAdapter;

        public MediaService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMediaAdapter mediaAdapter)
        {
            _httpClientFactory = httpClientFactory;
            _serviceUrl = configuration.GetValue<string>("OmdbApiCom:ServiceUrl");
            _mykey = configuration.GetValue<string>("OmdbApiCom:MyKey");
            _mediaAdapter = mediaAdapter;
        }
        public async Task<OmdbApiMedia?> SearchForMediaAtOmdbApiAsync(string mediaTitle)
        {
            var httpClient = _httpClientFactory.CreateClient("OmdbApiComServiceApi");
            //var endpoint = "";    // no custom endpoint, just base address

            var reponse = await httpClient.GetAsync("?apikey=" + _mykey + "&t=" + mediaTitle);
            if (reponse.IsSuccessStatusCode)
            {
                var content = await reponse.Content.ReadAsStringAsync();
                var omdbApiMedia = JsonConvert.DeserializeObject<OmdbApiMedia>(content); // 1

                // 2 OmdbApiMedia.Genres >> issaugo trukstamus i mano db

                // 3 adapteriui OmdbApiMedia ir visa list<mano db esanciu Genres>

                // 4 repo issaugo atiduota Media

                //Media media = _mediaAdapter.Bind(res);
                //return media;
                return omdbApiMedia;
            }
            return null;
        }









    }
}
