/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestCube.cs
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
    public class TestCube : MonoBehaviour
    {
        private new Renderer renderer;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void OnMouseDown()
        {
            var newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            var originColor = renderer.material.color;
            renderer.material.color = newColor;

            var handler = new DoHandler(
                () => renderer.material.color = originColor,
                () => renderer.material.color = newColor);
            Global.UndoManager.Register(handler);
        }
    }
}