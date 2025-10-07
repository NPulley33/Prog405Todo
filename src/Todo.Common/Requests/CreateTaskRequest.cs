namespace Todo.Common.Requests
{
    public class CreateTaskRequest
    {
        public CreateTaskRequest(string name, string description, DateTime dueDate)
        {
            this.Name = name;
            this.Description = description;
            this.DueDate = dueDate;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime DueDate { get; }

        public Result IsValid()
        {
            //validate request / current system state (in request)
            if (string.IsNullOrWhiteSpace(this.Name)) Result.Err("Name Required");
            if (this.DueDate <= DateTime.UtcNow) Result.Err("Due Date must be in the future");
            return Result.Ok();
        }
    }
}
