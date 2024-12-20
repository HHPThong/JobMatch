﻿using FPTJobMatch.Data;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDBContext _dbContext;
		public ICategoryRepository CategoryRepository { get; private set; }
		public IJobRepository JobRepository { get; private set; }
		public IAppUserRepository AppUserRepository { get; private set; }
		public IApplicationJobRepository ApplicationJobRepository { get; private set; }
		public UnitOfWork(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
			CategoryRepository = new CategoryRepository(dbContext);
			JobRepository = new JobRepository(dbContext);
			ApplicationJobRepository = new ApplicationJobRepository(dbContext);
			AppUserRepository = new AppUserRepository(dbContext);
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
