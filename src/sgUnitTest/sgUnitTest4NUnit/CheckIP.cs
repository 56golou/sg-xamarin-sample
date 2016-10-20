using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgUnitTest
{
    // IPをチェックするクラス
    public class CheckIP
    {
        private int _ip1 = 0;
        private int _ip2 = 0;
        private int _ip3 = 0;
        private int _ip4 = 0;

        public int IP1 { get { return _ip1; } }
        public int IP2 { get { return _ip2; } }
        public int IP3 { get { return _ip3; } }
        public int IP4 { get { return _ip4; } }
        public override string ToString()
        {
            return $"{_ip1}.{_ip2}.{_ip3}.{_ip4}";
        }
        public bool Check( string ipaddr )
        {
            var ips = ipaddr.Split(new string[]{ "." }, StringSplitOptions.None);
            _ip1 = int.Parse(ips[0]);
            _ip2 = int.Parse(ips[1]);
            _ip3 = int.Parse(ips[2]);
            _ip4 = int.Parse(ips[3]);
            return true;
        }
    }
}
