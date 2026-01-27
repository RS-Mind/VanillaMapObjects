using MapsExt.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VanillaMapObjects
{
    public static class RoundsExtensions
    {
        private class ExtraRopeData
        {
            public Action<SpringJoint2D> ropeListener = _ => { };
        }

        private static readonly ConditionalWeakTable<MapObjet_Rope, ExtraRopeData> s_ropeData = new ConditionalWeakTable<MapObjet_Rope, ExtraRopeData>();

        internal static void OnJointAdded(this MapObjet_Rope instance, Action<SpringJoint2D> cb)
        {
            var data = s_ropeData.GetOrCreateValue(instance);
            data.ropeListener += cb;
        }

        internal static void JointAdded(this MapObjet_Rope instance, SpringJoint2D joint)
        {
            s_ropeData.GetOrCreateValue(instance).ropeListener(joint);
        }
    }
}
