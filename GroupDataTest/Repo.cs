using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GroupDataTest
{
    class Repo
    {
        public  DataTable GetData(string sqlCommand)
        {
            string connectionString;
            DataTable table = new DataTable();


            connectionString = GetConnectionString();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);

            return table;
        }

        string GetConnectionString()
        {
            string path = @"GroupDataTest.txt";
            string line;
            string server = "";
            string instance = "";

            FileInfo file = new FileInfo(path);

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Substring(0, 3) == "Ser")
                            {
                                server = line.Split(' ').Last();
                            }

                            if (line.Substring(0, 3) == "Ins")
                            {
                                instance = line.Split(' ').Last();
                            }

                            if (line.Substring(0, 3) == "Dat")
                            {
                                connectionStringBuilder.InitialCatalog = line.Split(' ').Last();
                            }

                            if (line.Substring(0, 3) == "Use")
                            {
                                connectionStringBuilder.UserID = line.Split(' ').Last();
                            }

                            if (line.Substring(0, 3) == "Pas")
                            {
                                connectionStringBuilder.Password = line.Split(' ').Last();
                            }
                        }
                    }
                    connectionStringBuilder.DataSource = server + @"\" + instance;
                    connectionStringBuilder.IntegratedSecurity = false;
            }           
            return connectionStringBuilder.ConnectionString;           
        }
    }
}
