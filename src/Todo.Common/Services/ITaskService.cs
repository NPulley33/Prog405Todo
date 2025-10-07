using Todo.Common.Models;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    public interface ITaskService
    {
        Task<Result> CreateTaskAsync(CreateTaskRequest request);
    }

    public class TaskService : ITaskService
    {
        private readonly IFileDataService fileDataService;

        public TaskService(IFileDataService fileDataService)
        {
            this.fileDataService = fileDataService;
        }
        
        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            // request -> model

            var modelResult = TaskModel.CreateTask(request);
            if (modelResult.IsErr())
            {
                return Result.Err(modelResult.GetErr());
            }

            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result.Ok();
            
        }
    }
}
