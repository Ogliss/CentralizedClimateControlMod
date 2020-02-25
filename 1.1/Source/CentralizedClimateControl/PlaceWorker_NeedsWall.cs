﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace CentralizedClimateControl
{
    public class PlaceWorker_NeedsWall : PlaceWorker
    {
        /// <summary>
        /// Place Worker for Wall Mounted Air Vents. We check if a Wall must be present on the Target Cell.
        /// </summary>
        /// <param name="def">The Def Being Built</param>
        /// <param name="center">Target Location</param>
        /// <param name="rot">Rotation of the Object to be Placed</param>
        /// <param name="thingToIgnore">Unused field</param>
        /// <returns>Boolean/Acceptance Report if we can place the object of not.</returns>
        /// 
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            var c = loc;
            var wall = c.GetEdifice(map);

            return wall != null;
        }
    }
}
