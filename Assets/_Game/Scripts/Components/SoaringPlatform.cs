using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SoaringPlatform : MonoBehaviour
{
    private static Vector3 offset = new Vector3(0f, -1f, 0f);
    private void Awake()
    {
        transform.DOPunchPosition(offset, 5, 0, 5, false).SetLoops(-1);
    }
}
