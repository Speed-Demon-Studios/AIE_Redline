using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;

//using System.Drawing;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace DifficultyButtonSwitch
{
    public class ButtonSelectManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_pressAnyButton, m_title, m_speedClassSelect;

        [Header("Title")]

        [SerializeField]
        private Animator m_titleAnim;
        

        [SerializeField] private AnimationCurve m_titleFillCurve;

        [Header("Class Select")]

        [SerializeField]
        private AnimationCurve m_barChangeTween;
        [SerializeField]
        private float m_barChangeDuration, m_barLength;
        [SerializeField]
        private Transform m_speedBar, m_speedBarRed1, m_speedBarRed2, m_competitionSkillBar, m_competitionSkillBarRed1, m_competitionSkillBarRed2;
        [SerializeField]
        Animator m_debutAnimator, m_proAnimator, m_eliteAnimator;

        [Header("Vehicle Select")]
        [SerializeField]
        private TextMeshProUGUI m_pressStartToJoinText;

        [SerializeField]
        private Material m_cursorMat;

        [SerializeField]
        private AnimationCurve m_cursorPulseCurve;

        [SerializeField]
        private float m_cursorPulseDuration;

        private void OnEnable()
        {
            m_cursorMat.color = Color.white;
            Tween.Color(m_cursorMat, Color.red, m_cursorPulseDuration, 0, m_cursorPulseCurve, Tween.LoopType.Loop);
            Tween.Color(m_pressStartToJoinText, Color.red, m_cursorPulseDuration, 0, m_cursorPulseCurve, Tween.LoopType.Loop);
        }

        public void TransitionToTitle()
        {
            m_pressAnyButton.SetActive(false);
            m_title.SetActive(true);
            m_titleAnim.SetTrigger("TitleIn");

        }

        public void SpeedBarFill(float fillAmount)
        {
            Vector3 barPos = new Vector3(m_barLength - (m_barLength * fillAmount), 0, 0);
            Tween.LocalPosition(m_speedBar, barPos, m_barChangeDuration, 0.05f, m_barChangeTween);
            Tween.LocalPosition(m_speedBarRed1, barPos, m_barChangeDuration, 0, m_barChangeTween);
            Tween.LocalPosition(m_speedBarRed2, barPos, m_barChangeDuration, 0.1f, m_barChangeTween);
        }
    
        public void CompetitionSkillBarFill(float fillAmount)
        {
            Vector3 barPos = new Vector3(m_barLength - (m_barLength * fillAmount), 0, 0);
            Tween.LocalPosition(m_competitionSkillBar, barPos, m_barChangeDuration, 0.05f, m_barChangeTween);
            Tween.LocalPosition(m_competitionSkillBarRed1, barPos, m_barChangeDuration, 0, m_barChangeTween);
            Tween.LocalPosition(m_competitionSkillBarRed2, barPos, m_barChangeDuration, 0.1f, m_barChangeTween);
        }

        public void SpeedClassInfoChange(int speed)
        {
            switch (speed)
            {
                case 1:
                    m_debutAnimator.SetTrigger("TransitionIn");
                    m_proAnimator.SetTrigger("TransitionOut");
                    m_eliteAnimator.SetTrigger("TransitionOut");
                    Debug.Log("Debut");
                    break;

                case 2:
                    m_debutAnimator.SetTrigger("TransitionOut");
                    m_proAnimator.SetTrigger("TransitionIn");
                    m_eliteAnimator.SetTrigger("TransitionOut");
                    Debug.Log("Pro");
                    break;

                case 3:
                    m_debutAnimator.SetTrigger("TransitionOut");
                    m_proAnimator.SetTrigger("TransitionOut");
                    m_eliteAnimator.SetTrigger("TransitionIn");
                    Debug.Log("Elite");
                    break;

            }
        }
    }
}
