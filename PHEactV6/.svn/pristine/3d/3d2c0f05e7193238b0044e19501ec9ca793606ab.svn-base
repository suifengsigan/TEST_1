using NXOpen;
using NXOpen.Features;
using Snap.NX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapEx
{
    static partial  class ExMethod
    {
        public static List<Snap.NX.Body> BodiesEx(this Snap.NX.Part part)
        {
            var list = part.Bodies.ToList();
            if (part.RootComponent != null) 
            {
                part.RootComponent.Descendants.ToList().ForEach(m => {
                    var members=Members(m).Where(u => GetTypeFromTag(u.Tag) == Snap.NX.ObjectTypes.Type.Body);
                    list.AddRange(Enumerable.Select(members, u => { return Snap.NX.Body.Wrap(u.Tag); }));
                });
            }
            return list;
        }

        private static NXOpen.NXObject[] Members(Snap.NX.Component comp)
        {
            var uFSession = NXOpen.UF.UFSession.GetUFSession();
                Tag @null = Tag.Null;
                NXOpen.NXObject nxopenTaggedObject = null;
                var list = new List<NXOpen.NXObject>();
                do
                {
                    @null = uFSession.Assem.CycleEntsInPartOcc(comp.NXOpenTag, @null);
                    if (@null != Tag.Null)
                    {
                        try
                        {
                            nxopenTaggedObject = GetObjectFromTag(@null);
                            if (nxopenTaggedObject != null)
                            {
                                list.Add(nxopenTaggedObject);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                while (@null != Tag.Null);
                return list.ToArray();
        }


        private static NXOpen.NXObject GetObjectFromTag(Tag tag)
        {
            NXOpen.NXObject obj2 = null;
            try
            {
                obj2 = NXOpen.Utilities.NXObjectManager.Get(tag) as NXOpen.NXObject;
            }
            catch
            {
            }
            return obj2;
        }
        private static ObjectTypes.Type GetTypeFromTag(Tag nxopenTag)
        {
            int num;
            int num2;
            NXOpen.UF.UFSession.GetUFSession().Obj.AskTypeAndSubtype(nxopenTag, out num, out num2);
            ObjectTypes.Type body = (ObjectTypes.Type)num;
            if (num == 70)
            {
                switch (num2)
                {
                    case 0:
                        body = ObjectTypes.Type.Body;
                        break;

                    case 2:
                        body = ObjectTypes.Type.Face;
                        break;

                    case 3:
                        body = ObjectTypes.Type.Edge;
                        break;
                }
            }
            if (num == 0xcd)
            {
                NXOpen.Features.Feature objectFromTag = (NXOpen.Features.Feature)GetObjectFromTag(nxopenTag);
                if (objectFromTag is DatumAxisFeature)
                {
                    body = ObjectTypes.Type.DatumAxis;
                }
                if (objectFromTag is DatumPlaneFeature)
                {
                    body = ObjectTypes.Type.DatumPlane;
                }
            }
            return body;
        }
    }
}
