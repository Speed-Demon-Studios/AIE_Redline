using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelection : MonoBehaviour
{
    public List<GameObject> ships = new();
    public List<ShipVariant> variants = new();

    private GameObject m_currentShips;
    public ShipSelectionInfo sInfo;

    public Button readyButton;

    private GameObject m_ship;

    private int m_playerNum;
    private int m_shipIndex;
    private int m_materialIndex;


    /////////////////////////////////////////////////////////////////
    ///                                                          ///
    ///      All of the getters and setters in this script       ///
    ///                                                          ///
    /////////////////////////////////////////////////////////////////
    public void SetShipSelectionNumbers(int number) { m_playerNum = number; }
    public void SetShip(GameObject ship) { m_ship = ship; }

    private void Start()
    {
        // makest sure that the first ship is the one that shows up at the start
        m_currentShips = ships[0];

        // sets up the camera texture to display the ship
        //cam.GetComponentInChildren<Camera>().targetTexture = texture;
        //image.texture = texture;
    }

    private void OnEnable()
    {
        SetUp();
    }

    private void Update()
    {
        //m_y += 5f * Time.deltaTime;
        //cam.transform.rotation = Quaternion.Euler(0, m_y, 0);
    }

    /// <summary>
    /// Set up makes sure that all the text animation and the stats show up when the screen loads up
    /// </summary>
    public void SetUp()
    {
        // stops all the current coroutines
        StopAllCoroutines();
        // starts the text coroutine
        StartCoroutine(NameChange(variants[0].VariantName));

        GameManager.gManager.uiCInput.bSManager.TransitionToShipSelect(sInfo.shipDisplayAnim);

        List<Animator> tempList = new();
        int index = 0;
        foreach (Animator anim in sInfo.shipAnimators)
        {
            if (index != m_shipIndex)
            {
                tempList.Add(anim);
            }
            index++;
        }

        GameManager.gManager.uiCInput.bSManager.VehicleInfoChange(0, sInfo.shipAnimators[m_shipIndex], tempList);

        ships[m_shipIndex].GetComponent<ShipTypeInfo>().SwitchMaterials(m_materialIndex);
    }

    public void OnNextMat()
    {
        m_materialIndex++;
        if (m_materialIndex >= 3)
            m_materialIndex = 0;
        ships[m_shipIndex].GetComponent<ShipTypeInfo>().SwitchMaterials(m_materialIndex);
        GameManager.gManager.uiCInput.bSManager.ManufacturerChange(sInfo.manufacturerSprites[m_materialIndex], sInfo.manufacturerDisplayAnim,
            sInfo.manufacturerImage, sInfo.manufacturerImageRed);
    }

    /// <summary>
    /// OnNext will go to the next ship in the list when the input is pressed
    /// </summary>
    public void OnNext()
    {
        m_currentShips.SetActive(false); // sets the ship to false that was currently selected
        m_shipIndex += 1; // adds one to the ship index so that it goes to the next ship
        if (m_shipIndex > ships.Count - 1) // if the index goes over the count then go back to 0
        {
            m_shipIndex = 0;
        }
        m_currentShips = ships[m_shipIndex]; // set the current ship to the index
        m_currentShips.SetActive(true); // then set that current ship to true so it shows up
        //StopAllCoroutines(); // stop all Coroutines just incase the text one is still playing
        //StartCoroutine(NameChange(variants[m_shipIndex].VariantName)); // start a new text coroutine
        //sliders[0].value = variants[m_shipIndex].DefaultMaxSpeed;
        //sliders[1].value = variants[m_shipIndex].TurnSpeed;
        //sliders[2].value = variants[m_shipIndex].DefaultMaxAcceleration;
        List<Animator> tempList = new();
        int index = 0;
        foreach(Animator anim in sInfo.shipAnimators)
        {
            if(index != m_shipIndex)
            {
                tempList.Add(anim);
            }
            index++;
        }

        GameManager.gManager.uiCInput.bSManager.VehicleInfoChange(0, sInfo.shipAnimators[m_shipIndex], tempList);

        ships[m_shipIndex].GetComponent<ShipTypeInfo>().SwitchMaterials(m_materialIndex);
    }

    /// <summary>
    /// OnPrev will go to the prev ship in the list when the input is pressed
    /// </summary>
    public void OnPrev()
    {
        m_currentShips.SetActive(false); // sets the ship to false that was currently selected    
        m_shipIndex -= 1; // adds one to the ship index so that it goes to the next ship          
        if (m_shipIndex < 0) // if the index goes over the count then go back to 0                
        {                                                                                         
            m_shipIndex = ships.Count - 1;                                                        
        }                                                                                         
        m_currentShips = ships[m_shipIndex]; // set the current ship to the index                 
        m_currentShips.SetActive(true); // then set that current ship to true so it shows up      
        //StopAllCoroutines(); // stop all Coroutines just incase the text one is still playing     
        //StartCoroutine(NameChange(variants[m_shipIndex].VariantName)); // start a new text coroutine
        //sliders[0].value = variants[m_shipIndex].DefaultMaxSpeed;
        //sliders[1].value = variants[m_shipIndex].TurnSpeed;
        //sliders[2].value = variants[m_shipIndex].DefaultMaxAcceleration;
        List<Animator> tempList = new();
        int index = 0;
        foreach (Animator anim in sInfo.shipAnimators)
        {
            if (index != m_shipIndex)
            {
                tempList.Add(anim);
            }
            index++;
        }
        GameManager.gManager.uiCInput.bSManager.VehicleInfoChange(0, sInfo.shipAnimators[m_shipIndex], tempList);

        ships[m_shipIndex].GetComponent<ShipTypeInfo>().SwitchMaterials(m_materialIndex);
    }

    /// <summary>
    /// Once the player presses ready it sets all the variants to the shipControls and gets ready to spawn the models
    /// </summary>
    public void Ready()
    {
        // Sets ship variants
        m_ship.GetComponent<ShipsControls>().VariantObject = variants[m_shipIndex];
        m_ship.GetComponent<ShipsControls>().enabled = true; // Enables shipControls for movement
        GameManager.gManager.uiCInput.ReadyPlayer(m_playerNum); // Readys this player
        if (m_ship.GetComponent<VariantAudioContainer>() != null)
        {
            m_ship.GetComponent<VariantAudioContainer>().CheckVariant(m_shipIndex);
            m_ship.GetComponent<ShipsControls>().shipSelected = m_shipIndex;
        }

        m_ship.GetComponent<VariantAudioContainer>().CheckVariant(m_shipIndex);

        if (m_ship.GetComponent<ShipBlendAnimations>()) // if the ship selected has animations
            m_ship.GetComponent<ShipBlendAnimations>().enabled = true; // set the refrenece for animations

    }
    public void UnReady()
    {
        // Sets ship variants
        m_ship.GetComponent<ShipsControls>().VariantObject = null;
        m_ship.GetComponent<ShipsControls>().enabled = false; // Enables shipControls for movement 
        m_ship.GetComponent<ShipBlendAnimations>().enabled = false; // set the refrenece for animations 
        if (m_ship.GetComponent<ShipBlendAnimations>()) // if the ship selected has animations
            m_ship.GetComponent<ShipBlendAnimations>().enabled = false; // set the refrenece for animations

    }

    /// <summary>
    /// This is a effect for the text every time the ship is switch in the selection screen
    /// </summary>
    /// <param name="shipName"> What is the ships name you are trying to print out </param>
    /// <returns></returns>
    IEnumerator NameChange(string shipName)
    {
        string aToZ = "abcdefghijklmnopqrstuvwxyz"; // a string with every letter in the alphabet       
                                                                                                        
        int stringLength = shipName.Length; // an int for the ship string length                        
                                                                                                        
        string tempName = shipName; // temp reference to the orginal name of the ship                   
        for (int j = 0; j < stringLength; j++) // for each letter in the ship name                      
        {                                                                                               
            yield return new WaitForSeconds(0.005f); // wait                                            
                                                                                                        
            char randomLetter = aToZ[Random.Range(0, 24)]; // choose a random letter from aToZ          
                                                                                                        
            tempName = tempName.Remove(j, 1); // j being the index remove the letter at point j         
            tempName = tempName.Insert(j, randomLetter.ToString()); // replace it with the random letter
                                                                                                        
            //this.shipName.text = tempName; // set the text to the new text                              
        }                                                                                               
        // this is doing the same as before but now it will slow choose the correct letter                            
        for (int i = 0; i < stringLength; i++)                                                                        
        {                                                                                                             
            tempName = tempName.Remove(i, 1); // remove the letter at index i                                         
            tempName = tempName.Insert(i, shipName.ToCharArray()[i].ToString()); // replace it with the correct letter
                                                                                                                      
            for (int j = i + 1; j < stringLength; j++) // for the remaining letters                              
            {                                                                                                    
                yield return new WaitForSeconds(0.001f); // wait                                                 
                                                                                                                 
                char randomLetter = aToZ[Random.Range(0, 24)]; // choose random letter                           
                                                                                                                 
                tempName = tempName.Remove(j, 1); // remove at index j                                           
                tempName = tempName.Insert(j, randomLetter.ToString()); // replace with random letter              
            }                                                                                                    
            //this.shipName.text = tempName; // set text to new word                                                    
            yield return new WaitForSeconds(0.008f); // wait before doing it again                                    
                                                                                                                      
        }                                                                                                             
    }
}
