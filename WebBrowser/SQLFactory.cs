using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    public class SQLFactory:DAFactory
    {

        public override IKufang GetKufangInfo()
        {
            return new SQL_Kufang();
        }
    }
}
