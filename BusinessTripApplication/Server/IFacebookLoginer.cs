using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.Server
{
    public interface IFacebookLoginer
    {
        object SetLoginUrl(Uri redirectUri);
    }
}
