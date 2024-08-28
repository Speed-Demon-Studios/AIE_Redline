using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCountdown : MonoBehaviour
{
    private bool m_readyForCountdown = false;
    private bool m_countdownCoroutineStarted = false;
    private bool m_countdownFinished = false;
    private bool m_countdownStarted = false;

    private void Awake()
    {
        m_countdownStarted = false;
        m_countdownFinished = false;
        m_countdownCoroutineStarted = false;
        m_readyForCountdown = true;
    }

    public IEnumerator RaceCountdownTimer()
    {
        if (m_countdownStarted == false)
        {
            m_countdownStarted = true;
        }

        // Tell the rest of the script that the coroutine has started.
        m_countdownCoroutineStarted = true;

        if (GameManager.gManager.countdownIndex > 0)
        {
            GameManager.gManager.countdownIndex -= 1;
            Debug.Log("Countdown: " + GameManager.gManager.countdownIndex);
        }
        else if (GameManager.gManager.countdownIndex <= 0)
        {
            m_countdownFinished = true;
            m_countdownCoroutineStarted = false;
            StopCoroutine(RaceCountdownTimer());
        }
        yield return new WaitForSecondsRealtime(1);
        m_countdownCoroutineStarted = false;
        StopCoroutine(RaceCountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_readyForCountdown == true)
        {
            if (m_countdownFinished == false)
            {
                if (m_countdownCoroutineStarted == false)
                {
                    StartCoroutine(RaceCountdownTimer());
                }
            }
        }

        if (m_countdownFinished == true)
        {
            GameManager.gManager.raceStarted = true;
        }
    }
}
