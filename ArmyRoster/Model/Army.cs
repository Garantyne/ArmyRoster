using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Model
{
    public class Army
    {
        public string NameArmy {  get; set; }
        public string Path {  get; set; }

        public List<UnitsW40K> Units { get; set; }
        public Army() { }

        public override string ToString()
        {
            return NameArmy;
        }
    }
}
