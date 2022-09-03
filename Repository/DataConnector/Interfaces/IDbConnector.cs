using System;
using System.Data;

namespace Odevez.Repository.DataConnector
{
    public interface IDbConnector : IDisposable
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
    }
}
