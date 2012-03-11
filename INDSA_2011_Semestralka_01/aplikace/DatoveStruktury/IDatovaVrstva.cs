using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aplikace.DatoveStruktury;

namespace aplikace {
    interface IDatovaVrstva {
        void UlozHrany(List<CestyGraf.Hrana> cesty);
        void UlozVrcholy(List<CestyGraf.Vrchol> vrcholy);
        List<CestyGraf.Hrana> NactiHrany(ref CestyGraf graf);
        List<CestyGraf.Vrchol> NactiVrcholy();
    }
}
