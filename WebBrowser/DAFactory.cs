using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    abstract public class DAFactory
    {
        abstract public IKufang GetKufangInfo();
    }

    public class DAProxy
    {
        private static readonly DAFactory daFactory = new SQLFactory();

        public static DAFactory Factory
        {
            get { return daFactory; }
        }
    }
}
