using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Services
{
    public interface IMailService
    {
        bool SendMail(string to, string from, string subject, string body);
    }
}
