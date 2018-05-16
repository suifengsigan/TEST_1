namespace NXOpen.Utilities
{
    using System;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;

    public class NXRemotingHelper
    {
        public static object GetServerObject(Type objectType, object ownerSession)
        {
            object obj2 = null;
            if (!(ownerSession is MarshalByRefObject))
            {
                return obj2;
            }
            ObjRef ref2 = (ownerSession as MarshalByRefObject).CreateObjRef(ownerSession.GetType());
            string str = null;
            foreach (object obj4 in ref2.ChannelInfo.ChannelData)
            {
                if (obj4 is IChannelDataStore)
                {
                    IChannelDataStore store = (IChannelDataStore) obj4;
                    str = store.ChannelUris[0];
                    break;
                }
            }
            string url = str + "/" + objectType.Name + "_ServerObject";
            return Activator.GetObject(objectType, url);
        }

        public static bool IsSessionRunningRemotely(object ownerSession)
        {
            return RemotingServices.IsObjectOutOfAppDomain(ownerSession);
        }
    }
}

