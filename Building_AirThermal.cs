﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace CentralizedClimateControl
{
    public class Building_AirThermal : Building_AirFlowControl
    {
        public CompTempControl CompTempControl;
        public CompAirFlowTempControl CompAirFlowTempControl;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            CompTempControl = base.GetComp<CompTempControl>();
            CompAirFlowTempControl = base.GetComp<CompAirFlowTempControl>();
            base.SpawnSetup(map, respawningAfterLoad);
        }

        public override void TickRare()
        {
            if (!CompPowerTrader.PowerOn)
            {
                return;
            }

            if (!CompAirFlowTempControl.IsOperating())
            {
                return;
            }

            CompAirFlowTempControl.TickRare(CompTempControl);
        }

    }
}