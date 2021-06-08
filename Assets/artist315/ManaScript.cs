using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : Singleton<ManaScript>
{

    [HideInInspector]public int mana { get; set; }
    [SerializeField]private int manaIncome;
    [SerializeField]private int startMana;
    [SerializeField]private Image manaBar;
    [SerializeField]private Text manaText;


    private void Start()
    {
        mana = startMana;
        updateUI();
        ChangeScene.OnLevelStarted += OnLevelStarted;
        
    }

    private IEnumerator Income()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            RewardInc(manaIncome);
        }       
    }

    public bool Build(int cost)
    {
        bool enogth;
        if (mana >= cost)
        {
            mana -= cost;
            enogth = true;
        }
        else
        {
            enogth = false;
        }
        updateUI();
        return enogth;
    }

    public void RewardInc(int reward)
    {
        mana += reward;
        updateUI();
    }

    private void updateUI()
    {
        manaText.text = mana.ToString();
    }

    private void OnLevelStarted()
    {
        StartCoroutine(Income());
    }
}
