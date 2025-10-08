using Task_Tracker_API.BLL.Models;
using Task_Tracker_API.BLL.Utility;
using Task_Tracker_API.DAL;
using System.Data;

namespace Task_Tracker_API.BLL.ApiMethods
{
    public class Methods
    {
        private readonly SQLLogic _sql;
        private const string _defaultErrorMsg = "Some error occurred.";

        public Methods(SQLLogic sqlLogic)
        {
            _sql = sqlLogic;
        }

        public TasksResponse GetTasks(string dueDate)
        {
            TasksResponse response = new TasksResponse();
            List<TaskInfo> taskList = new List<TaskInfo>();
            try
            {
                dueDate = !string.IsNullOrEmpty(dueDate) && DateTime.TryParse(dueDate, out DateTime parsedDate)
                        ? parsedDate.ToString("yyyy-MM-dd")
                        : "";
                DataSet ds = _sql.GetTasks(dueDate);
                if (Utils.IsValidDataSet(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        taskList.Add(new TaskInfo
                        {
                            TaskId = Utils.ConvertToInt(dr["task_id"]),
                            Title = dr["title"].ToString() ?? string.Empty,
                            Description = dr["description"].ToString() ?? string.Empty,
                            AssignedUser = dr["assigned_user"].ToString() ?? string.Empty,
                            Status = Utils.ConvertToInt(dr["status"]),
                            DueDate = dr["due_date"] == DBNull.Value ? null : (DateTime?)dr["due_date"],
                            CompletedAt = dr["completed_at"] == DBNull.Value ? null : (DateTime?)dr["completed_at"]
                        });
                    }
                }
                else
                {
                    response.Message = "No tasks found.";
                }
                response.Result = 1;
                response.Tasks = taskList;

            }
            catch (Exception ex)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

        // Get task by Id
        public TaskByIdResponse GetTaskById(int id)
        {
            TaskByIdResponse response = new TaskByIdResponse();
            TaskInfo task = null;
            try
            {
                DataSet ds = _sql.GetTaskById(id);
                if (Utils.IsValidDataSet(ds) && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    task = new TaskInfo
                    {
                        TaskId = Utils.ConvertToInt(dr["task_id"]),
                        Title = dr["title"].ToString() ?? string.Empty,
                        Description = dr["description"].ToString() ?? string.Empty,
                        AssignedUser = dr["assigned_user"].ToString() ?? string.Empty,
                        Status = Utils.ConvertToInt(dr["status"]),
                        DueDate = dr["due_date"] == DBNull.Value ? null : (DateTime?)dr["due_date"],
                        CompletedAt = dr["completed_at"] == DBNull.Value ? null : (DateTime?)dr["completed_at"]
                    };
                    response.Result = 1;
                }
                else
                {
                    response.Result = 2;
                    response.Message = "Task not found.";
                }
                response.Task = task;
            }
            catch (Exception)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

        // Add new task
        public CommonResponse AddTask(TaskRequest task)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                bool isAdded = _sql.AddTask(task);
                response.Result = isAdded ? 1 : 2;
                response.Message = isAdded ? "Task added successfully." : "Failed to add task.";
            }
            catch (Exception)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

        // Update existing task
        public CommonResponse UpdateTask(int id, TaskRequest task)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                bool isUpdated = _sql.UpdateTask(id, task);
                response.Result = isUpdated ? 1 : 2;
                response.Message = isUpdated ? "Task updated successfully." : "Failed to update task.";
            }
            catch (Exception)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

        // Delete task
        public CommonResponse DeleteTask(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                bool isDeleted = _sql.DeleteTask(id);
                response.Result = isDeleted ? 1 : 2;
                response.Message = isDeleted ? "Task deleted successfully." : "Failed to delete task.";
            }
            catch (Exception)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

        // Change task status
        public CommonResponse ChangeStatus(int id, int status)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                bool isUpdated = _sql.UpdateTaskStatus(id, status);
                response.Result = isUpdated ? 1 : 2;
                response.Message = isUpdated ? "Status updated successfully." : "Failed to update status.";
            }
            catch (Exception)
            {
                response.Result = 3;
                response.Message = _defaultErrorMsg;
            }
            return response;
        }

    }
}
