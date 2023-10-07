using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] List<PersistentEffectCollectible> persitentEffectsList = new();
    [SerializeField] List<TimedEffectCollectible> timedEffectsList = new();


    public void AddEffect(Collectible effect)
    {
        if (effect is TimedEffectCollectible)
        {
            TimedEffectCollectible timedEffect = effect as TimedEffectCollectible;
            int i;

            for (i = 0; i < timedEffectsList.Count; i++)
            {
                if (timedEffectsList[i].Effect == timedEffect.Effect)
                {
                    if (timedEffectsList[i].EffectDuration < timedEffect.EffectDuration)
                    {
                        timedEffectsList[i].StopEffect();
                        timedEffectsList.Add(timedEffect);
                        break;
                    }
                    else
                    {
                        timedEffect.StopEffect();
                        break;
                    }
                }
            }

            if (i == timedEffectsList.Count)
            {
                timedEffectsList.Add(timedEffect);
            }
        }
        else if (effect is PersistentEffectCollectible)
        {

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

        }
    }
}