using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class DashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DashBoardModel> GetDashboardDataAsync()
        {
            var dashboardData = new DashBoardModel
            {
                Counts = new List<DashboardCounts>(),
                RecentOrders = new List<Inventory>()
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("usp_GetDashboardData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Fetch counts
                        while (await reader.ReadAsync())
                        {
                            dashboardData.Counts.Add(new DashboardCounts
                            {
                                Metric = reader["Metric"].ToString(),
                                Value = Convert.ToInt32(reader["Value"])
                            });
                        }

                        // Fetch inventory details (recent orders)
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                dashboardData.RecentOrders.Add(new Inventory
                                {
                                    InventoryID = Convert.ToInt32(reader["InventoryID"]),
                                    ItemName = reader["ItemName"].ToString(),
                                    QuantityAvailable = Convert.ToDouble(reader["QuantityAvailable"]),
                                    QuantityWanted = Convert.ToDouble(reader["QuantityWanted"])
                                });
                            }
                        }
                    }
                }
            }
            return dashboardData;
        }
    }