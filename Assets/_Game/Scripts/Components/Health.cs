using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Item
{
    [SerializeField] private int _health;
    private ParticleSystem _fx;
    private Light _light;
    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _light = GetComponentInChildren<Light>();
        _fx = GetComponentInChildren<ParticleSystem>(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        IHealth health = other.GetComponent<IHealth>();

        if (health != null)
        {
            health.AddHealth(_health);

            EventHolder.OnPlayerTakeHealth(_health);
            _meshRenderer.enabled = false;

            _light.gameObject.SetActive(false);

            _fx.gameObject.SetActive(true);

            Destroy(this.gameObject, 1);
        }
    }
}
