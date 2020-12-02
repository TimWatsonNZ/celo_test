using System.Collections.Generic;
using System.Threading.Tasks;
using celo_test.Filters;
using MongoDB.Driver;

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
      var findFilter = Builders<User>.Filter.Where(user => true);

      if (string.IsNullOrEmpty(filter.FirstName)) {
        findFilter = findFilter & Builders<User>.Filter.Eq("firstName", filter.FirstName);
      }

      if (string.IsNullOrEmpty(filter.LastName)) {
        findFilter = findFilter & Builders<User>.Filter.Eq("lastName", filter.LastName);
      }

      var limit = filter.Limit > 0 ? filter.Limit : null;
      var users = _users.Find(findFilter, null).Limit(limit).ToList();

      return users;
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

      return user;
    }
    
  }

}
