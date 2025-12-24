/*************************************************************************
 *  Copyright © 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UndoHandler.cs
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
    /// <summary>
    /// UndoHandler with different action.
    /// </summary>
    public class UndoHandler : IUndoHandler
    {
        protected Action undoAction;
        protected Action redoAction;

        public UndoHandler(Action undoAction, Action redoAction)
        {
            this.undoAction = undoAction;
            this.redoAction = redoAction;
        }

        public virtual void Undo()
        {
            undoAction?.Invoke();
        }

        public virtual void Redo()
        {
            redoAction?.Invoke();
        }
    }

    /// <summary>
    /// UndoHandler with same action to change value.
    /// </summary>
    /// <typeparam name="T">Type of value.</typeparam>
    public class UndoHandler<T> : IUndoHandler
    {
        protected T oldValue;
        protected T newValue;
        protected Action<T> doAction;

        public UndoHandler(T oldValue, T newValue, Action<T> doAction)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.doAction = doAction;
        }

        public virtual void Undo()
        {
            doAction?.Invoke(oldValue);
        }

        public virtual void Redo()
        {
            doAction?.Invoke(newValue);
        }
    }
}