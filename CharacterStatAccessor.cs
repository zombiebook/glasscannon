using System;
using System.Reflection;
using ItemStatsSystem;
using UnityEngine;

namespace DuckovCheatUI
{
	// Token: 0x0200000D RID: 13
	public static class CharacterStatAccessor
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000055F4 File Offset: 0x000037F4
		public static CharacterMainControl TryGetMainCharacter()
		{
			return CharacterMainControl.Main;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000560C File Offset: 0x0000380C
		private static Item GetCharItem(CharacterMainControl ch)
		{
			bool flag = ch == null;
			Item item;
			if (flag)
			{
				item = null;
			}
			else
			{
				item = ch.CharacterItem;
			}
			return item;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005634 File Offset: 0x00003834
		private static Health GetHealth(CharacterMainControl ch)
		{
			bool flag = ch == null;
			Health health;
			if (flag)
			{
				health = null;
			}
			else
			{
				health = ch.Health;
			}
			return health;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000565C File Offset: 0x0000385C
		private static Item GetHealthItem(Health h)
		{
			bool flag = h == null;
			Item item;
			if (flag)
			{
				item = null;
			}
			else
			{
				item = CharacterStatAccessor.GetPrivateField<Item>(h, "item");
			}
			return item;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005688 File Offset: 0x00003888
		private static float? GetStatBaseValue_FromChar(CharacterMainControl ch, string privateHashFieldName)
		{
			Item charItem = CharacterStatAccessor.GetCharItem(ch);
			bool flag = charItem == null;
			float? num;
			if (flag)
			{
				num = null;
			}
			else
			{
				int privateField = CharacterStatAccessor.GetPrivateField<int>(ch, privateHashFieldName);
				bool flag2 = privateField == 0;
				if (flag2)
				{
					num = null;
				}
				else
				{
					Stat stat = charItem.GetStat(privateField);
					bool flag3 = stat == null;
					if (flag3)
					{
						num = null;
					}
					else
					{
						num = new float?(stat.BaseValue);
					}
				}
			}
			return num;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005708 File Offset: 0x00003908
		private static bool SetStatBaseValue_OnChar(CharacterMainControl ch, string privateHashFieldName, float newValue)
		{
			Item charItem = CharacterStatAccessor.GetCharItem(ch);
			bool flag = charItem == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int privateField = CharacterStatAccessor.GetPrivateField<int>(ch, privateHashFieldName);
				bool flag3 = privateField == 0;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					Stat stat = charItem.GetStat(privateField);
					bool flag4 = stat == null;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						stat.BaseValue = newValue;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000576C File Offset: 0x0000396C
		private static bool SetStatBaseValue_OnHealth(Health h, string privateHashFieldName, float newValue)
		{
			bool flag = h == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Item healthItem = CharacterStatAccessor.GetHealthItem(h);
				bool flag3 = healthItem == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					int privateField = CharacterStatAccessor.GetPrivateField<int>(h, privateHashFieldName);
					bool flag4 = privateField == 0;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						Stat stat = healthItem.GetStat(privateField);
						bool flag5 = stat == null;
						if (flag5)
						{
							flag2 = false;
						}
						else
						{
							stat.BaseValue = newValue;
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000057E0 File Offset: 0x000039E0
		private static float? GetStatBaseValue_FromHealth(Health h, string privateHashFieldName)
		{
			bool flag = h == null;
			float? num;
			if (flag)
			{
				num = null;
			}
			else
			{
				Item healthItem = CharacterStatAccessor.GetHealthItem(h);
				bool flag2 = healthItem == null;
				if (flag2)
				{
					num = null;
				}
				else
				{
					int privateField = CharacterStatAccessor.GetPrivateField<int>(h, privateHashFieldName);
					bool flag3 = privateField == 0;
					if (flag3)
					{
						num = null;
					}
					else
					{
						Stat stat = healthItem.GetStat(privateField);
						bool flag4 = stat == null;
						if (flag4)
						{
							num = null;
						}
						else
						{
							num = new float?(stat.BaseValue);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000587C File Offset: 0x00003A7C
		public static float? GetCurrentHP()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			return (health != null) ? new float?(health.CurrentHealth) : null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000058BC File Offset: 0x00003ABC
		public static void SetCurrentHP(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			bool flag = health == null;
			if (!flag)
			{
				health.CurrentHealth = Mathf.Min(health.MaxHealth, v);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000058F8 File Offset: 0x00003AF8
		public static float? GetMaxHP()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			return (health != null) ? new float?(health.MaxHealth) : null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005938 File Offset: 0x00003B38
		public static void SetMaxHP(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			bool flag = health == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnHealth(health, "maxHealthHash", v);
				bool flag2 = health.CurrentHealth > health.MaxHealth;
				if (flag2)
				{
					health.CurrentHealth = health.MaxHealth;
				}
			}
		}
// 🔽 이거 새로 추가
public static void SetMaxHP(Health health, float v)
{
    if (health == null)
    {
        return;
    }

    CharacterStatAccessor.SetStatBaseValue_OnHealth(health, "maxHealthHash", v);

    if (health.CurrentHealth > health.MaxHealth)
    {
        health.CurrentHealth = health.MaxHealth;
    }
}
		// Token: 0x0600004D RID: 77 RVA: 0x00005990 File Offset: 0x00003B90
		public static float? GetBodyArmor()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			return (health != null) ? new float?(health.BodyArmor) : null;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000059D0 File Offset: 0x00003BD0
		public static void SetBodyArmor(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			bool flag = health == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnHealth(health, "bodyArmorHash", v);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005A08 File Offset: 0x00003C08
		public static float? GetHeadArmor()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			return (health != null) ? new float?(health.HeadArmor) : null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005A48 File Offset: 0x00003C48
		public static void SetHeadArmor(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			bool flag = health == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnHealth(health, "headArmorHash", v);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005A80 File Offset: 0x00003C80
		public static float? GetElementFactor(ElementTypes t)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			Health health = CharacterStatAccessor.GetHealth(characterMainControl);
			bool flag = health == null;
			float? num;
			if (flag)
			{
				num = null;
			}
			else
			{
				num = new float?(health.ElementFactor(t));
			}
			return num;
		}

		public static void SetElementFactor(ElementTypes t, float v)
{
    CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
    Health health = CharacterStatAccessor.GetHealth(characterMainControl);
    bool flag = health == null;
    if (!flag)
    {
        string text = null;

        // 🔽 enum 을 int 로 한 번 캐스팅해서 비교
        switch ((int)t)
        {
            case 0:
                text = "Hash_ElementFactor_Physics";
                break;
            case 1:
                text = "Hash_ElementFactor_Fire";
                break;
            case 2:
                text = "Hash_ElementFactor_Poison";
                break;
            case 3:
                text = "Hash_ElementFactor_Electricity";
                break;
            case 4:
                text = "Hash_ElementFactor_Space";
                break;
        }

        bool flag2 = text != null;
        if (flag2)
        {
            CharacterStatAccessor.SetStatBaseValue_OnHealth(health, text, v);
        }
    }
}

		// Token: 0x06000053 RID: 83 RVA: 0x00005B54 File Offset: 0x00003D54
public static float? GetElemental(string key)
{
    float? num;
    if (!(key == "physics"))
    {
        if (!(key == "fire"))
        {
            if (!(key == "poison"))
            {
                if (!(key == "electricity"))
                {
                    if (!(key == "space"))
                    {
                        num = null;
                    }
                    else
                    {
                        num = CharacterStatAccessor.GetElementFactor((ElementTypes)4);
                    }
                }
                else
                {
                    num = CharacterStatAccessor.GetElementFactor((ElementTypes)3);
                }
            }
            else
            {
                num = CharacterStatAccessor.GetElementFactor((ElementTypes)2);
            }
        }
        else
        {
            num = CharacterStatAccessor.GetElementFactor((ElementTypes)1);
        }
    }
    else
    {
        num = CharacterStatAccessor.GetElementFactor((ElementTypes)0);
    }
    return num;
}

public static void SetElemental(string key, float v)
{
    if (!(key == "physics"))
    {
        if (!(key == "fire"))
        {
            if (!(key == "poison"))
            {
                if (!(key == "electricity"))
                {
                    if (key == "space")
                    {
                        CharacterStatAccessor.SetElementFactor((ElementTypes)4, v);
                    }
                }
                else
                {
                    CharacterStatAccessor.SetElementFactor((ElementTypes)3, v);
                }
            }
            else
            {
                CharacterStatAccessor.SetElementFactor((ElementTypes)2, v);
            }
        }
        else
        {
            CharacterStatAccessor.SetElementFactor((ElementTypes)1, v);
        }
    }
    else
    {
        CharacterStatAccessor.SetElementFactor((ElementTypes)0, v);
    }
}

		// Token: 0x06000055 RID: 85 RVA: 0x00005C6C File Offset: 0x00003E6C
		public static float? GetCurrentStamina()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			float? num;
			if (flag)
			{
				num = null;
			}
			else
			{
				num = new float?(CharacterStatAccessor.GetPrivateField<float>(characterMainControl, "currentStamina"));
			}
			return num;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00005CAC File Offset: 0x00003EAC
		public static void SetCurrentStamina(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetPrivateField(characterMainControl, "currentStamina", v);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public static float? GetMaxStamina()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MaxStamina) : null;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005D18 File Offset: 0x00003F18
		public static void SetMaxStamina(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "maxStaminaHash", v);
				float maxStamina = characterMainControl.MaxStamina;
				float privateField = CharacterStatAccessor.GetPrivateField<float>(characterMainControl, "currentStamina");
				bool flag2 = privateField > maxStamina;
				if (flag2)
				{
					CharacterStatAccessor.SetPrivateField(characterMainControl, "currentStamina", maxStamina);
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005D78 File Offset: 0x00003F78
		public static float? GetStaminaDrainRate()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.StaminaDrainRate) : null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public static void SetStaminaDrainRate(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "staminaDrainRateHash", v);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public static float? GetStaminaRecoverRate()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.StaminaRecoverRate) : null;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005E18 File Offset: 0x00004018
		public static void SetStaminaRecoverRate(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "staminaRecoverRateHash", v);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005E48 File Offset: 0x00004048
		public static float? GetStaminaRecoverTime()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.StaminaRecoverTime) : null;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005E80 File Offset: 0x00004080
		public static void SetStaminaRecoverTime(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "staminaRecoverTimeHash", v);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005EB0 File Offset: 0x000040B0
		public static float? GetCurrentEnergy()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CurrentEnergy) : null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005EE8 File Offset: 0x000040E8
		public static void SetCurrentEnergy(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				characterMainControl.CurrentEnergy = v;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005F14 File Offset: 0x00004114
		public static float? GetMaxEnergy()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MaxEnergy) : null;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005F4C File Offset: 0x0000414C
		public static void SetMaxEnergy(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "maxEnergyHash", v);
				bool flag2 = characterMainControl.CurrentEnergy > characterMainControl.MaxEnergy;
				if (flag2)
				{
					characterMainControl.CurrentEnergy = characterMainControl.MaxEnergy;
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005F9C File Offset: 0x0000419C
		public static float? GetEnergyCostPerMin()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.EnergyCostPerMin) : null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005FD4 File Offset: 0x000041D4
		public static void SetEnergyCostPerMin(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "energyCostPerMinHash", v);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006004 File Offset: 0x00004204
		public static float? GetCurrentWater()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CurrentWater) : null;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000603C File Offset: 0x0000423C
		public static void SetCurrentWater(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				characterMainControl.CurrentWater = v;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006068 File Offset: 0x00004268
		public static float? GetMaxWater()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MaxWater) : null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000060A0 File Offset: 0x000042A0
		public static void SetMaxWater(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "maxWaterHash", v);
				bool flag2 = characterMainControl.CurrentWater > characterMainControl.MaxWater;
				if (flag2)
				{
					characterMainControl.CurrentWater = characterMainControl.MaxWater;
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000060F0 File Offset: 0x000042F0
		public static float? GetWaterCostPerMin()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.WaterCostPerMin) : null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006128 File Offset: 0x00004328
		public static void SetWaterCostPerMin(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "waterCostPerMinHash", v);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006158 File Offset: 0x00004358
		public static float? GetWaterEnergyRecoverMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.WaterEnergyRecoverMultiplier) : null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006190 File Offset: 0x00004390
		public static void SetWaterEnergyRecoverMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "waterEnergyRecoverMultiplierHash", v);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000061C0 File Offset: 0x000043C0
		public static float? GetWalkSpeed()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CharacterWalkSpeed) : null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000061F8 File Offset: 0x000043F8
		public static void SetWalkSpeed(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "walkSpeedHash", v);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006228 File Offset: 0x00004428
		public static float? GetRunSpeed()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CharacterRunSpeed) : null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006260 File Offset: 0x00004460
		public static void SetRunSpeed(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "runSpeedHash", v);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006290 File Offset: 0x00004490
		public static float? GetCharacterMoveability()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CharacterMoveability) : null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000062C8 File Offset: 0x000044C8
		public static void SetCharacterMoveability(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "moveabilityHash", v);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000062F8 File Offset: 0x000044F8
		public static float? GetTurnSpeed()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CharacterTurnSpeed) : null;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006330 File Offset: 0x00004530
		public static void SetTurnSpeed(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "turnSpeedHash", v);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006360 File Offset: 0x00004560
		public static float? GetAimTurnSpeed()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.CharacterAimTurnSpeed) : null;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006398 File Offset: 0x00004598
		public static void SetAimTurnSpeed(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "aimTurnSpeedHash", v);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000063C8 File Offset: 0x000045C8
		public static float? GetDashSpeed()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.DashSpeed) : null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006400 File Offset: 0x00004600
		public static void SetDashSpeed(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "dashSpeedHash", v);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006430 File Offset: 0x00004630
		public static float? GetGunDamageMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.GunDamageMultiplier) : null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006468 File Offset: 0x00004668
		public static void SetGunDamageMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "gunDamageMultiplierHash", v);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006498 File Offset: 0x00004698
		public static float? GetMeleeDamageMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MeleeDamageMultiplier) : null;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000064D0 File Offset: 0x000046D0
		public static void SetMeleeDamageMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "meleeDamageMultiplierHash", v);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006500 File Offset: 0x00004700
		public static float? GetRecoilControl()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.RecoilControl) : null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006538 File Offset: 0x00004738
		public static void SetRecoilControl(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "recoilControlHash", v);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006568 File Offset: 0x00004768
		public static float? GetGunScatterMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.GunScatterMultiplier) : null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000065A0 File Offset: 0x000047A0
		public static void SetGunScatterMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "GunScatterMultiplierHash", v);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000065D0 File Offset: 0x000047D0
		public static float? GetGunBulletSpeedMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.GunBulletSpeedMultiplier) : null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006608 File Offset: 0x00004808
		public static void SetGunBulletSpeedMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "gunBulletSpeedMultiplierHash", v);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006638 File Offset: 0x00004838
		public static float? GetReloadSpeedGain()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.ReloadSpeedGain) : null;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006670 File Offset: 0x00004870
		public static void SetReloadSpeedGain(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "reloadSpeedGainHash", v);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000066A0 File Offset: 0x000048A0
		public static float? GetGunDistanceMultiplier()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.GunDistanceMultiplier) : null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000066D8 File Offset: 0x000048D8
		public static void SetGunDistanceMultiplier(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "gunDistanceMultiplierHash", v);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006708 File Offset: 0x00004908
		public static float? GetMeleeCritRateGain()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MeleeCritRateGain) : null;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006740 File Offset: 0x00004940
		public static void SetMeleeCritRateGain(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "meleeCritRateGainHash", v);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006770 File Offset: 0x00004970
		public static float? GetMeleeCritDamageGain()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MeleeCritDamageGain) : null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000067A8 File Offset: 0x000049A8
		public static void SetMeleeCritDamageGain(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "meleeCritDamageGainHash", v);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000067D8 File Offset: 0x000049D8
		public static float? GetMaxWeight()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.MaxWeight) : null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006810 File Offset: 0x00004A10
		public static void SetMaxWeight(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "maxWeightHash", v);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006840 File Offset: 0x00004A40
		public static float? GetInventoryCapacity()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.InventoryCapacity) : null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006878 File Offset: 0x00004A78
		public static void SetInventoryCapacity(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "InventoryCapacityHash", v);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000068A8 File Offset: 0x00004AA8
		public static float? GetViewAngle()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.ViewAngle) : null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000068E0 File Offset: 0x00004AE0
		public static void SetViewAngle(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "viewAngleHash", v);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006910 File Offset: 0x00004B10
		public static float? GetViewDistance()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.ViewDistance) : null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006948 File Offset: 0x00004B48
		public static void SetViewDistance(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "viewDistanceHash", v);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006978 File Offset: 0x00004B78
		public static float? GetSenseRange()
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			return (characterMainControl != null) ? new float?(characterMainControl.SenseRange) : null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000069B0 File Offset: 0x00004BB0
		public static void SetSenseRange(float v)
		{
			CharacterMainControl characterMainControl = CharacterStatAccessor.TryGetMainCharacter();
			bool flag = characterMainControl == null;
			if (!flag)
			{
				CharacterStatAccessor.SetStatBaseValue_OnChar(characterMainControl, "senseRangeHash", v);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000069E0 File Offset: 0x00004BE0
		private static T GetPrivateField<T>(object obj, string fieldName)
		{
			bool flag = obj == null;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				Type type = obj.GetType();
				FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
				bool flag2 = field == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					object value = field.GetValue(obj);
					bool flag3 = value == null;
					if (flag3)
					{
						t = default(T);
					}
					else
					{
						t = (T)((object)value);
					}
				}
			}
			return t;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006A5C File Offset: 0x00004C5C
		private static bool SetPrivateField(object obj, string fieldName, object value)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Type type = obj.GetType();
				FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
				bool flag3 = field == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					field.SetValue(obj, value);
					flag2 = true;
				}
			}
			return flag2;
		}
	}
}
