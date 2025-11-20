using TweetService.Models;

namespace TweetService.Services;

public interface ITweetService
{
    // ツイート一覧を取得する
    IEnumerable<Tweet> GetTweets();

    // ツイートを投稿する
    void PostTweet(Tweet tweet);
}