using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgStationData
{
public class JRLine
{
    public int line_cd { get; set; }
    public string line_name { get; set; }
    public double line_lon { get; set; }
    public double line_lat { get; set; }
    public int line_zoom { get; set; }
    public List<JRStation> station_l { get; set; }
    public override string ToString()
    {
        return line_name;
    }
}
public class JRStation
{
    public int station_cd { get; set; }
    public int station_g_cd { get; set; }
    public string station_name { get; set; }
    public double lon { get; set; }
    public double lat { get; set; }
    public override string ToString()
    {
        return station_name;
    }
}
}
