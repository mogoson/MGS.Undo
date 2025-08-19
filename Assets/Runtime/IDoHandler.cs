/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IDoHandler.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/18
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Undo
{
    public interface IDoHandler
    {
        void Undo();

        void Redo();
    }
}