using SeminarskaPraksa.AsyncTasks;
using System.Data.SqlClient;
using System.Text;

namespace SqlModule
{
    public class SqlAsyncTask : IAsyncTask
    {
        private readonly StringBuilder _sb;
        private readonly string _connectionString;

        public SqlAsyncTask(string connectionString)
        {
            _sb = new StringBuilder();
            _connectionString = connectionString;
        }

        public async Task<string> RunAsync(string querry)
        {
            var safetycheck = Safetycheck(querry);
            if (!string.IsNullOrEmpty(safetycheck))
                return safetycheck;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(querry, connection);
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                    _sb.Append($"{reader[i].ToString()}, "); // Append each column's value to the StringBuilder
                                if (_sb.Length > 0)
                                    _sb.Length -= 2; //remove ', ' from the end of the line
                                _sb.AppendLine(); //add line at the end
                            }
                        }
                        else
                            return ("No rows found.");
                    }
                }
                catch (Exception ex)
                {
                    return ($"An error occurred: {ex.Message}");
                }
            }
            var result = _sb.ToString();
            _sb.Clear();
            return result;
        }

        private string Safetycheck(string querry)
        {
            if (string.IsNullOrEmpty(_connectionString))
                return "No connection string";
            if (string.IsNullOrEmpty(querry))
                return "no querry provided";
            return string.Empty;
        }
    }
}
