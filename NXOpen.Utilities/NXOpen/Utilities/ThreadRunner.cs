namespace NXOpen.Utilities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal class ThreadRunner
    {
        private static object externalLock = new object();
        private static bool finished = false;
        private static object finishedLock = new object();
        private static bool initialized = false;
        private static EventLoopHandler myLoopHandler;
        private static Thread nxThread;
        private static Runnable objectToRun;
        private static object runnableLock = new object();

        private static void initialize()
        {
            if (!initialized)
            {
                initialized = true;
            }
            if ((JAM_is_ui_present() == 1) && JAM_is_register_managed_event_loop_empty())
            {
                myLoopHandler = new EventLoopHandler(ThreadRunner.LoopHandler);
                JAM_register_managed_event_loop_fn(myLoopHandler);
            }
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern bool JAM_is_register_managed_event_loop_empty();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_is_ui_present();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_lprintf(string s);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_register_managed_event_loop_fn(EventLoopHandler handler);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_signal_event_loop();
        private static void log(string s)
        {
            Console.WriteLine(s);
            JAM_lprintf(s);
        }

        private static void LoopHandler()
        {
            bool flag = false;
            if (nxThread == null)
            {
                nxThread = Thread.CurrentThread;
            }
            try
            {
                Monitor.Enter(runnableLock);
                if (ThreadRunner.objectToRun != null)
                {
                    Runnable objectToRun = ThreadRunner.objectToRun;
                    ThreadRunner.objectToRun = null;
                    objectToRun.Run();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                ThreadRunner.objectToRun = null;
                flag = true;
                log("Caught exception " + exception.GetType().ToString());
                log(exception.StackTrace);
            }
            finally
            {
                Monitor.Exit(runnableLock);
            }
            if (flag)
            {
                Monitor.Enter(finishedLock);
                finished = true;
                Monitor.Pulse(finishedLock);
                Monitor.Exit(finishedLock);
            }
        }

        public static void RunSafely(Runnable obj)
        {
            initialize();
            if (JAM_is_ui_present() == 1)
            {
                if (Thread.CurrentThread == nxThread)
                {
                    log("  In main nxThread - running directly\n");
                    obj.Run();
                }
                else
                {
                    Monitor.Enter(externalLock);
                    Monitor.Enter(runnableLock);
                    finished = false;
                    objectToRun = obj;
                    Monitor.Exit(runnableLock);
                    if (JAM_signal_event_loop() != 1)
                    {
                        for (bool flag = false; !flag; flag = JAM_signal_event_loop() == 1)
                        {
                            log("Event signaller not ready - sleeping\n");
                            Thread.Sleep(0x7d0);
                        }
                    }
                    Monitor.Enter(finishedLock);
                    if (!finished)
                    {
                        Monitor.Wait(finishedLock);
                    }
                    Monitor.Exit(finishedLock);
                    Monitor.Exit(externalLock);
                }
            }
            else
            {
                obj.Run();
            }
        }

        private delegate void EventLoopHandler();
    }
}

