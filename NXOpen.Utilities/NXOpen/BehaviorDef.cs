namespace NXOpen
{
    using System;

    public abstract class BehaviorDef
    {
        protected BehaviorDef()
        {
        }

        public abstract void Define(IDefinitionContext access);
        public abstract void Refresh(IRuntimeContext context);
        public abstract void Repaint();
        public abstract void Start(IRuntimeContext context);
        public abstract void Step(IRuntimeContext context, double dt);
        public abstract void Stop(IRuntimeContext context);
    }
}

