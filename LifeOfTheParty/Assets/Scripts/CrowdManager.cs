using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdManager : MonoBehaviour
{
    [SerializeField] private Image crowdRef;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private List<GameObject> npcList = new List<GameObject>();
    [SerializeField] private int crowdSize = 200;
    [SerializeField] private float blackAreaRadius = 10f;
    [SerializeField] private float npcSpeed = 1f;
    [SerializeField] private float npcRepelForce = 1f;
    [SerializeField] private float npcAttractForce = 1f;
    [SerializeField] private float npcFreezeTime = 1f;
    // REF to CROWDREF, will MANIPULATE it
    // USE black as base natural
    // Object pool ALL NPCs (random) (200?) (place RANDOMLY, starting in black areas)
    // Each has AI behaviour

    private void Start() {
        for (int i = 0; i < crowdSize; i++) {
            GameObject npc = Instantiate(npcPrefab, crowdRef.transform);
            // position at a black area of the crowdref image

            npcList.Add(npc);
        }
    }
}
