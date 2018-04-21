using Assets.Source.Contexts.Game.Model.Political;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Contexts.Game.UI.Typed.Panels
{
    public class WarOverviewPanelView : TypedUiView<IWar>
    {
        [SerializeField] private Text WarNameText;
        [SerializeField] private BeligerentListPanelView AttackersListView;
        [SerializeField] private BeligerentListPanelView DefendersListView;

        public void UpdateValues(IWar war)
        {
            WarNameText.text = war.Name;

            AttackersListView.ResetAndSpawn(war.Attackers);
            DefendersListView.ResetAndSpawn(war.Defenders);
        }
    }
}