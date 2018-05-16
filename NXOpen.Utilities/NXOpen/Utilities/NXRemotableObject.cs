namespace NXOpen.Utilities
{
    using System;
    using System.Runtime.Remoting.Messaging;

    public class NXRemotableObject : MarshalByRefObject, IMessageSink
    {
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            try
            {
                MessageRunner runner = new MessageRunner {
                    inputMessage = msg,
                    target = this
                };
                ThreadRunner.RunSafely(runner);
                IMessage outMessage = runner.outMessage;
                replySink.SyncProcessMessage(outMessage);
                return new MessageControl();
            }
            catch (Exception exception)
            {
                replySink.SyncProcessMessage(new ReturnMessage(exception, (IMethodCallMessage) msg));
                return new MessageControl();
            }
        }

        protected void initialize()
        {
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            try
            {
                MessageRunner runner = new MessageRunner {
                    inputMessage = msg,
                    target = this
                };
                ThreadRunner.RunSafely(runner);
                return runner.outMessage;
            }
            catch (Exception exception)
            {
                return new ReturnMessage(exception, (IMethodCallMessage) msg);
            }
        }

        public IMessageSink NextSink
        {
            get
            {
                return null;
            }
        }
    }
}

