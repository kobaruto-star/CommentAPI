using TweetService.Models;
using TweetService.Repositories;

namespace TweetService.Services;

public class TweetService : ITweetService
{
    // 倉庫番（インターフェース）を持っておく変数
    private readonly ITweetRepository _repository;

    // 【重要】コンストラクタ
    // 「ITweetServiceを作る時は、誰か ITweetRepository（倉庫番）を持ってきてくれ！」と要求する
    public TweetService(ITweetRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Tweet> GetTweets()
    {
        // 倉庫番に「全部取ってきて」と指示する
        return _repository.GetAll();
    }

    public void PostTweet(Tweet tweet)
    {
        // ここでビジネスロジック（チェック処理など）を入れることが多い
        // 例: 「中身が空っぽならエラーにする」など
        if (string.IsNullOrEmpty(tweet.Content))
        {
            throw new ArgumentException("つぶやきの中身がないよ！");
        }

        // 問題なければ倉庫番に「保存しておいて」と渡す
        _repository.Add(tweet);
    }
}