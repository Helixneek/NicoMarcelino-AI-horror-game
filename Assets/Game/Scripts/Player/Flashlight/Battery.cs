using AgateAIHorror.Player;
using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class Battery : Item
    {
        public override void Pickup(PlayerCharacter player)
        {
            base.Pickup(player);

            player.Flashlight.RefillBattery();
        }
    }
}
