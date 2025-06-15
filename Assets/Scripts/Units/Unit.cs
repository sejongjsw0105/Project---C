using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public enum Faction

    {
        Friendly,   // �츮 ����
        Enemy,       // �� ����
        Neutral      // �߸� ����

    }

    public enum UnitType
    {
        Melee,       // ����
        Cavalry,        // �⺴
        Ranged,         // ���Ÿ�
    }
    public Area area;              // ������ ��ġ�� ����
    public string unitName;            // ���� �̸�
    public int unitId;                 // ���� ID
    public int health;                 // ���� ü��
    public int attackPower;            // ���� ���ݷ�
    public int defensePower;           // ���� ����
    public Faction faction;           // ������ ���� (��: �Ʊ�, ����)
    public UnitType type;              // ������ ���� (��: ����, �⺴, ���Ÿ�)
    
    public List<StatusEffect> statusEffects = new List<StatusEffect> (); // ������ ���� ȿ�� ���
    private void OnMouseDown()
    {
        UnitSelector.Instance.OnUnitClicked(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameObject = this.gameObject;
    }
    public void Die()
    {
        // ������ �׾��� ���� ó��
        Destroy(gameObject);
    }
    public class Support
    {
        public AreaCondition areacond; // ������ ������ ���� ����
        public int supportAmount; // ������
        public int supportRange; // ���� ����
        public Support(AreaCondition areacond, int supportAmount, int supportRange)
        {
            this.areacond = areacond;
            this.supportAmount = supportAmount;
            this.supportRange = supportRange;
        }

    }

    public class StatusEffect
    {
        protected int duration;
        protected GameObject target;
        protected int turnCount = 0; // ���� ȿ���� ����� �� ��

        public StatusEffect(int duration, GameObject target)
        {
            this.duration = duration;
            this.target = target;
        }

    }

}
