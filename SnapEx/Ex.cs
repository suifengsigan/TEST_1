using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapEx
{
    public static class Ex
    {
        public static Snap.Geom.Box3d AcsToWcsBox3d(this Snap.NX.NXObject obj)
        {
            var corners = new List<Snap.Position>();
            obj.Box.Corners.ToList().ForEach(u => {
                var acsOrientation = Snap.Orientation.Identity;
                var wcsOrientation = Snap.Globals.WcsOrientation;
                var transR = Snap.Geom.Transform.CreateRotation(acsOrientation, wcsOrientation);
                corners.Add(u.Copy(transR));
            });
            var xList = Enumerable.Select(corners, u => u.X).ToList();
            var yList = Enumerable.Select(corners, u => u.Y).ToList();
            var zList = Enumerable.Select(corners, u => u.Z).ToList();

            return new Snap.Geom.Box3d(xList.Min(), yList.Min(), zList.Min(), xList.Max(), yList.Max(), zList.Max());
        }

        /// <summary>
        /// 是否有该属性
        /// </summary>
        public static bool IsHasAttr(this Snap.NX.NXObject obj, string title)
        {
            return obj.GetAttributeInfo().Where(m => m.Title.ToUpper() == title.ToUpper()).Count() > 0;
        }

        /// <summary>
        /// 匹配属性值
        /// </summary>
        public static bool MatchAttrValue(this Snap.NX.NXObject obj, string title, object value)
        {
            return value.ToString() == obj.GetAttrValue(title);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        public static int GetAttrIntegerValue(this Snap.NX.NXObject nxObject, string title)
        {
            int result = 0;
            int.TryParse(nxObject.GetAttrValue(title), out result);
            return result;
        }

        public static double GetAttrRealValue(this Snap.NX.NXObject nxObject, string title)
        {
            double result = 0.00;
            double.TryParse(nxObject.GetAttrValue(title), out result);
            return result;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        public static string GetAttrValue(this Snap.NX.NXObject nxObject, string title)
        {
            string result = string.Empty;
            if (nxObject.IsHasAttr(title))
            {
                var attr = nxObject.GetAttributeInfo().FirstOrDefault(u => u.Title.ToUpper() == title.ToUpper());
                title = attr.Title;
                switch (attr.Type)
                {
                    case Snap.NX.NXObject.AttributeType.Integer:
                        {
                            result = nxObject.GetIntegerAttribute(title).ToString();
                            break;
                        }
                    case Snap.NX.NXObject.AttributeType.Real:
                        {
                            result = nxObject.GetRealAttribute(title).ToString();
                            break;
                        }
                    case Snap.NX.NXObject.AttributeType.String:
                        {
                            result = nxObject.GetStringAttribute(title);
                            break;
                        }
                }
            }
            return result;
        }

        /// <summary>
        /// 比例
        /// </summary>
        public static void SetScale(this NXOpen.Drawings.BaseView view, double numerator, int denominator)
        {
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            var baseViewBuilder1 = workPart.DraftingViews.CreateBaseViewBuilder(view);
            baseViewBuilder1.Scale.Numerator = numerator;
            baseViewBuilder1.Scale.Denominator = denominator;
            baseViewBuilder1.Commit();
            baseViewBuilder1.Destroy();
        }

        public static int UC6400(string viewName, NXOpen.Tag objectTag)
        {
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode = _uc6400(viewName, objectTag);
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }

        public static int UC6450(string cp1, string cp2, int ip3, int ip4)
        {
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode = _uc6450(cp1,cp2,ip3,ip4);
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }

        public static int UC6434(string cp1, int ip2, NXOpen.Tag np3, double[] rp4) 
        {
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode = _uc6434(cp1, ip2, np3, rp4);
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }

        public static int UC6449(string viewName)
        {
            NXOpen.Utilities.JAM.StartUFCall();
            int errorCode = _uc6449(viewName);
            NXOpen.Utilities.JAM.EndUFCall();
            return errorCode;
        }

        [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "uc6434", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _uc6434(string cp1, int ip2, NXOpen.Tag np3, double[] rp4);

         [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "uc6400", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        internal static extern int _uc6400(string viewName, NXOpen.Tag objectTag);


         [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "uc6450", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
         internal static extern int _uc6450(string cp1, string cp2, int ip3, int ip4);

         [System.Runtime.InteropServices.DllImport("libufun.dll", EntryPoint = "uc6449", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
         internal static extern int _uc6449(string cp1);
       

        /// <summary>
        /// 比例
        /// </summary>
        public static void SetScaleEx(this NXOpen.Drawings.BaseView view, double scale)
        {
            SetScale(view, scale, 1);
        }


        /// <summary>
        ///  获取边界尺寸
        /// </summary>
        public static NXOpen.Point2d GetBorderSize(this NXOpen.Drawings.DraftingView view)
        {
            var view_borders = new double[4];
            NXOpen.UF.UFSession.GetUFSession().Draw.AskViewBorders(view.Tag, view_borders);
            var size = new NXOpen.Point2d();
            size.Y = Math.Abs(view_borders[3] - view_borders[1]);
            size.X = Math.Abs(view_borders[2] - view_borders[0]);
            return size;
        }

    }
}
