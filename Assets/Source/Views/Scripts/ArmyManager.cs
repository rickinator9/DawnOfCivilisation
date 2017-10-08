using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Views.Scripts
{
    public class ArmyManager : MonoBehaviour
    {
        public ArmyView ArmyPrefab;

        void Update()
        {
            foreach (var armyWithoutView in Armies.Instance.ArmiesWithoutView)
            {
                armyWithoutView.View = CreateArmyView();
            }
        }

        IArmyView CreateArmyView()
        {
            var armyView = Instantiate(ArmyPrefab);
            armyView.gameObject.transform.SetParent(transform);

            return armyView;
        }
    }
}