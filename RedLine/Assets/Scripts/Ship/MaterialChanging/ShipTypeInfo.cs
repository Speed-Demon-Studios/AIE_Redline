using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTypeInfo : MonoBehaviour
{
    public List<SkinnedMeshRenderer> partsOfTheShipDouble = new();
    public List<SkinnedMeshRenderer> partsOfTheShipSingle = new();
    public Material[] citadelMatDouble;
    public Material[] citadelMatSingle;
    public Material[] falconMatDouble;
    public Material[] falconMatSingle;
    public Material[] monarcMatDouble;
    public Material[] monarcMatSingle;

    public Animator animator;

    public void SwitchMaterials(int index)
    {
        foreach(SkinnedMeshRenderer renderer in partsOfTheShipDouble)
        {
            switch (index)
            {
                case 0:
                    renderer.materials = citadelMatDouble;
                    break;
                case 1:
                    renderer.materials = falconMatDouble;
                    break;
                case 2:
                    renderer.materials = monarcMatDouble;
                    break;
            }
        }
        
        foreach(SkinnedMeshRenderer renderer in partsOfTheShipSingle)
        {
            switch (index)
            {
                case 0:
                    renderer.materials = citadelMatSingle;
                    break;
                case 1:
                    renderer.materials = falconMatSingle;
                    break;
                case 2:
                    renderer.materials = monarcMatSingle;
                    break;
            }
        }
    }
}
