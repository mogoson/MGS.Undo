/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CubeUndoSample.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/19
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Undo.Sample
{
    [RequireComponent(typeof(Renderer))]
    public class CubeUndoSample : MonoBehaviour
    {
        private new Renderer renderer;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void OnMouseDown()
        {
            var oldColor = renderer.material.color;
            var newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            var handler = new DoHandler<Color>(oldColor, newColor, color => renderer.material.color = color);
            Global.UndoManager.Todo(handler);
        }
    }
}