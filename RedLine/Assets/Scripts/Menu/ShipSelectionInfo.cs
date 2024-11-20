using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionInfo : MonoBehaviour
{
    public Animator shipDisplayAnim;
    public Animator manufacturerDisplayAnim;
    public List<Animator> shipAnimators;
    public List<Sprite> manufacturerSprites;
    public Image manufacturerImage;
    public Image manufacturerImageRed;
}
