using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model
{
    public class EmailModel
    {
        public string RecipientEmailAddress { get; set; }
        public string RecipientName {get;set;}

        public string RecipientUserName { get; set; }

        public string RecipientPassword { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

    }
}
