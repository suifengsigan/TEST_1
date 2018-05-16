namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Runtime.InteropServices;

    public class RuntimeImpl : IRuntimeContext
    {
        private IntPtr m_pSelf;

        public ComponentPart AskRoot()
        {
            return (RuntimeObject.FromPtr(ExAskRoot(this.m_pSelf)) as ComponentPart);
        }

        public void Error(bool severity, string strMessage)
        {
            ExError(this.m_pSelf, severity ? -1 : 0, strMessage);
        }

        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_context_exec_ask_root", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExAskRoot(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_define_results_get_runtime_object", CallingConvention=CallingConvention.Cdecl)]
        public static extern IntPtr ExAskRuntimeObject(IntPtr pItem, Tag physTag);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_define_results_get_runtime_objects", CallingConvention=CallingConvention.Cdecl)]
        public static extern unsafe void ExAskRuntimeObjects(IntPtr pItem, Tag physTag, out int numOfObjects, out void*** runtimeObjects);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_context_exec_error", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        public static extern void ExError(IntPtr pItem, int severe, string strMessage);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_context_exec_force_pause", CallingConvention=CallingConvention.Cdecl)]
        public static extern void ExForcePause(IntPtr pItem);
        [DllImport("libmdphysint.dll", EntryPoint="MDPHYS_set_perform_simulation", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern void ExSetPerformSimulation(SimulationAction action);
        public void ForcePause()
        {
            ExForcePause(this.m_pSelf);
        }

        public RuntimeObject GetRuntimeObject(Tag physTag)
        {
            return RuntimeObject.FromPtr(ExAskRuntimeObject(this.m_pSelf, physTag));
        }

        public unsafe void GetRuntimeObjects(Tag physTag, out int numOfObjects, out RuntimeObject[] runtimeObjects)
        {
            runtimeObjects = null;
            void*** voidPtr = null;
            ExAskRuntimeObjects(this.m_pSelf, physTag, out numOfObjects, out voidPtr);
            if (numOfObjects > 0)
            {
                runtimeObjects = new RuntimeObject[numOfObjects];
                for (int i = 0; i < numOfObjects; i++)
                {
                    IntPtr pItem = *((IntPtr*) (voidPtr + i));
                    runtimeObjects[i] = RuntimeObject.FromPtr(pItem);
                }
                SM_free_area((void*) voidPtr);
            }
        }

        public void Init(long pSelf)
        {
            this.m_pSelf = new IntPtr(pSelf);
        }

        public void SetPerformSimulation(SimulationAction action)
        {
            ExSetPerformSimulation(action);
        }

        [DllImport("libmdphysint.dll", EntryPoint="MDPHYSINT_free_area", CallingConvention=CallingConvention.Cdecl)]
        internal static extern unsafe void SM_free_area(void* ptr);
    }
}

