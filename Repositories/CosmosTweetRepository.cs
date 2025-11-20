using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using TweetService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq; // List<T>のToList()を使うために必要

namespace TweetService.Repositories;
public class CosmosTweetRepository : ITweetRepository
{  
    private const string DatabaseId = "TweetDB";
    private const string ContainerId = "Tweets";
    
    private readonly Container _container;
    public CosmosTweetRepository(IConfiguration configuration)
    {

        var connectionString = configuration["CosmosDB:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Cosmos DB の接続文字列が設定されていません。");
        }

        var client = new CosmosClient(connectionString);
        var database = client.GetDatabase(DatabaseId);
        _container = database.GetContainer(ContainerId);
    }

    public async Task<IEnumerable<Tweet>> GetAll()
    {
        var query = new QueryDefinition("SELECT * FROM c");
        var iterator = _container.GetItemQueryIterator<Tweet>(query);
        var results = new List<Tweet>();
        
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task<Tweet?> GetById(string id)
    {
        try
        {
            ItemResponse<Tweet> response = await _container.ReadItemAsync<Tweet>(
                id,
                new PartitionKey(id)
            );
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }



    public async Task Add(Tweet tweet)
    {
        await _container.CreateItemAsync(tweet, new PartitionKey(tweet.Id));
    }
}