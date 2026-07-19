using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using ConferenceBooking.Api.DTOs.Analytics;
using ConferenceBooking.Api.Models.Analytics;
using ConferenceBooking.Api.Models.Analytics.DTO;
using ConferenceBooking.Api.Repository.Interfaces;

namespace ConferenceBooking.Api.Repository;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly ClickHouseConnection _connection;

    public AnalyticsRepository(ClickHouseConnection connection)
    {
        _connection = connection;
    }

    public async Task SaveBookingAsync(BookingAnalytics booking)
    {
        try
        {
            await _connection.OpenAsync();

            await using var command = _connection.CreateCommand();

            command.CommandText =
            """
            INSERT INTO booking_analytics
            (
                BookingId,
                HallId,
                HallName,
                StartTime,
                EndTime,
                TotalPrice,
                ServiceCount,
                CreatedAt
            )
            VALUES
            (
                @BookingId,
                @HallId,
                @HallName,
                @StartTime,
                @EndTime,
                @TotalPrice,
                @ServiceCount,
                @CreatedAt
            )
            """;

            command.AddParameter("BookingId", booking.BookingId);
            command.AddParameter("HallId", booking.HallId);
            command.AddParameter("HallName", booking.HallName);
            command.AddParameter("StartTime", booking.StartTime);
            command.AddParameter("EndTime", booking.EndTime);
            command.AddParameter("TotalPrice", booking.TotalPrice);
            command.AddParameter("ServiceCount", booking.ServiceCount);
            command.AddParameter("CreatedAt", booking.CreatedAt);

            await command.ExecuteNonQueryAsync();
        }
        catch
        {
            throw;
        }
        finally
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                await _connection.CloseAsync();
            }
        }
    }

    public async Task<RevenueReportResponse> GetRevenueReportAsync()
    {
        try
        {
            await _connection.OpenAsync();

            await using var command = _connection.CreateCommand();

            command.CommandText =
            """
            SELECT
                SUM(TotalPrice) AS TotalRevenue,
                COUNT() AS BookingCount,
                AVG(TotalPrice) AS AverageBookingPrice
            FROM booking_analytics
            """;

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new RevenueReportResponse
                {
                    TotalRevenue = reader.IsDBNull(0)
                        ? 0
                        : reader.GetDecimal(0),

                    BookingCount = reader.IsDBNull(1)
                        ? 0
                        : reader.GetInt32(1),

                    AverageBookingPrice = reader.IsDBNull(2)
                        ? 0
                        : reader.GetDecimal(2)
                };
            }

            return new RevenueReportResponse();
        }
        catch
        {
            throw;
        }
        finally
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                await _connection.CloseAsync();
            }
        }
    }
    public async Task<IEnumerable<PopularHallResponse>> GetPopularHallsAsync()
    {
        try
        {
            await _connection.OpenAsync();

            await using var command = _connection.CreateCommand();

            command.CommandText =
            """
        SELECT
            HallName,
            COUNT() AS BookingCount,
            SUM(TotalPrice) AS TotalRevenue
        FROM booking_analytics
        GROUP BY HallName
        ORDER BY BookingCount DESC
        """;

            await using var reader = await command.ExecuteReaderAsync();

            var result = new List<PopularHallResponse>();

            while (await reader.ReadAsync())
            {
                result.Add(new PopularHallResponse
                {
                    HallName = reader.GetString(0),

                    BookingCount = reader.IsDBNull(1)
                        ? 0
                        : reader.GetInt32(1),

                    TotalRevenue = reader.IsDBNull(2)
                        ? 0
                        : reader.GetDecimal(2)
                });
            }

            return result;
        }
        catch
        {
            throw;
        }
        finally
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                await _connection.CloseAsync();
            }
        }
    }
    public async Task<IEnumerable<DailyRevenueResponse>> GetDailyRevenueAsync()
    {
        try
        {
            await _connection.OpenAsync();

            await using var command = _connection.CreateCommand();

            command.CommandText =
            """
        SELECT
            toDate(CreatedAt) AS ReportDate,
            SUM(TotalPrice) AS TotalRevenue,
            COUNT() AS BookingCount
        FROM booking_analytics
        GROUP BY ReportDate
        ORDER BY ReportDate
        """;

            await using var reader = await command.ExecuteReaderAsync();

            var result = new List<DailyRevenueResponse>();

            while (await reader.ReadAsync())
            {
                result.Add(new DailyRevenueResponse
                {
                    Date = reader.GetDateTime(0),

                    TotalRevenue = reader.IsDBNull(1)
                        ? 0
                        : reader.GetDecimal(1),

                    BookingCount = reader.IsDBNull(2)
                        ? 0
                        : reader.GetInt32(2)
                });
            }

            return result;
        }
        catch
        {
            throw;
        }
        finally
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                await _connection.CloseAsync();
            }
        }
    }
}