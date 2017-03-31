﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using SandboxCore.Identity.Dapper.Connections;
using System.Threading;

namespace SandboxCore.Identity.Dapper.UnitOfWork
{
    public class UnitOfWork : SandboxCore.Identity.Dapper.UnitOfWork.Contracts.IUnitOfWork
    {
        private DbConnection _connection;
        private DbTransaction _transaction;

        private readonly IConnectionProvider _connectionProvider;
        private readonly SemaphoreSlim _semaphore;
        public UnitOfWork(IConnectionProvider connProvider)
        {
            _connectionProvider = connProvider;
            _semaphore = new SemaphoreSlim(1);
        }

        public DbTransaction Transaction => _transaction;

        public DbConnection Connection => _connection;

        public void CommitChanges() => _transaction?.Commit();

        public DbConnection CreateOrGetConnection()
        {
            _semaphore.Wait();

            if (_connection == null)
            {
                _connection = _connectionProvider.Create();
                _connection.Open();

                _transaction = _connection.BeginTransaction();
            }

            _semaphore.Release();

            return _connection;
        }

        public void DiscardChanges() => _transaction?.Rollback();

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}