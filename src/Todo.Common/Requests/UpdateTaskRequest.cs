using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.Common.Requests
{
    public class UpdateTaskRequest
    {
        public CreateTaskRequest UpdatedTask { get; set; }
        public string Key { get; private set; }

        public UpdateTaskRequest(string key, CreateTaskRequest replacement) 
        { 
            UpdatedTask = replacement;
            Key = key;
        }

        public Result IsValid()
        {
            //validate request / current system state (in request)
            if (string.IsNullOrWhiteSpace(UpdatedTask.Name)) Result.Err("Name Required");
            if (string.IsNullOrWhiteSpace(Key)) Result.Err("Key Required");
            if (UpdatedTask.DueDate <= DateTime.UtcNow) Result.Err("Due Date must be in the future");
            return Result.Ok();
        }

    }
}
