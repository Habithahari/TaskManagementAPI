
using Microsoft.Data.SqlClient;
using  Dapper;
using Microsoft.Extensions.Configuration;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        

        //for get the tasks by Id
        public async Task<TaskModel> GetTaskById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<TaskModel>(
                "sp_GetTaskById",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //for getting the tasks by userId
        public async Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<TaskModel>(
                "sp_GetTasksByUserId",
                new { UserId = userId },
                commandType: System.Data.CommandType.StoredProcedure);
        }


        //for creating the task in tasks table
        public async Task<int> CreateTask(TaskModel taskModel)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "sp_CreateTask",
                new
                {
                    taskModel.Title,
                    taskModel.Description,
                    taskModel.DueDate,
                    taskModel.IsCompleted,
                    taskModel.CreatedAt,
                    taskModel.UserId
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}