/*************************************************************************
 *  Copyright © 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UndoManager.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/18
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.Undo
{
    public class UndoManager : IUndoManager
    {
        public event Action<int> OnUndosChanged;
        public event Action<int> OnRedosChanged;

        public int UndosCount { get { return undos.Count; } }

        public int RedosCount { get { return redos.Count; } }

        protected uint capacity;
        protected List<IUndoHandler> undos = new();
        protected Stack<IUndoHandler> redos = new();

        public UndoManager(uint capacity = 10)
        {
            this.capacity = capacity;
        }

        public virtual void Todo(IUndoHandler handler)
        {
            handler.Redo();
            Register(handler);
        }

        public virtual void Register(IUndoHandler handler)
        {
            PushToUndos(handler);
            ClearRedos();
        }

        public virtual bool Undo()
        {
            var handler = PopFromUndos();
            if (handler == null)
            {
                return false;
            }

            handler.Undo();
            PushToRedos(handler);
            return true;
        }

        public virtual bool Redo()
        {
            var handler = PopFromRedos();
            if (handler == null)
            {
                return false;
            }

            handler.Redo();
            PushToUndos(handler);
            return true;
        }

        public virtual void Clear()
        {
            ClearUndos();
            ClearRedos();
        }

        protected void ClearUndos()
        {
            foreach (var handler in undos)
            {
                if (handler is IUndoDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            undos.Clear();
            OnUndosChanged?.Invoke(undos.Count);
        }

        protected void ClearRedos()
        {
            foreach (var handler in redos)
            {
                if (handler is IRedoDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            redos.Clear();
            OnRedosChanged?.Invoke(redos.Count);
        }

        protected void PushToUndos(IUndoHandler handler)
        {
            undos.Add(handler);
            RequireUndosCapacity();
            OnUndosChanged?.Invoke(undos.Count);
        }

        protected IUndoHandler PopFromUndos()
        {
            if (undos.Count == 0)
            {
                return null;
            }

            var popIndex = undos.Count - 1;
            var handler = undos[popIndex];

            undos.RemoveAt(popIndex);
            OnUndosChanged?.Invoke(undos.Count);
            return handler;
        }

        protected void RequireUndosCapacity()
        {
            while (undos.Count > capacity)
            {
                var handler = undos[0];
                if (handler is IUndoDisposable disposable)
                {
                    disposable.Dispose();
                }
                undos.Remove(handler);
            }
        }

        protected void PushToRedos(IUndoHandler handler)
        {
            redos.Push(handler);
            OnRedosChanged?.Invoke(redos.Count);
        }

        protected IUndoHandler PopFromRedos()
        {
            if (redos.Count == 0)
            {
                return null;
            }

            var handler = redos.Pop();
            OnRedosChanged?.Invoke(redos.Count);
            return handler;
        }
    }
}