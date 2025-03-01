﻿using EntityStates;
using RoR2;

namespace ModdedEntityStates.TeslaTrooper.Tower {

    public class TowerSpawnState : BaseState
    {
        public static float TowerSpawnDuration = 1.5f;
        protected float duration;

        public override void OnEnter()
        {
            duration = TowerSpawnDuration / attackSpeedStat;

            //I am still fucking exploding
            Util.PlaySound("Play_buliding_uplace", gameObject);
            base.PlayAnimation("Body", "ConstructionComplete", "construct.playbackRate", this.duration);
            base.OnEnter();

            characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);

            TeslaTowerControllerController towerController = characterBody.master.minionOwnership?.ownerMaster.GetBodyObject()?.GetComponent<TeslaTowerControllerController>();

            if (towerController) {
                towerController.addTower(gameObject);

                //characterBody.masterObject.GetComponent<Deployable>()?.onUndeploy.AddListener(() => {
                //    towerController.removeTower(gameObject);
                //});
            }
        }

        public override void FixedUpdate() {
            base.FixedUpdate();
            if (base.fixedAge >= this.duration && base.isAuthority) {

                this.outer.SetNextState(new TowerIdleSearch {
                    justSpawned = true
                });
            }
        }
    }

}
