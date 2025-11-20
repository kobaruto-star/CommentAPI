using Microsoft.AspNetCore.Mvc;
using TweetService.Models;
using TweetService.Services;

namespace TweetService.Controllers;

[ApiController]            // 「これはWeb API用のコントローラですよ」という宣言
[Route("api/[controller]")] // URLのルールを決めます。この場合 "api/tweets" になります
public class TweetsController : ControllerBase
{
    private readonly ITweetService _service;

    // コンストラクタ：Service（司令塔）を受け取る
    public TweetsController(ITweetService service)
    {
        _service = service;
    }

    // GET: api/tweets
    // ブラウザやアプリから「GETリクエスト」が来たらここが動く
    [HttpGet]
    public IActionResult Get()
    {
        // Serviceに「全部くれ」と頼む
        var tweets = _service.GetTweets();
        
        // HTTP 200 OK と共にデータをJSONで返す
        return Ok(tweets);
    }

    // POST: api/tweets
    // 「POSTリクエスト（データの登録）」が来たらここが動く
    [HttpPost]
    public IActionResult Post(Tweet tweet)
    {
        try
        {
            // Serviceに「登録してくれ」と頼む
            _service.PostTweet(tweet);
            
            // 成功したら HTTP 200 OK を返す
            return Ok(new { Message = "保存しました！", Data = tweet });
        }
        catch (ArgumentException ex)
        {
            // データがおかしい場合（空っぽなど）は HTTP 400 Bad Request を返す
            return BadRequest(ex.Message);
        }
    }
}