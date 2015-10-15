using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface ICountryService : IEntityService<Country>
    {
        Country GetById(int Id);
    }
}
