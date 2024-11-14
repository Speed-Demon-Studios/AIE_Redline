using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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
        private Transform m_topSpeedBar, m_topSpeedBarRed1, m_topSpeedBarRed2, m_accelerationBar, m_accelerationBarRed1, m_accelerationBarRed2, m_handlingBar, m_handlingBarRed1, m_handlingBarRed2;

        [SerializeField]
        Animator m_splitwingAnim, m_fulcrumAnim, m_cutlassAnim, m_shipDisplayAnim, m_manufacturerDisplayAnim;

        [SerializeField]
        private GameObject[] m_SplitwingModel, m_FulcrumModel, m_CutlassModel;

        [SerializeField]
        private GameObject m_citadelShips, m_falconShips, m_monarchShips;

        public int m_manufacturer = 1;

        [SerializeField]
        private Sprite m_citadelImage, m_falconImage, m_monarchImage;

        [SerializeField]
        private Image m_manufacturerImage, m_manufacturerImageRed;



        [SerializeField]
        private Material m_cursorMat;

        [SerializeField]
        private AnimationCurve m_cursorPulseCurve;

        [SerializeField]
        private float m_cursorPulseDuration;

        private void Update()
        {
            // for testing manufacturer change
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("CHAAAAAANGE");
                ManufacturerChange();
            }
        }

        private void OnEnable()
        {
            m_cursorMat.color = Color.white;
            Tween.Color(m_cursorMat, Color.red, m_cursorPulseDuration, 0, m_cursorPulseCurve, Tween.LoopType.Loop);
            Tween.Color(m_pressStartToJoinText, Color.red, m_cursorPulseDuration, 0, m_cursorPulseCurve, Tween.LoopType.Loop);
            m_shipDisplayAnim.SetTrigger("RotateShipDisplay");

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

        public void VehicleInfoChange(int ship)
        {
            switch (ship)
            {
                case 1:
                    m_splitwingAnim.SetTrigger("TransitionIn");
                    m_fulcrumAnim.SetTrigger("TransitionOut");
                    m_cutlassAnim.SetTrigger("TransitionOut");
                    foreach (GameObject model in m_SplitwingModel)
                    {
                        model.SetActive(true);
                    }
                    foreach (GameObject model in m_FulcrumModel)
                    {
                        model.SetActive(false);
                    }
                    foreach (GameObject model in m_CutlassModel)
                    {
                        model.SetActive(false);
                    }
                    break;

                case 2:
                    m_splitwingAnim.SetTrigger("TransitionOut");
                    m_fulcrumAnim.SetTrigger("TransitionIn");
                    m_cutlassAnim.SetTrigger("TransitionOut");
                    foreach (GameObject model in m_SplitwingModel)
                    {
                        model.SetActive(false);
                    }
                    foreach (GameObject model in m_FulcrumModel)
                    {
                        model.SetActive(true);
                    }
                    foreach (GameObject model in m_CutlassModel)
                    {
                        model.SetActive(false);
                    }
                    break;

                case 3:
                    m_splitwingAnim.SetTrigger("TransitionOut");
                    m_fulcrumAnim.SetTrigger("TransitionOut");
                    m_cutlassAnim.SetTrigger("TransitionIn");
                    foreach (GameObject model in m_SplitwingModel)
                    {
                        model.SetActive(false);
                    }
                    foreach (GameObject model in m_FulcrumModel)
                    {
                        model.SetActive(false);
                    }
                    foreach (GameObject model in m_CutlassModel)
                    {
                        model.SetActive(true);
                    }
                    break;

            }
        }

        // MANUFACTURERS: 1 = Citadel, 2 = Falcon, 3 = Monarch
        public void ManufacturerChange()
        {
            switch (m_manufacturer)
            {
                case 1:
                    m_manufacturer = 2;
                    m_manufacturerDisplayAnim.SetTrigger("ChangeManufacturer");
                    m_manufacturerImage.sprite = m_falconImage;
                    m_manufacturerImageRed.sprite = m_falconImage;
                    m_citadelShips.SetActive(false);
                    m_falconShips.SetActive(true);
                    m_monarchShips.SetActive(false);

                    break;

                case 2:
                    m_manufacturer = 3;
                    m_manufacturerDisplayAnim.SetTrigger("ChangeManufacturer");
                    m_manufacturerImage.sprite = m_monarchImage;
                    m_manufacturerImageRed.sprite = m_monarchImage;
                    m_citadelShips.SetActive(false);
                    m_falconShips.SetActive(false);
                    m_monarchShips.SetActive(true);
                    break;

                case 3:
                    m_manufacturer = 1;
                    m_manufacturerDisplayAnim.SetTrigger("ChangeManufacturer");
                    m_manufacturerImage.sprite = m_citadelImage;
                    m_manufacturerImageRed.sprite = m_citadelImage;
                    m_citadelShips.SetActive(true);
                    m_falconShips.SetActive(false);
                    m_monarchShips.SetActive(false);
                    break;
            }
        }

        public void TopSpeedBarFill(float fillAmount)
        {
            Vector3 barPos = new Vector3(m_barLength - (m_barLength * fillAmount), 0, 0);
            Tween.LocalPosition(m_topSpeedBar, barPos, m_barChangeDuration, 0.05f, m_barChangeTween);
            Tween.LocalPosition(m_topSpeedBarRed1, barPos, m_barChangeDuration, 0, m_barChangeTween);
            Tween.LocalPosition(m_topSpeedBarRed2, barPos, m_barChangeDuration, 0.1f, m_barChangeTween);
        }

        public void AccelerationBarFill(float fillAmount)
        {
            Vector3 barPos = new Vector3(m_barLength - (m_barLength * fillAmount), 0, 0);
            Tween.LocalPosition(m_accelerationBar, barPos, m_barChangeDuration, 0.05f, m_barChangeTween);
            Tween.LocalPosition(m_accelerationBarRed1, barPos, m_barChangeDuration, 0, m_barChangeTween);
            Tween.LocalPosition(m_accelerationBarRed2, barPos, m_barChangeDuration, 0.1f, m_barChangeTween);
        }

        public void HandlingBarFill(float fillAmount)
        {
            Vector3 barPos = new Vector3(m_barLength - (m_barLength * fillAmount), 0, 0);
            Tween.LocalPosition(m_handlingBar, barPos, m_barChangeDuration, 0.05f, m_barChangeTween);
            Tween.LocalPosition(m_handlingBarRed1, barPos, m_barChangeDuration, 0, m_barChangeTween);
            Tween.LocalPosition(m_handlingBarRed2, barPos, m_barChangeDuration, 0.1f, m_barChangeTween);
        }
    }
}
