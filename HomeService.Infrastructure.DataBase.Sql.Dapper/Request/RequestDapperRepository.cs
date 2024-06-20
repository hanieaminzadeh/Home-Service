using Dapper;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Core.Entities.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace HomeService.Infrastructure.DataBase.Sql.Dapper.Repositories;

public class RequestDapperRepository : IRequestDapperRepository
{
    private readonly SiteSettings _siteSettings;
public RequestDapperRepository(SiteSettings? siteSettings)
        {
            _siteSettings = siteSettings;
        }

    public async Task<List<RequestDto>> GetRequests(CancellationToken cancellationToken)
    {
        using (IDbConnection db = new SqlConnection(_siteSettings.ConnectionStrings.AppConnectionString))
        {
            return (List<RequestDto>)await db.QueryAsync<RequestDto>("SELECT * FROM Request");
        }
    }
}
