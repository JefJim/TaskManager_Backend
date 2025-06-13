// Services/TaskService.cs
using MongoDB.Driver;
using TaskManager_Backend.Models;

namespace TaskManager_Backend.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<TaskItem> _taskCollection;

        public TaskService(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoDB:ConnectionString").Value);
            var database = client.GetDatabase(config.GetSection("MongoDB:Database").Value);
            _taskCollection = database.GetCollection<TaskItem>("Tasks");
        }

        public async Task<List<TaskItem>> GetAllAsync() =>
            await _taskCollection.Find(_ => true).ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(string id) =>
            await _taskCollection.Find(t => t.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TaskItem task) =>
            await _taskCollection.InsertOneAsync(task);

        public async Task<bool> UpdateAsync(string id, TaskItem updatedTask)
        {
            var result = await _taskCollection.ReplaceOneAsync(t => t.Id == id, updatedTask);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _taskCollection.DeleteOneAsync(t => t.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
