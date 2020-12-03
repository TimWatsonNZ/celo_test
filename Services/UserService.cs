using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celo_test.Filters;
using celo_test.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace celo_test.Services {
  public class UserService {
    private readonly IMongoCollection<User> _users;
    
    public UserService(IUserDatabaseSettings settings) 
    {
      var client = new MongoClient(settings.ConnectionString);
      var database = client.GetDatabase(settings.DatabaseName);

      _users = database.GetCollection<User>(settings.UsersCollectionName);
    }

    public List<User> Get(UserFilter filter) {
      var query = _users.AsQueryable();

      if (!string.IsNullOrEmpty(filter.FirstName)) {
        query = query.Where(user => user.firstName.ToLower() == filter.FirstName.ToLower());
      }

      if (!string.IsNullOrEmpty(filter.LastName)) {
        query = query.Where(user => user.lastName.ToLower() == filter.LastName.ToLower());
      }

      if (filter.Limit.HasValue) {
        query = query.Take(filter.Limit.Value);
      }

      return query.ToList();
    }

    public User Insert(User user) {
      _users.InsertOne(user);
      return user;
    }

    public User Get(string id) {
      var user = _users.Find(user => user.id == id).FirstOrDefault();
      return user;
    }

    public async Task<User> Update(User user) {
      var filter = Builders<User>.Filter.Eq("id", user.id);
      var update = Builders<User>.Update
        .Set("firstName", user.firstName)
        .Set("lastName", user.lastName)
        .Set("email", user.email)
        .Set("title", user.title)
        .Set("dateOfBirth", user.dateOfBirth)
        .Set("phoneNumber", user.phoneNumber)
        .Set("imageUrl", user.imageUrl)
        .Set("thumbnailUrl", user.thumbnailUrl);
      
      var result = await _users.UpdateOneAsync(filter, update);

      if (result.ModifiedCount > 0) {
        return user;
      }
      return null;
    }

    public bool Delete(string id) {
      var result = _users.DeleteOne(user => user.id == id);
      
      return result.DeletedCount > 0;
    }
    
  }

}
