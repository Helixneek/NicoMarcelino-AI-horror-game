using AgateAIHorror.Player;
using UnityEngine;

public interface IInteractable
{
    public string Name { get; }
    public void Interact(PlayerCharacter player);
}
