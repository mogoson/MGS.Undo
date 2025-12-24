/*************************************************************************
 *  Copyright © 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IUndoHandler.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/18
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Undo
{
    public interface IUndoHandler
    {
        void Undo();

        void Redo();
    }
}