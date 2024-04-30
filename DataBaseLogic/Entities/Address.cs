using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Entities
{
    public class Address : BaseEntity
    {
        public int Id { get; set; }

        public int StreetId { get; set; }

        public string HouseNumber { get; set; }
    }
}
