using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BabybondPOSInventorySystem
{
    public static class ActivityLogger
    {
        private static string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";

        public static void LogActivity(int accountId, string activity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO tblHistory (AccountId, HistoryRecord, Activities) VALUES (@accId, @date, @activity)", conn);
                cmd.Parameters.AddWithValue("@accId", accountId);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@activity", activity);
                cmd.ExecuteNonQuery();
            }
        }
        


    }
}
