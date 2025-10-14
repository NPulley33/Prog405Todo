using Todo.Common.Models;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    public interface ITaskService
    {
        Task<Result<string>> CreateTaskAsync(CreateTaskRequest request);
    }

    public class TaskService : ITaskService
    {
        private readonly IFileDataService fileDataService;

        public TaskService(IFileDataService fileDataService)
        {
            this.fileDataService = fileDataService;
        }
        
        public async Task<Result<string>> CreateTaskAsync(CreateTaskRequest request)
        {
            // request -> model

            var modelResult = TaskModel.CreateTask(request);
            if (modelResult.IsErr())
            {
                return Result<string>.Err(modelResult.GetErr());
            }
            var model = modelResult.GetVal();
            if ( model is null)
            {
                return Result<string>.Err("No Model");
            }

            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result<string>.Ok(model.Key);
            
        }

        public async Task<Result<string>> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var modelResult = TaskModel.UpdateTask(request);
            if (modelResult.IsErr())
            {
                return Result<string>.Err(modelResult.GetErr());
            }

            var model = modelResult.GetVal();
            if (model is null)
            {
                return Result<string>.Err("No Model");
            }

            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result<string>.Ok(model.Key);
        }

        public async Task<TaskModel?> GetAsync(string key)
        {
            return await this.fileDataService.GetAsync(key);
        }


    }
}
