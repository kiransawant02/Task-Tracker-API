using Microsoft.AspNetCore.Authorization;
using Task_Tracker_API.BLL.ApiMethods;
using Task_Tracker_API.BLL.Models;

namespace Task_Tracker_API.BLL.Endpoints
{
    public static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            // group v1 endpoints
            var v1 = app.MapGroup("/v1").WithTags("v1");

            // Get all tasks
            v1.MapGet("/tasks", [AllowAnonymous] (Methods methods, string? dueDate) => methods.GetTasks(dueDate))
              .WithTags("Tasks");

            // Get single task by id
            v1.MapGet("/tasks/{id}", [AllowAnonymous] (Methods methods, int id) => methods.GetTaskById(id))
              .WithTags("Tasks");

            // Add new task
            v1.MapPost("/tasks", [AllowAnonymous] (Methods methods, TaskRequest task) => methods.AddTask(task))
              .WithTags("Tasks");

            // Update task
            v1.MapPut("/tasks/{id}", [AllowAnonymous] (Methods methods, int id, TaskRequest task) => methods.UpdateTask(id, task))
              .WithTags("Tasks");

            // Delete task
            v1.MapDelete("/tasks/{id}", [AllowAnonymous] (Methods methods, int id) => methods.DeleteTask(id))
              .WithTags("Tasks");

            // Change task status
            v1.MapPatch("/tasks/{id}/status", [AllowAnonymous] (Methods methods, int id, int status) => methods.ChangeStatus(id, status))
              .WithTags("Tasks");

        }
    }
}
