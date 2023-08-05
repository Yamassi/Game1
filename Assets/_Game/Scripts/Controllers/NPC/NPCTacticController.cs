using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTacticController : MonoBehaviour
{
    [SerializeField] private List<NPCController> _nPCs = new List<NPCController>();
    private int _npcsAttackAtOnce = 1;
    public void Init()
    {
        SendNPCToAttack();
    }
    public void AddNPC(NPCController nPC)
    {
        _nPCs.Add(nPC);
    }
    public void RemoveNPC(NPCController nPC)
    {
        _nPCs.Remove(nPC);
    }
    public void SetCountNPCsAttackAtOnce(int npc)
    {
        _npcsAttackAtOnce = npc;
    }
    public void SendNPCToAttack()
    {
        switch (_npcsAttackAtOnce)
        {
            case 1:
                NPCToAttack(1);
                break;
            case 2:
                NPCToAttack(2);
                break;
            case 3:
                NPCToAttack(3);
                break;
        }

    }

    private void NPCToAttack(int count)
    {
        if (_nPCs.Count > 0)
        {
            if (count > 1 && _nPCs.Count >= count)
            {
                List<int> randomNumbers = new List<int>();

                while (randomNumbers.Count < count)
                {
                    int randomNumber = Random.Range(0, _nPCs.Count);
                    Debug.Log("Random Number" + randomNumber);
                    if (!randomNumbers.Contains(randomNumber))
                    {
                        randomNumbers.Add(randomNumber);
                    }
                }

                for (int i = 0; i < randomNumbers.Count; i++)
                {
                    _nPCs[randomNumbers[i]].ChangeBehaviour(NPCBehaviour.Attack);
                }
            }
            else if (count == 1)
            {
                _nPCs[0].ChangeBehaviour(NPCBehaviour.Attack);
            }

        }

        Debug.Log("Count - " + count);
    }
    // private void NewRandomNPCToAttack()
    // {


    //     _nPCs[randomNPC].ChangeBehaviour(NPCBehaviour.Attack);
    // }
}
