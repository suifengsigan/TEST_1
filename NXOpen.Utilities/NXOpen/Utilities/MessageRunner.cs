namespace NXOpen.Utilities
{
    using System;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Messaging;

    internal class MessageRunner : Runnable
    {
        public IMessage inputMessage;
        public IMessage outMessage;
        public MarshalByRefObject target;

        public void Run()
        {
            this.outMessage = RemotingServices.ExecuteMessage(this.target, (IMethodCallMessage) this.inputMessage);
        }
    }
}

