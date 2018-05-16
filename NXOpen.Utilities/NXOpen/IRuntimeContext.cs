namespace NXOpen
{
    using System;
    using System.Runtime.InteropServices;

    public interface IRuntimeContext
    {
        ComponentPart AskRoot();
        void Error(bool severity, string strMessage);
        void ForcePause();
        RuntimeObject GetRuntimeObject(Tag physTag);
        void GetRuntimeObjects(Tag physTag, out int numOfObjects, out RuntimeObject[] runtimeObjects);
        void SetPerformSimulation(SimulationAction action);
    }
}

