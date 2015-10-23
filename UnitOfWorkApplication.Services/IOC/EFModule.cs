using Autofac;
using UnitOfWorkApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Repository;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Model.Repository;

namespace UnitOfWorkApplication_UnitOfWork.IOC
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(UnitOfWorkApplicationDBContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }

    }
}
