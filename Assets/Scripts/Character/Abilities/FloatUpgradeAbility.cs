
using UnityEngine;

namespace Vampire
{
    public class FloatUpgradeAbility<T> : Ability where T : UpgradeableFloat
    {
        [SerializeField] protected float[] upgrades;

        public void ConfigureUpgrades(float[] newUpgrades)
        {
            if (newUpgrades == null || newUpgrades.Length == 0) return;
            upgrades = (float[])newUpgrades.Clone();
        }

        public override string Description
        {
            get {
                return GetUpgradeDescription();
            }
        }

        protected override void Use()
        {
            base.Use();
            gameObject.SetActive(true);
            abilityManager.UpgradeValue<T, float>(upgrades[level]);
        }

        protected override void Upgrade()
        {
            abilityManager.UpgradeValue<T, float>(upgrades[level]);
        }

        public override bool RequirementsMet()
        {
            return level < upgrades.Length;
        }

        protected string GetUpgradeDescription()
        {
            return DescriptionUtils.GetUpgradeDescription(localizedDescription.GetLocalizedString(), upgrades[level]);
        }
    }
}