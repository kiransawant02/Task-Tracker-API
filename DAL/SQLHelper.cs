using Microsoft.Data.SqlClient;
using System;
using System.Data; 
using System.Threading.Tasks;

namespace Task_Tracker_API.DAL
{
    /// <summary>
    /// A static helper class for executing SQL Server database operations.
    /// Provides sync and async methods for querying and executing commands.
    /// </summary>
    public static class SQLHelper
    {
        /// <summary>
        /// Connection string to be initialized once at app startup.
        /// </summary>
        public static string connectionString { get; set; }

        #region --- Synchronous Methods ---

        /// <summary>
        /// Executes a query and returns results as a DataSet.
        /// </summary>
        public static DataSet ExecuteDataset(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var adapter = new SqlDataAdapter(command);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            var ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Executes a query and returns results as a DataTable.
        /// </summary>
        public static DataTable ExecuteDataTable(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var adapter = new SqlDataAdapter(command);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Executes Insert/Update/Delete commands and returns affected rows.
        /// </summary>
        public static int ExecuteNonQuery(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a query and returns a single value (first column of first row).
        /// </summary>
        public static object ExecuteScalar(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteScalar();
        }

        #endregion

        #region --- Async Methods ---

        /// <summary>
        /// Executes a query asynchronously and returns results as a DataSet.
        /// </summary>
        public static async Task<DataSet> ExecuteDatasetAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            await connection.OpenAsync();

            using var adapter = new SqlDataAdapter(command);
            var ds = new DataSet();
            await Task.Run(() => adapter.Fill(ds)); // adapter.Fill is synchronous
            return ds;
        }

        /// <summary>
        /// Executes a query asynchronously and returns results as a DataTable.
        /// </summary>
        public static async Task<DataTable> ExecuteDataTableAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            await connection.OpenAsync();

            using var adapter = new SqlDataAdapter(command);
            var dt = new DataTable();
            await Task.Run(() => adapter.Fill(dt)); // Fill is sync
            return dt;
        }

        /// <summary>
        /// Executes Insert/Update/Delete commands asynchronously and returns affected rows.
        /// </summary>
        public static async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Executes a query asynchronously and returns a single value.
        /// </summary>
        public static async Task<object> ExecuteScalarAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);

            command.CommandType = commandType;
            if (parameters?.Length > 0)
                command.Parameters.AddRange(parameters);

            await connection.OpenAsync();
            return await command.ExecuteScalarAsync();
        }

        #endregion
    }
}
