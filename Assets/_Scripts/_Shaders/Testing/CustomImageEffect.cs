﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    void OnRenderImage (RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, EffectMaterial);
    }
}
