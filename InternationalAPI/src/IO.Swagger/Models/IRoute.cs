using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using System;

namespace IO.Swagger.Models
{
    public interface IRoute
    {
        Guid? Id { get; set; }
        decimal? PriceTotal { get; set; }
        List<Segment> Segments { get; set; }

        string ToString();

        string ToJson();

        bool Equals(object obj);

        bool Equals(Route other);

        int GetHashCode();
       
    }
}
