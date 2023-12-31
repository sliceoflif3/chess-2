using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatController : MonoBehaviour
{
    public static PlayerStatController instance;

    private void Awake()
    {
        instance = this;
    }

    public List<PlayerStatValue> moveSpeed, health, pickupRange, maxWeapon;
    public int moveSpeedLevelCount, healthLevelCount, pickupRangeLevelCount;

    public int moveSpeedLevel, healthLevel, pickupRangeLevel, maxWeaponLevel;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = moveSpeed.Count - 1; i < moveSpeedLevelCount ;  i++)
        {
            moveSpeed.Add(new PlayerStatValue(moveSpeed[i].cost + moveSpeed[1].cost, moveSpeed[i].value + (moveSpeed[1].value - moveSpeed[0].value)));
        }
        for (int i = health.Count - 1; i < healthLevelCount; i++)
        {
            health.Add(new PlayerStatValue(health[i].cost + health[1].cost, health[i].value + (health[1].value - health[0].value)));
        }
        for (int i = pickupRange.Count - 1; i < pickupRangeLevelCount; i++)
        {
            pickupRange.Add(new PlayerStatValue(pickupRange[i].cost + pickupRange[1].cost, pickupRange[i].value + (pickupRange[1].value - pickupRange[0].value)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UIController.instance.levelUpPanel.activeSelf == true)
        {
            UpdateDipslay();
        }
    }

    public void UpdateDipslay()
    {
        if(moveSpeedLevel < moveSpeed.Count - 1)
        {
            UIController.instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeed[moveSpeedLevel + 1].cost, moveSpeed[moveSpeedLevel].value, moveSpeed[moveSpeedLevel + 1].value);
        } else
        {
            UIController.instance.moveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (healthLevel < health.Count - 1)
        {
            UIController.instance.healthUpgradeDisplay.UpdateDisplay(health[healthLevel + 1].cost, health[healthLevel].value, health[healthLevel + 1].value);
        }
        else
        {
            UIController.instance.healthUpgradeDisplay.ShowMaxLevel();
        }

        if (pickupRangeLevel < pickupRange.Count - 1)
        {
            UIController.instance.pickupRangeUpgradeDisplay.UpdateDisplay(pickupRange[pickupRangeLevel + 1].cost, pickupRange[pickupRangeLevel].value, pickupRange[pickupRangeLevel + 1].value);
        }
        else
        {
            UIController.instance.pickupRangeUpgradeDisplay.ShowMaxLevel();
        }

        if (maxWeaponLevel < maxWeapon.Count - 1)
        {
            UIController.instance.maxWeaponUpgradeDisplay.UpdateDisplay(maxWeapon[maxWeaponLevel + 1].cost, maxWeapon[maxWeaponLevel].value, maxWeapon[maxWeaponLevel + 1].value);
        }
        else
        {
            UIController.instance.maxWeaponUpgradeDisplay.ShowMaxLevel();
        }
    }

    public void PurchaseMoveSpeed()
    {
        moveSpeedLevel++;
        CoinController.instance.spendCoins(moveSpeed[moveSpeedLevel].cost);

        PlayerController.instance.moveSpeed = moveSpeed[moveSpeedLevel].value;
        UpdateDipslay();
    }

    public void PurchaseHealth()
    {
        healthLevel++;
        CoinController.instance.spendCoins(health[healthLevel].cost);

        PlayerHealthController.instance.maxHealth = health[healthLevel].value;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        UpdateDipslay();
    }

    public void PurchasePickupRange()
    {
        pickupRangeLevel++;
        CoinController.instance.spendCoins(pickupRange[pickupRangeLevel].cost);

        PlayerController.instance.pickupRange = pickupRange[pickupRangeLevel].value;
        UpdateDipslay();
    }

    public void PurchaseMaxWeapon()
    {
        maxWeaponLevel++;
        CoinController.instance.spendCoins(maxWeapon[maxWeaponLevel].cost);

        PlayerController.instance.maxWeapons = Mathf.RoundToInt(maxWeapon[maxWeaponLevel].value);
        UpdateDipslay();
    }
}

[System.Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;

    public PlayerStatValue(int newCost, float newValue)
    {
        this.cost = newCost;
        this.value = newValue;
    }
}
