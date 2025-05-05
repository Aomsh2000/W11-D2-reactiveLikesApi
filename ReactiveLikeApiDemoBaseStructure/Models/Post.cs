using System.Text.Json.Serialization;

namespace ReactiveLikeApiDemo.Models
{
    public class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

    }
}