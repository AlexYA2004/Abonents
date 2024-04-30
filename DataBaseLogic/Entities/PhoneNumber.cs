using DataBaseLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public int Id { get; set; }

        public int AbonentId { get; set; }

        public string Number { get; set; }

        public PhoneType PhoneType { get; set; }
    }
}
