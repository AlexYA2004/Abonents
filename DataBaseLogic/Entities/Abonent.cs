using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Entities
{
    public class Abonent : BaseEntity
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int AddressId { get; set; }
    }
}
