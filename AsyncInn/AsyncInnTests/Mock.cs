using AsyncInn.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncInnTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connections;
        protected readonly AsyncInnDbContext _db;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
