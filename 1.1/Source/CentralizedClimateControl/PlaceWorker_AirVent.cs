﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace CentralizedClimateControl
{
    public class PlaceWorker_AirVent : PlaceWorker
    {
        /// <summary>
        /// Draw Overlay when Selected or Placing.
        /// 
        /// Here we just draw a red/blue/cyan cell (based on Network flow type) towards the North. To indicate Exhaust.
        /// </summary>
        /// <param name="def">The Thing's Def</param>
        /// <param name="center">Location</param>
        /// <param name="rot">Rotation</param>
        /// <param name="ghostCol">Ghost Color</param>
        /// 
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            var type = AirFlowType.Hot;

            Map map = Find.CurrentMap;

            var list = center.GetThingList(map);
            foreach (var thing2 in list)
            {
                if (!(thing2 is Building_AirVent))
                {
                    continue;
                }

                var airVent = thing2 as Building_AirVent;

                if (airVent.CompAirFlowConsumer.AirFlowNet != null)
                {
                    type = airVent.CompAirFlowConsumer.AirFlowNet.FlowType;
                }

                break;
            }

            var intVec = center + IntVec3.North.RotatedBy(rot);

            var typeColor = type == AirFlowType.Hot ? Color.red : type == AirFlowType.Cold ? Color.blue : Color.cyan;

            GenDraw.DrawFieldEdges(new List<IntVec3>
            {
                intVec
            }, typeColor);

            var roomGroup = intVec.GetRoomGroup(map);
            if (roomGroup == null)
            {
                return;
            }

            if (!roomGroup.UsesOutdoorTemperature)
            {
                GenDraw.DrawFieldEdges(roomGroup.Cells.ToList(), typeColor);
            }
        }
        /// <summary>
        /// Place Worker for Air Vents.
        /// 
        /// Checks:
        /// - North Cell from Center musn't be Impassable
        /// </summary>
        /// <param name="def">The Def Being Built</param>
        /// <param name="center">Target Location</param>
        /// <param name="rot">Rotation of the Object to be Placed</param>
        /// <param name="thingToIgnore">Unused field</param>
        /// <returns>Boolean/Acceptance Report if we can place the object of not.</returns>
        /// 
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            var vec = loc + IntVec3.North.RotatedBy(rot);

            if (vec.Impassable(map))
            {
                return "CentralizedClimateControl.Consumer.AirVentPlaceError".Translate();
            }

            return true;
        }
    }
}
