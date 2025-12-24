/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UndoHandlerForGo.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/22/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Undo
{
    /// <summary>
    /// UndoHandler for GameObject.
    /// </summary>
    public abstract class UndoHandlerForGo : UndoHandler
    {
        protected static Transform Ghost { get; }
        protected List<GameObject> gos = new();
        protected List<Transform> parents = new();

        static UndoHandlerForGo()
        {
            var go = new GameObject("UndoGhost")
            {
                hideFlags = HideFlags.HideInHierarchy
            };
            UnityEngine.Object.DontDestroyOnLoad(go);
            Ghost = go.transform;
        }

        public UndoHandlerForGo(Action undoAction, Action redoAction, params GameObject[] gos)
            : base(undoAction, redoAction)
        {
            this.gos.AddRange(gos);
            foreach (var go in gos)
            {
                parents.Add(go.transform.parent);
            }
        }

        protected void SimulateDestroy()
        {
            foreach (var go in gos)
            {
                go.SetActive(false);
                go.transform.SetParent(Ghost);
            }
        }

        protected void SimulateUndoDestroy()
        {
            var index = 0;
            foreach (var go in gos)
            {
                var parent = parents[index];
                go.transform.SetParent(parent);
                go.SetActive(true);
                index++;
            }
        }

        protected void ReallyDestroy()
        {
            foreach (var go in gos)
            {
                UnityEngine.Object.Destroy(go);
            }
            gos.Clear();
        }
    }

    /// <summary>
    /// UndoHandler for GameObject Instantiate.
    /// </summary>
    public class UndoHandlerForGoInstantiate : UndoHandlerForGo, IRedoDisposable
    {
        public UndoHandlerForGoInstantiate(Action undoAction, Action redoAction, params GameObject[] gos)
            : base(undoAction, redoAction, gos)
        { }

        public override void Undo()
        {
            base.Undo();
            SimulateDestroy();
        }

        public override void Redo()
        {
            base.Redo();
            SimulateUndoDestroy();
        }

        public virtual void Dispose()
        {
            ReallyDestroy();
        }
    }

    /// <summary>
    /// UndoHandler for GameObject Destroy.
    /// </summary>
    public class UndoHandlerForGoDestroy : UndoHandlerForGo, IUndoDisposable
    {
        public UndoHandlerForGoDestroy(Action undoAction, Action redoAction, params GameObject[] gos)
            : base(undoAction, redoAction, gos)
        { }

        public override void Undo()
        {
            base.Undo();
            SimulateUndoDestroy();
        }

        public override void Redo()
        {
            base.Redo();
            SimulateDestroy();
        }

        public virtual void Dispose()
        {
            ReallyDestroy();
        }
    }
}