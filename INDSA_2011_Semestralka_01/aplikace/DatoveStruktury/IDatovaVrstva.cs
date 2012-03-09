using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikace {
    interface IDatovaVrstva {
        void UlozHrany(List<Hrana> cesty);
        void UlozVrcholy(List<Vrchol> vrcholy);
        List<Hrana> NactiHrany(ref Vrcholy vrcholy);
        List<Vrchol> NactiVrcholy();
    }
}
