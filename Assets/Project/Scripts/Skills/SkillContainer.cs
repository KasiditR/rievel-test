using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private SkillUI skillUIPrefab;
    private SkillSystem _skillSystem;

    public void Init(SkillSystem skillSystem)
    {
        this._skillSystem = skillSystem;
        // this._skills = skills;
        foreach (Skill skill in skillSystem.Skills)
        {
            SkillUI skillUI = Instantiate(skillUIPrefab,Vector3.zero,Quaternion.identity,this.transform);
            skillUI.Init(skillSystem,skill);
        }
    }
}
