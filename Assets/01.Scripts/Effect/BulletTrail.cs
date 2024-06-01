using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : PoolableMono
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void DrawTrail(Vector3 startPos, Vector3 endPos, float lifeTime)
    {
        _trailRenderer.AddPosition(startPos);
        transform.position = endPos;

        _trailRenderer.time = lifeTime;

        StartCoroutine(LifeTimeCoroutine(lifeTime));
    }

    private IEnumerator LifeTimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        _trailRenderer.Clear();
    }
}
