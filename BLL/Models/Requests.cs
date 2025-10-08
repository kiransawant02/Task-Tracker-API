namespace Task_Tracker_API.BLL.Models
{
    public class Requests
    {
    }

    public class TaskRequest
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AssignedUser { get; set; } = string.Empty;
        public int Status { get; set; } // 0=Pending, 1=In Progress, 2=Completed
        public DateTime? DueDate { get; set; }
    }
}
