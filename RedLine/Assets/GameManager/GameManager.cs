using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CheckpointHandler checkpointParent;
    public GameObject[] StartingPoints;
    public PositionHandler pHandler;
    public IList<GameObject> playerObjects = new List<GameObject>();
    public IList<GameObject> racerObjects = new List<GameObject>();
    public bool raceStarted = false;

    public static GameManager gManager { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        if (gManager != null && gManager != this)
        {
            Destroy(this);
        }
        else
        {
            gManager = this;
        }
    }
}
