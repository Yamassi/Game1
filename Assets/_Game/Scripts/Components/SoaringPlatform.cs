using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SoaringPlatform : MonoBehaviour
{
    private static Vector3 offset = new Vector3(0f, -0.3f, 0f);
    private Tween _tween;
    private void Awake()
    {
        _tween = transform.DOPunchPosition(offset, 5, 0, 5, false).SetLoops(-1);
    }
    private void OnDisable()
    {
        _tween.Kill();
    }
}
