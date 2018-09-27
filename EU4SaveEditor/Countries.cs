using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4SaveEditor
{
    class Country
    {
        public Country(string Vname = "Useless", int Vid = 0)
        {
            Name = Vname;
            Id = Vid;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }

    class Province : Country
    {
        public Province(string Vname = "Useless", int Vid = 0, string owner = "Useless", int OwnId = 0)
        {
            Name = Vname;
            Id = Vid;
            OwnerName = owner;
            OwnerId = OwnId;
        }

        public string OwnerName { get; set; }
        public int OwnerId { get; set; }

        public string Tax { get; set; }
        public int TaxId { get; set; }

        public string Prod { get; set; }
        public int ProdId { get; set; }

        public string ManPow { get; set; }
        public int ManPowId { get; set; }
    }
}
