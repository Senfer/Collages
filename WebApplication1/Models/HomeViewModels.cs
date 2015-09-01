using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class HomeViewModel 
    {
        public System.Collections.Generic.IEnumerable<Collages> CollageData;
        public Collages CurrentCollage;
        public int CollageID;
    }
}