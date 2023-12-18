using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using Zamin.Extensions.Events.Abstractions;
using Zamin.Extensions.Events.PollingPublisher.Dal.Dapper.Options;

namespace Zamin.Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess
{

    public class SqlOutBoxEventItemRepository : IOutBoxEventItemRepository
    {
        private readonly PollingPublisherDalRedisOptions _options;
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<SqlOutBoxEventItemRepository> _logger;

        public SqlOutBoxEventItemRepository(IOptions<PollingPublisherDalRedisOptions> options, ILogger<SqlOutBoxEventItemRepository> logger)
        {
            _options = options.Value;
            _dbConnection = new SqlConnection(_options.ConnectionString);
            _logger = logger;
            _logger.LogInformation("New Instance of SqlOutBoxEventItemRepository Created");
        }
        public List<OutBoxEventItem> GetOutBoxEventItemsForPublishe(int maxCount = 100)
        {
            try
            {
                var result = _dbConnection.Query<OutBoxEventItem>(_options.SelectCommand, new { Count = maxCount }).ToList();
                _logger.LogDebug("{Count} of event fetched for sending from service {ApplicaitonName} at {DateTime}", result.Count, _options.ApplicationName, DateTime.Now);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "fetching events failed in applicaiton {ApplicaitonName}", _options.ApplicationName);
                throw;
            }

        }
        public void MarkAsRead(List<OutBoxEventItem> outBoxEventItems)
        {
            try
            {
                var idForMark = outBoxEventItems.Where(c => c.IsProcessed).Select(c => c.OutBoxEventItemId).ToList();
                if (idForMark != null && idForMark.Any())
                {
                    _dbConnection.Execute(_options.UpdateCommand, new
                    {
                        Ids = idForMark
                    });
                    _logger.LogInformation("{Count} of event marked as processed in service {ApplicaitonName} at {DateTime}. marked ids are {Ids}", outBoxEventItems.Count, _options.ApplicationName, DateTime.Now, idForMark);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Marking events as processed failed in applicaiton {ApplicaitonName}", _options.ApplicationName);
                throw;
            }

        }
    }

}
