using System.Collections.Generic;
using System.Threading.Tasks; // Task を使うために必要
using TweetService.Models;

namespace TweetService.Repositories;

public interface ITweetRepository
{
    // 💡 修正点: 非同期実装に合わせるため Task<IEnumerable<Tweet>> に変更
    Task<IEnumerable<Tweet>> GetAll();
    
    // 💡 修正点: 非同期実装に合わせるため Task<Tweet?> に変更
    Task<Tweet?> GetById(string id);
    
    // 💡 修正点: 非同期実装に合わせるため Task に変更 (戻り値なしの非同期)
    Task Add(Tweet tweet);
}