﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace CentralizedClimateControl
{
    public class CompAirFlowPipe : CompAirFlow
    {
        public override string CompInspectStringExtra()
        {
            return GetAirTypeString(this.Props.flowType);
        }
    }
}