/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
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
        protected List<IDoHandler> undos = new();
        protected Stack<IDoHandler> redos = new();

        public UndoManager(uint capacity = 10)
        {
            this.capacity = capacity;
        }

        public virtual void Register(IDoHandler doHandler)
        {
            PushToUndos(doHandler);
            ClearRedos();
        }

        public virtual bool Undo()
        {
            var undo = PopFromUndos();
            if (undo == null)
            {
                return false;
            }

            undo.Undo();
            PushToRedos(undo);
            return true;
        }

        public virtual bool Redo()
        {
            var redo = PopFromRedos();
            if (redo == null)
            {
                return false;
            }

            redo.Redo();
            PushToUndos(redo);
            return true;
        }

        public virtual void Clear()
        {
            ClearUndos();
            ClearRedos();
        }

        protected void ClearUndos()
        {
            undos.Clear();
            OnUndosChanged?.Invoke(undos.Count);
        }

        protected void ClearRedos()
        {
            redos.Clear();
            OnRedosChanged?.Invoke(redos.Count);
        }

        protected void PushToUndos(IDoHandler doHandler)
        {
            undos.Add(doHandler);
            RequireUndosCapacity();
            OnUndosChanged?.Invoke(undos.Count);
        }

        protected IDoHandler PopFromUndos()
        {
            if (undos.Count == 0)
            {
                return null;
            }

            var popIndex = undos.Count - 1;
            var undo = undos[popIndex];

            undos.RemoveAt(popIndex);
            OnUndosChanged?.Invoke(undos.Count);
            return undo;
        }

        protected void RequireUndosCapacity()
        {
            while (undos.Count > capacity)
            {
                undos.RemoveAt(0);
            }
        }

        protected void PushToRedos(IDoHandler doHandler)
        {
            redos.Push(doHandler);
            OnRedosChanged?.Invoke(redos.Count);
        }

        protected IDoHandler PopFromRedos()
        {
            var redo = redos.Pop();
            OnRedosChanged?.Invoke(redos.Count);
            return redo;
        }
    }
}