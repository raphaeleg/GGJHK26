using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
public class InventoryManager: MonoBehaviour
{
    [Header("Mask Icon")]
    [SerializeField] private GameObject dashMaskIcon;
    [SerializeField] private GameObject barrierMaskIcon;
    [SerializeField] private GameObject repelMaskIcon;

    private void Start()
    {
        dashMaskIcon.SetActive(false);
        barrierMaskIcon.SetActive(false);
        repelMaskIcon.SetActive(false);
    }
    public void ShowDashMaskIcon()
    {
        dashMaskIcon.SetActive(true);
    }

    public void ShowBarrierMaskIcon()
    {
        barrierMaskIcon.SetActive(true);
    }

    public void ShowRepelMaskIcon()
    {
        repelMaskIcon.SetActive(true);
    }

    public void HideDashMaskIcon()
    {
        dashMaskIcon.SetActive(false);
    }

    public void HideBarrierMaskIcon()
    {
        barrierMaskIcon.SetActive(false);
    }

    public void HideRepelMaskIcon()
    {
        repelMaskIcon.SetActive(false);
    }
}
