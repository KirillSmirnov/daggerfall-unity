﻿// Project:         Daggerfall Tools For Unity
// Copyright:       Copyright (C) 2009-2018 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using UnityEngine;
using DaggerfallConnect;
using DaggerfallWorkshop.Game.Entity;
using FullSerializer;

namespace DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects
{
    /// <summary>
    /// Base class for Transfer stat effect classes.
    /// Essentially a DrainEffect on target with a HealEffect step for caster.
    /// </summary>
    public abstract class TransferEffect : DrainEffect
    {
        protected override bool IsLikeKind(IncumbentEffect other)
        {
            return (other is TransferEffect && (other as TransferEffect).drainStat == drainStat) ? true : false;
        }

        protected override void BecomeIncumbent()
        {
            base.BecomeIncumbent();
            HealCaster();
        }

        protected override void AddState(IncumbentEffect incumbent)
        {
            base.AddState(incumbent);
            HealCaster();
        }

        void HealCaster()
        {
            if (caster)
            {
                DrainEffect incumbentDrain = caster.GetComponent<EntityEffectManager>().FindDrainStatIncumbent(drainStat);
                if (incumbentDrain != null)
                    incumbentDrain.Heal(lastMagnitudeIncreaseAmount);
            }
        }
    }
}