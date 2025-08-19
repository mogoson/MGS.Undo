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
    public class DoHandler : IDoHandler
    {
        protected Action undo;
        protected Action redo;

        public DoHandler(Action undo, Action redo)
        {
            this.undo = undo;
            this.redo = redo;
        }

        public virtual void Undo()
        {
            undo?.Invoke();
        }

        public virtual void Redo()
        {
            redo?.Invoke();
        }
    }
}