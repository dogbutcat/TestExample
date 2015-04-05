using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    public class BLL_Kufang
    {
        IKufang manage = DAProxy.Factory.GetKufangInfo();

        public DataTable GetKufangInfo()
        {
            return manage.SelectKufang();
        }
    }
}
