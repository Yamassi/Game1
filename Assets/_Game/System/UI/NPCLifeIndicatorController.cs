using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLifeIndicatorController : MonoBehaviour
{
    [SerializeField] private NPCLifeIndicator _nPCLifePrefab;
    private Dictionary<NPCController, NPCLifeIndicator> _lifeIndicators = new Dictionary<NPCController, NPCLifeIndicator>();
    private void Awake()
    {
        EventHolder.OnNPCTakeDamage += LifeRemove;
        EventHolder.OnNPCDie += LifeIndicatorRemove;
    }
    private void OnDisable()
    {
        EventHolder.OnNPCTakeDamage -= LifeRemove;
        EventHolder.OnNPCDie -= LifeIndicatorRemove;
    }
    private void LifeRemove(NPCController npc)
    {
        IHealth health = npc.GetComponent<IHealth>();
        if (!_lifeIndicators.ContainsKey(npc))
        {
            NPCLifeIndicator lifeIndicator = NewMethod(npc, health);
            _lifeIndicators.Add(npc, lifeIndicator);
        }
        else
        {
            if (_lifeIndicators[npc] != null)
                _lifeIndicators[npc].SetCurrentLife(health.GetCurrentHealth());
            else
            {
                NPCLifeIndicator lifeIndicator = NewMethod(npc, health);
                _lifeIndicators[npc] = lifeIndicator;
            }
        }
    }

    private NPCLifeIndicator NewMethod(NPCController npc, IHealth health)
    {
        NPCLifeIndicator lifeIndicator = Instantiate(_nPCLifePrefab, this.transform);
        lifeIndicator.SetMaxLifes(health.GetMaxHealth());
        lifeIndicator.SetName(npc.name);
        lifeIndicator.SetCurrentLife(health.GetCurrentHealth());
        return lifeIndicator;
    }

    private void LifeIndicatorRemove(NPCController npc)
    {
        Destroy(_lifeIndicators[npc].gameObject, 0.5f);
        _lifeIndicators.Remove(npc);
    }
}
