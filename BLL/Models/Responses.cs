namespace Task_Tracker_API.BLL.Models
{
    public class Responses
    {
    }
    public class CommonResponse
    {
        public int Result { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class TasksResponse : CommonResponse
    {
        public List<TaskInfo>? Tasks { get; set; }
    }

    public class TaskByIdResponse : CommonResponse
    {
        public TaskInfo? Task { get; set; }
    }

    public class TaskInfo
    {

        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AssignedUser { get; set; } = string.Empty;
        public int Status { get; set; } // 0=Pending, 1=In Progress, 2=Completed
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
