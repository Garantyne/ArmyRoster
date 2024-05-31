using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Model
{
    internal interface IUnitInfo
    {
        void Load();
        void Save(bool flag, string path, string name);
    }
}
