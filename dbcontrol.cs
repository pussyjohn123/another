using System;
using MySql.Data.MySqlClient;
namespace cnc_gui
{
    public class DatabaseModel
    {
        public class PlcControl
        {
            public int AddressId { get; set; }
            public string AddressType { get; set; }
            public int Handl { get; set; }
            public int Address { get; set; }
            public int Decimal { get; set; }
        }
        public class FlusherTimeSetting
        {
            public int ImageProcessingTime { get; set; }
            public int TotalFlusherTime { get; set; }
            public int DelayTime { get; set; }
            public int Total_process_time { get; set; }
        }
        public class LevelSet
        {
            public int LevelId { get; set; }
            public string LevelType { get; set; }
            public int Level1 { get; set; }
            public int Level2 { get; set; }
            public int Level3 { get; set; }
            public int Level4 { get; set; }
        }
        public class Ip_port
        {
            public string Cncip { get; set; }
            public string Cncport { get; set; }
        }
        public class Level_result
        {
            public int flusher_level_result { get; set; }
            public int excluder_level_result { get; set; }
        }
        private string connectionString = "server=localhost;database=cnc_db;user=root;password=ncutncut;";
        public LevelSet GetLevelSetById(int levelId)
        {
            LevelSet levelSet = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Level_id, Level_type, Level_1, Level_2, Level_3, Level_4 FROM Level_set WHERE Level_id = @LevelId;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@LevelId", levelId);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        levelSet = new LevelSet
                        {
                            LevelId = Convert.ToInt32(reader["Level_id"]),
                            LevelType = reader["Level_type"].ToString(),
                            Level1 = Convert.ToInt32(reader["Level_1"]),
                            Level2 = Convert.ToInt32(reader["Level_2"]),
                            Level3 = Convert.ToInt32(reader["Level_3"]),
                            Level4 = Convert.ToInt32(reader["Level_4"])
                        };
                    }
                }
            }

            return levelSet;
        }

        //依序輸入要修改哪一筆資料，哪一欄以及要寫進的參數
        public void UpdateLevelSet(int levelId, string columnName, object newValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"UPDATE Level_set SET {columnName} = @newValue WHERE Level_id = @levelId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@LevelId", levelId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public PlcControl GetPlcControlById(int addressId)
        {
            PlcControl plcControl = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Address_id, Address_type, Handl, Address, Decimal FROM Plc_control WHERE Address_id = @AddressId;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        plcControl = new PlcControl
                        {
                            AddressId = Convert.ToInt32(reader["Address_id"]),
                            AddressType = reader["Address_type"].ToString(),
                            Handl = Convert.ToInt32(reader["Handl"]),
                            Address = Convert.ToInt32(reader["Address"]),
                            Decimal = Convert.ToInt32(reader["Decimal"])
                        };
                    }
                }
            }

            return plcControl;
        }

        //依序輸入要修改哪一筆資料，哪一欄以及要寫進的參數
        public void UpdatePlcControl(int addressId, string columnName, object newValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"UPDATE Plc_control SET {columnName} = @newValue WHERE Address_id = @AddressId";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FlusherTimeSetting GetFlusherTimeSetting()
        {
            FlusherTimeSetting flusherTimeSetting = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Image_processing_time, Total_flusher_time, Delay_time, Total_process_time FROM Flusher_time_setting;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        flusherTimeSetting = new FlusherTimeSetting
                        {
                            ImageProcessingTime = Convert.ToInt32(reader["Image_processing_time"]),
                            TotalFlusherTime = Convert.ToInt32(reader["Total_flusher_time"]),
                            DelayTime = Convert.ToInt32(reader["Delay_time"]),
                            Total_process_time = Convert.ToInt32(reader["Total_process_time"]),
                        };
                    }
                }
            }

            return flusherTimeSetting;
        }

        //輸入要修改的欄位以及參數
        public void UpdateFlusherTimeSetting(string columnName, object newValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"UPDATE Flusher_time_setting SET {columnName} = @newValue";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Ip_port GetIp_Port()
        {
            Ip_port ip_port = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Cncip, Cncport FROM Ip_port;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ip_port = new Ip_port
                        {
                            Cncip = reader["Cncip"].ToString(),
                            Cncport = reader["Cncport"].ToString()
                        };
                    }
                }
            }

            return ip_port;
        }
        //輸入要修改的欄位以及參數
        public void UpdateIpPort(string columnName, object newValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"UPDATE Ip_port SET {columnName} = @newValue";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Level_result GetLevelResult()
        {
            Level_result level_result = null;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Flusher_level_result, Excluder_level_result FROM Level_result;";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        level_result = new Level_result
                        {
                            flusher_level_result = Convert.ToInt32(reader["Flusher_level_result"]),
                            excluder_level_result = Convert.ToInt32(reader["Excluder_level_result"])
                        };
                    }
                }
            }

            return level_result;
        }
        //輸入要修改的欄位以及參數
        public void UpdateLevelResult(string columnName, object newValue)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"UPDATE Level_result SET {columnName} = @newValue";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
