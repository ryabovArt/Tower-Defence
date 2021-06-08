using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManaSphere : MonoBehaviour, IPointerClickHandler
{
    private int manaIncome;
    private int healthIncome;
    public int ManaIncome
    {
        set
        {
            manaIncome = value;
        }
    }

    public int HealthIncome
    {
        set
        {
            healthIncome = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(manaIncome);
        Debug.Log(healthIncome);
        ManaScript.Instance.RewardInc(manaIncome);
        Goal.Instance.Heal(healthIncome);
        Destroy(this.gameObject);
    }
}
