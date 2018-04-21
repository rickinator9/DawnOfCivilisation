using System;
using Assets.Source.Contexts.Game.Model.Country;
using Assets.Source.Contexts.Game.Model.Political;
using Assets.Source.Core.IoC;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Source.Contexts.Game.Commands.War
{
    #region Signals
    /// <summary>
    /// WarDeclarationParams: Parameters about the attacker and defender.
    /// </summary>
    public class WarDeclarationSignal : Signal<WarDeclarationParams> { }
    #endregion

    public struct WarDeclarationParams
    {
        public ICountry Attacker { get; set; }

        public ICountry Defender { get; set; }
    }

    public class WarDeclarationCommand : Command
    {
        [Inject]
        public WarDeclarationParams Params { get; set; }

        [Inject(CustomContextKeys.NewInstance)]
        public IWar NewWar { get; set; }

        public override void Execute()
        {
            NewWar.Name = string.Format("{0} Conquest of {1}", Params.Attacker.Name, Params.Defender.Name);
            NewWar.AddAttacker(Params.Attacker);
            NewWar.AddDefender(Params.Defender);

            foreach (var attacker in NewWar.Attackers)
            {
                attacker.Wars.Add(NewWar);
            }
            foreach (var defender in NewWar.Defenders)
            {
                defender.Wars.Add(NewWar);
            }

            Debug.LogFormat("War declared between {0} and {1}.", Params.Attacker.Name, Params.Defender.Name);
        }
    }
}