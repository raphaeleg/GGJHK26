using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // STATE MACHINE
    [SerializeField] private GameObject crowdRef;
    [SerializeField] private float npcSpeed = 1f;
    [SerializeField] private float npcRepelForce = 1f;
    [SerializeField] private float npcAttractForce = 1f;
    [SerializeField] private float npcFreezeTime = 1f;
    // Default behaviour: stick to black of CROWDREF, move slightly ish
    // Attract behaviour
    // Repel behaviour
    // Freeze Behaviour, ignore black and stay still for x seconds (use GAMEMANAGER)
}
