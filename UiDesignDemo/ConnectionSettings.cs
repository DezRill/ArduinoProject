using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesignDemo
{
    [Serializable]
    public class ConnectionSettings
    {
        public string DataSource { set; get; }
        public string InitialCatalog { set; get; }
        public string UserID { set; get; }
        public string Password { set; get; }
    }
}