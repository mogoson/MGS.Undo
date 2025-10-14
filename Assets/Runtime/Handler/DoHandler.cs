/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DoHandler.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/19
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Undo
{
    public class DoHandler<T> : IDoHandler
    {
        protected T oldValue;
        protected T newValue;
        protected Action<T> doAction;

        public DoHandler(T oldValue, T newValue, Action<T> doAction)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.doAction = doAction;
        }

        public virtual void Todo()
        {
            doAction?.Invoke(newValue);
        }

        public virtual void Undo()
        {
            doAction?.Invoke(oldValue);
        }
    }
}