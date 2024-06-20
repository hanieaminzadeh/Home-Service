using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeService.Core.Contracts.RequestContracts
{
    public interface IRequestDapperRepository
    {
        Task<List<RequestDto>> GetRequests(CancellationToken cancellationToken);
    }
}
