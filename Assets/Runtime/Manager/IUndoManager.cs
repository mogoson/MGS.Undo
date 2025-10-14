/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IUndoManager.cs
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
    public interface IUndoManager
    {
        event Action<int> OnUndosChanged;
        event Action<int> OnRedosChanged;

        int UndosCount { get; }

        int RedosCount { get; }

        void Todo(IDoHandler doHandler);

        bool Undo();

        bool Redo();

        void Clear();
    }
}