using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Services.Interfaces
{
    public interface ICommunicationService
    {
        void SendMail(User user);       
    }
}
