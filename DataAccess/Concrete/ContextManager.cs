using System;
using System.Data;
using DataModel.Contexts;

namespace DataAccess.Concrete
{
    public class ContextManager : IDbContextManager, IDisposable
    {
        private static bool _isAlive;
        private static RestorauntDbContext _context;

        public RestorauntDbContext Context
        {
            get
            {
                CheckCtx();
                return _context;
            }
        }

        static ContextManager()
        {
            _context = new RestorauntDbContext();
        }

        private void CheckCtx()
        {
            RefreshCtx();
            OpenConn();
        }
        public void RefreshCtx()
        {
            if (_isAlive == false)
            {
                _context = new RestorauntDbContext();
                _isAlive = true;
            }
        }
        private void OpenConn()
        {
            if (_context.Database.Connection.State == ConnectionState.Closed)
            {
                _context.Database.Connection.Open();
            }
        }

        public void Dispose()
        {
            _isAlive = false;
            Context.Dispose();
        }
    }

    public interface IDbContextManager
    {
        RestorauntDbContext Context { get; }
    }
}
