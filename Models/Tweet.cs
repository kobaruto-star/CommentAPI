using Newtonsoft.Json;

namespace TweetService.Models;

public class Tweet
{
    // int から string に変更！
    // Guid.NewGuid().ToString() で、自動的に "a1b2-c3d4..." みたいな文字列を作ります
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Content { get; set; }
    public string? Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now; // ついでに日付も自動で入るようにしておきましょう
}