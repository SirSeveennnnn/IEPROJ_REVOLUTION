using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] List<PersistentEffectCollectible> persitentEffectsList = new();
    [SerializeField] List<TimedEffectCollectible> timedEffectsList = new();


    public void AddEffect(Collectible effect)
    {
        if (effect is TimedEffectCollectible)
        {
            timedEffectsList.Add(effect as TimedEffectCollectible);
        }
        else if (effect is PersistentEffectCollectible)
        {
            PersistentEffectCollectible newPersistentEffect = effect as PersistentEffectCollectible;
            PersistentEffectCollectible effectInList = persitentEffectsList.Find(x => x.Effect == newPersistentEffect.Effect);

            if (effectInList == null)
            {
                persitentEffectsList.Add(newPersistentEffect);
            }
            else
            {
                newPersistentEffect.AddExtraPoint();
            }
        }
    }

    public void RemoveEffect(Collectible effect)
    {
        if (effect is TimedEffectCollectible)
        {
            TimedEffectCollectible timedEffect = effect as TimedEffectCollectible;

            if (timedEffectsList.Contains(timedEffect))
            {
                timedEffectsList.Remove(timedEffect);
            }
        }
        else if (effect is PersistentEffectCollectible)
        {
            PersistentEffectCollectible persistentEffect = effect as PersistentEffectCollectible;

            if (persitentEffectsList.Contains(persistentEffect))
            {
                persitentEffectsList.Remove(persistentEffect);
            }
        }
    }

    public List<TimedEffectCollectible> GetCurrentTimedEffects(EStatusEffects timedEffect)
    {
        return (timedEffectsList.FindAll(x => x.Effect == timedEffect));
    }

    public bool HasTimedEffect(EStatusEffects effect)
    {
        return (timedEffectsList.Find(x => x.Effect == effect) != null);
    }

    public bool HasPersistentEffect(EStatusEffects effect)
    {
        return (persitentEffectsList.Find(x => x.Effect == effect) != null);
    }

    public void OnEffectTerminalEvent(EEffectTerminalEvents terminalEvent)
    {
        for (int i = 0; i < persitentEffectsList.Count; i++)
        {
            if (persitentEffectsList[i].TerminalEvent == terminalEvent)
            {
                persitentEffectsList.RemoveAt(i);
                break;
            }
        }
    }
}