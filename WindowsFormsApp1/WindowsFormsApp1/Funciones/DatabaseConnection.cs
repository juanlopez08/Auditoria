using System.Data.SqlClient;



namespace WindowsFormsApp1.Funciones
{
    class DatabaseConnection
    {
        string connectionString;
        SqlConnection cnn;
        public bool connect(string [] data)
        {
            foreach (string param in data)
                if (checkValue(param) || param.Contains("'"))
                    return false;

            connectionString = $"Data Source=localhost;Initial Catalog={data[0]};User ID={data[1]};Password={data[2]}";
            cnn = new SqlConnection(connectionString);
           try
            {
                cnn.Open();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        private bool checkValue(string value)
        {
            //We can implement a proper validation sometime in the future...
            return value.Length == 0;
        }
        public SqlConnection getConnection()
        {
            return cnn;
        }
        public void openConnection()
        {
            cnn.Open();
        }
        public void closeConnection()
        {
            cnn.Close();
        }
    }
}
