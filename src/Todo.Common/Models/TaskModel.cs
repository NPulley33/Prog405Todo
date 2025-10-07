using Todo.Common.Requests;

namespace Todo.Common.Models
{
    public class TaskModel
    {
        private TaskModel()
        {
            //Must:
            //exist
            this.Key = string.Empty;
            //Must:
            //exist
            this.Name = string.Empty;
            //Optional
            this.Description = string.Empty;
            //Must:
            //be in the future & exist
            this.DueDate = DateTime.MinValue.ToUniversalTime(); //add time zone otherwise fail (or weird things happen)
        }
        public string Key {get; private set;}
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }

        public static Result<TaskModel> CreateTask(CreateTaskRequest request)
        {
            //validate request / current system state (in request)
            var validationResult = request.IsValid();
            if(validationResult.IsErr())
            {
                return Result<TaskModel>.Err(validationResult.GetErr());
            }

            return Result<TaskModel>.Ok(new TaskModel
            {
                //reasonable key, trillion of ints rand generated
                Key = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate,
            });
        }
    }
}
