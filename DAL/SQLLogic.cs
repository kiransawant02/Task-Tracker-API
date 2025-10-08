using Microsoft.Data.SqlClient;
using System.Data;
using Task_Tracker_API.BLL.Models;

namespace Task_Tracker_API.DAL
{
    public class SQLLogic
    {
        public DataSet GetTasks(string dueDate)
        {
            string dateFilter = string.IsNullOrEmpty(dueDate) ? "" : $" WHERE CAST(due_date AS DATE) = CAST('{dueDate}' AS DATE) ";
            string query = $@"select 
	                            task_id, title, description, assigned_user, status, due_date, completed_at
                            from 
	                            tasks
                            {dateFilter}";
            return SQLHelper.ExecuteDataset(query, CommandType.Text);
        }

        public DataSet GetTaskById(int taskId)
        {
            string query = @"select 
	                            task_id, title, description, assigned_user, status, due_date, completed_at
                            from 
	                            tasks
                            where 
                                task_id = @task_id;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@task_id", taskId)
            };
            return SQLHelper.ExecuteDataset(query, CommandType.Text, parameters);
        }

        public bool AddTask(TaskRequest task)
        {
            string query = @"AddTask";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@title", task.Title),
                new SqlParameter("@description", task.Description),
                new SqlParameter("@assigned_user", task.AssignedUser),
                new SqlParameter("@status", task.Status),
                new SqlParameter("@due_date", (object?)task.DueDate ?? DBNull.Value)
            };
            int rowsAffected = SQLHelper.ExecuteNonQuery(query, CommandType.StoredProcedure, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateTask(int taskId, TaskRequest task)
        {
            string query = @"UpdateTask";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", taskId),
                new SqlParameter("@title", task.Title),
                new SqlParameter("@description", task.Description),
                new SqlParameter("@assigned_user", task.AssignedUser),
                new SqlParameter("@status", task.Status),
                new SqlParameter("@due_date", (object?)task.DueDate ?? DBNull.Value)
            };
            int rowsAffected = SQLHelper.ExecuteNonQuery(query, CommandType.StoredProcedure, parameters);
            return rowsAffected > 0;
        }

        public bool DeleteTask(int taskId)
        {
            string query = @"DeleteTask";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", taskId)
            };
            int rowsAffected = SQLHelper.ExecuteNonQuery(query, CommandType.StoredProcedure, parameters);
            return rowsAffected > 0;
        }

        public bool UpdateTaskStatus(int taskId, int status)
        {
            string query = @"UpdateTaskStatus";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@taskId", taskId),
                new SqlParameter("@status", status)
            };
            int rowsAffected = SQLHelper.ExecuteNonQuery(query, CommandType.StoredProcedure, parameters);
            return rowsAffected > 0;
        }

    }
}
