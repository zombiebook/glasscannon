using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ItemStatsSystem;
using UnityEngine;

namespace glasscannon
{
    /// <summary>
    /// 로더가 찾는 엔트리:
    ///   glasscannon.ModBehaviour : Duckov.Modding.ModBehaviour
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public ModBehaviour()
        {
            try
            {
                Debug.Log("[GlassCannon] ModBehaviour 생성자 호출됨");

                GameObject root = new GameObject("GlassCannonRoot");
                UnityEngine.Object.DontDestroyOnLoad(root);
                root.AddComponent<GlassCannonController>();

                Debug.Log("[GlassCannon] GlassCannonController 추가 완료");
            }
            catch (Exception ex)
            {
                Debug.Log("[GlassCannon] ModBehaviour 생성자 예외: " + ex);
            }
        }
    }

    /// <summary>
    /// 유리대포 컨트롤러
    /// - 플레이어 HP/방어: 1 / 0으로 고정
    /// - 총/근접 관련 몇 가지 스탯 버프
    /// - 모든 적 Health의 Max/CurrentHealth 를 1로 묶어서 사실상 한 방 컷
    /// </summary>
internal class GlassCannonController : MonoBehaviour
{
    private const float DamageMultiplier = 10.0f;   // 총/근접 배율
    private const float EnforceInterval = 0.5f;     // 주기

    private bool _initialized;
    private float _baseGunMul = 1f;
    private float _nextEnforceTime;
    private bool _loggedFirstApply;

    private CharacterMainControl _playerChar;
    private Health _playerHealth;

    private void Update()
    {
        try
        {
            if (!_initialized)
            {
                TryInitialize();
            }

            if (!_initialized)
                return;

            if (Time.time < _nextEnforceTime)
                return;

            _nextEnforceTime = Time.time + EnforceInterval;
            ApplyGlassCannon();
        }
        catch (Exception ex)
        {
            Debug.Log("[GlassCannon] Update 예외: " + ex);
            enabled = false;
        }
    }

    /// <summary>
    /// 메인 캐릭터와 Health, 기본 총기 배율을 한 번만 잡아둔다.
    /// </summary>
    private void TryInitialize()
    {
        _playerChar = DuckovCheatUI.CharacterStatAccessor.TryGetMainCharacter();
        if (_playerChar == null)
            return;

        _playerHealth = _playerChar.Health;

        float? gunMul = DuckovCheatUI.CharacterStatAccessor.GetGunDamageMultiplier();
        _baseGunMul = (gunMul.HasValue && gunMul.Value > 0.01f) ? gunMul.Value : 1f;

        _initialized = true;
        Debug.Log("[GlassCannon] 초기화 완료. baseGunMul=" + _baseGunMul);
    }

    /// <summary>
    /// 플레이어 유리대포 + 적 HP 1 고정.
    /// </summary>
    private void ApplyGlassCannon()
    {
        try
        {
            var ch = DuckovCheatUI.CharacterStatAccessor.TryGetMainCharacter();
            if (ch == null)
                return;

            // 1) 플레이어 HP / 방어 세팅
            DuckovCheatUI.CharacterStatAccessor.SetMaxHP(1f);
            DuckovCheatUI.CharacterStatAccessor.SetCurrentHP(1f);
            DuckovCheatUI.CharacterStatAccessor.SetCurrentHP(1f); // HUD 갱신 한 번 더

            DuckovCheatUI.CharacterStatAccessor.SetBodyArmor(0f);
            DuckovCheatUI.CharacterStatAccessor.SetHeadArmor(0f);

            // 2) 공격 스탯 버프
            float strong = DamageMultiplier;        // 10
            float insane = DamageMultiplier * 5f;   // 50

            DuckovCheatUI.CharacterStatAccessor.SetGunDamageMultiplier(strong);
            DuckovCheatUI.CharacterStatAccessor.SetGunDistanceMultiplier(strong);
            DuckovCheatUI.CharacterStatAccessor.SetGunBulletSpeedMultiplier(strong);
            DuckovCheatUI.CharacterStatAccessor.SetGunScatterMultiplier(0.2f);

            DuckovCheatUI.CharacterStatAccessor.SetMeleeDamageMultiplier(strong);
            DuckovCheatUI.CharacterStatAccessor.SetMeleeCritRateGain(strong);
            DuckovCheatUI.CharacterStatAccessor.SetMeleeCritDamageGain(insane);

            DuckovCheatUI.CharacterStatAccessor.SetElemental("physics", strong);

            // 3) 모든 적 HP를 1로 묶기
            SoftenEnemies();

            if (!_loggedFirstApply)
            {
                _loggedFirstApply = true;
                Debug.Log("[GlassCannon] 유리대포 적용: PlayerHP=1, Armor=0, Gun/Melee x"
                          + strong + ", EnemyHP=1");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("[GlassCannon] ApplyGlassCannon 예외: " + ex);
        }
    }

    /// <summary>
    /// 씬 안의 모든 Health를 찾아서
    ///  - 플레이어 Health는 제외
    ///  - 나머지 MaxHealth / CurrentHealth 를 1 이하로 제한
    /// </summary>
    private void SoftenEnemies()
    {
        if (_playerHealth == null)
            return;

        try
        {
            Health[] all = GameObject.FindObjectsOfType<Health>();
            if (all == null || all.Length == 0)
                return;

            for (int i = 0; i < all.Length; i++)
            {
                Health h = all[i];
                if (h == null)
                    continue;

                // 플레이어 Health는 스킵
                if (ReferenceEquals(h, _playerHealth))
                    continue;

                // MaxHealth 를 1로 압축 (새로 만든 SetMaxHP(Health, float) 사용)
                if (h.MaxHealth > 1f)
                {
                    DuckovCheatUI.CharacterStatAccessor.SetMaxHP(h, 1f);
                }

                // CurrentHealth 도 1 이하로 잘라내기
                if (h.CurrentHealth > 1f)
                {
                    h.CurrentHealth = Mathf.Min(h.CurrentHealth, 1f);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log("[GlassCannon] SoftenEnemies 예외: " + ex);
        }
    }
  }
}
