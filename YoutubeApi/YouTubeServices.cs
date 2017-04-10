using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YoutubeApi
{
    public class YouTubeServices
    {
        public YouTubeService Client;

        public YouTubeServices()
        {
            Client = CreateYouTubeService();
        }

        private YouTubeService CreateYouTubeService()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "Api Key here",
                ApplicationName = this.GetType().ToString()
            });
        }

        public  async Task<List<VideoModel>> GetVideos(string searchQuery)
        {
            var searchListRequest = Client.Search.List("snippet");
            searchListRequest.Q = searchQuery;
            searchListRequest.MaxResults = 20;
            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            return (from searchResult in searchListResponse.Items
                where searchResult.Id.Kind.Equals("youtube#video")
                select new VideoModel
                {
                    VideoId = searchResult.Id.VideoId,
                    Url = "https://www.youtube.com/watch?v=" + searchResult.Id.VideoId,
                    Title = searchResult.Snippet.Title,
                    Description = searchResult.Snippet.Description
                }).ToList();
        }
    }
}