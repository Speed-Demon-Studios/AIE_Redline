using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace DifficultyButtonSwitch
{
    public class ButtonSelectManager : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve m_barChangeTween;
        [SerializeField]
        private float m_barChangeDuration, m_barLength;
        [SerializeField]
        private Transform m_speedBar, m_speedBarRed1, m_speedBarRed2, m_competitionSkillBar, m_competitionSkillBarRed1, m_competitionSkillBarRed2;
        [SerializeField]
        Animator m_debutAnimator, m_proAnimator, m_eliteAnimator;

        [SerializeField]
        private Material m_cursorMat;

        [SerializeField]
        private AnimationCurve m_cursorPulseCurve;

        private void OnEnable()
        {
            Tween.Color(m_cursorMat, Color.red, 1, 0, m_cursorPulseCurve, Tween.LoopType.Loop);
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
