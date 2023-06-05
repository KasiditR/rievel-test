using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _skillText;
    [SerializeField] private Skill skill;
    [SerializeField] private UIVirtualButton uIVirtualButton;

    private SkillSystem _skillSystem;
    public void Init(SkillSystem skillSystem,Skill skill)
    {
        this._skillSystem = skillSystem;
        this.skill = skill;
        this._skillText.text = skill.name;
        uIVirtualButton.buttonClickOutputEvent.AddListener(OnClickSkill);
    }

    private void OnClickSkill()
    {
        _skillSystem.UseSkill(skill);
    }

}
