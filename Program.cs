using TweetService.Services;
using TweetService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// 1. サービスの登録（DIコンテナへの登録）
// =========================================================

builder.Services.AddControllers();

// ★変更点：ここを「SwaggerGen」に変えることで、画面を作れるようになります
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DIの配線（ここはそのまま）
builder.Services.AddSingleton<ITweetRepository, CosmosTweetRepository>();
builder.Services.AddScoped<ITweetService, TweetService.Services.TweetService>();

var app = builder.Build();

// =========================================================
// 2. アプリの動作設定 (パイプライン)
// =========================================================

if (app.Environment.IsDevelopment())
{
    // ★変更点：ここを「SwaggerUI」に変えることで、ブラウザで画面が見れます
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();