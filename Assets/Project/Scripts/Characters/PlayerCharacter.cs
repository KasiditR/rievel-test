using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] private float _exp;
    public event Action<float> onExpChange;
    [SerializeField] private float _money;
    public event Action<float> onMoneyChange;
    [Header("Config Mana Point")]
    [SerializeField] private float _currentMana;
    [SerializeField] private int _maxStamina = 100;
    [SerializeField] private float _manaRegenerationAmount = 1;
    public event Action<float> onManaChange;
    [Header("Config Stamina")]
    [SerializeField] private float _currentStamina;
    [SerializeField] private int _maxMana = 100;
    [SerializeField] private float _staminaRegenerationAmount = 1;
    public event Action<float> onStaminaChange;

    [Header("Config Time Regenerate")]
    [SerializeField] private float _regenerationInterval = 1f;

    [Header("Config Skill")]
    [SerializeField] private SkillSystem _skillSystem;
    [SerializeField] private Skill[] _skills;

    [SerializeField] private float _damageMelee;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask _monsterLayer;

    private bool _isRegenerating;

    public float DamageMelee => _damageMelee;

    public int MaxStamina { get => _maxStamina; }
    public int MaxMana { get => _maxMana; }
    public float GetExp()
    {
        return _exp;
    }
    public void SetExp(float value)
    {
        _exp = value;
        onExpChange?.Invoke(GetExp());
    }

    public float GetMoney()
    {
        return _money;
    }
    public void SetMoney(float value)
    {
        _money = value;
        onMoneyChange?.Invoke(GetMoney());
    }

    private void Awake() 
    {
        this.health = this.maxHealth;
        _currentStamina = _maxStamina;
        _currentMana = _maxMana;
        StartCoroutine(RegenerateStamina());
        StartCoroutine(RegenerateMana());

        _skillSystem.InitSkill(_skills);
        _skillSystem.OnSkillUsed += OnSkill;
    }

    private void OnSkill(Skill skill)
    {
        switch (skill.skillType)
        {
            case SkillType.Heal:
                this.health += skill.value;
                this.OnHealthChange(health);
                break;
            case SkillType.AOE:
                AOEAttack(skill);
                break;
            case SkillType.Slash:
                SlashAttack(skill);
                break;
            default:
                break;
        }

    }

    private void SlashAttack(Skill skill)
    {
        // Get a projectile from the object pool
        GameObject projectileRef = Resources.Load("Slash") as GameObject;
        GameObject projectile = Instantiate(projectileRef, Vector3.zero, Quaternion.identity);


        // Set the projectile's position and rotation to match the shooter's
        projectile.transform.position = _point.position;
        projectile.transform.rotation = _point.rotation;

        // Activate the projectile
        projectile.SetActive(true);

        Vector3 target = this._point.forward * 20;
        ObjectPooling objectPooling = projectile.GetComponent<ObjectPooling>();
        objectPooling.Init(skill.value, (int)skill.value);
        objectPooling.onHit += () => Destroy(projectile);
        objectPooling.Launch("Monster", target);
    }

    private void AOEAttack(Skill skill)
    {
        Debug.Log(_monsterLayer);
        Collider[] hitMonsters = Physics.OverlapSphere(this.transform.position, 3, _monsterLayer);
        foreach (Collider hit in hitMonsters)
        {
            Debug.Log(hit.gameObject.name);
            IDamageable damageable = hit.GetComponent<IDamageable>();
            damageable?.TakeDamage(DamageMelee);
        }
    }

    private IEnumerator RegenerateStamina()
    {
        while (true)
        {
            if (_currentStamina < _maxStamina && !_isRegenerating)
            {
                _isRegenerating = true;
                yield return StartCoroutine(IncreaseStamina());
                _isRegenerating = false;
            }
            yield return null;
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {

            if (_currentMana < _maxMana && !_isRegenerating)
            {
                _isRegenerating = true;
                yield return StartCoroutine(IncreaseMana());
                _isRegenerating = false;
            }

            yield return null;
        }
    }

    private IEnumerator IncreaseStamina()
    {
        while (_currentStamina < _maxStamina)
        {
            _currentStamina += _staminaRegenerationAmount;
            _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
            onStaminaChange?.Invoke(_currentStamina);
            yield return new WaitForSeconds(_regenerationInterval);
        }
    }

    private IEnumerator IncreaseMana()
    {
        while (_currentMana < _maxMana)
        {
            _currentMana += _manaRegenerationAmount;
            _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
            onManaChange?.Invoke(_currentMana);
            yield return new WaitForSeconds(_regenerationInterval);
        }
    }

    public float GetManaPoint()
    {
        return _currentMana;
    }

    public void DecreaseManaPoint(float value)
    {
        _currentMana -= value;
        onManaChange?.Invoke(_currentMana);
    }

    public float GetStamina()
    {
        return _currentStamina;
    }

    public void DecreaseStamina(float value)
    {
        _currentStamina -= value;
        onStaminaChange?.Invoke(_currentStamina);
    }

    private void OnEnable()
    {
        this.onDie += OnDie;
    }
    private void OnDisable()
    {
        this.onDie -= OnDie;
    }
    private void OnDie()
    {
        Debug.Log("Player is Die");
    }
}
