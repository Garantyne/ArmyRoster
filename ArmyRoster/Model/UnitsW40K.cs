using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Model
{
    public class UnitsW40K :IUnits
    {
        public string Name {  get; set; }
        public UnitInfoW40K UnitInfo { get; set; }
        public UnitsW40K() { }

    }
}
