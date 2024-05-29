using UnityEngine;

namespace SHG.AnimatorCoder
{
    public class MyStateBehaviour : StateMachineBehaviour
    {
        [SerializeField, Tooltip("Параметр для тестирования")]
        private Parameters parameter;
        [SerializeField, Tooltip("Укажите, должен ли он быть включен или выключен")]
        private bool target;
        [SerializeField, Tooltip("Цепочка анимаций, воспроизводимых при выполнении условия")]
        private AnimationData[] nextAnimations;

        private AnimatorCoder animatorBrain;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animatorBrain = animator.GetComponent<AnimatorCoder>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animatorBrain.GetBool(parameter) != target) return;
            animatorBrain.SetLocked(false, layerIndex);

            for (int i = 0; i < nextAnimations.Length - 1; ++i)
                nextAnimations[i].nextAnimation = nextAnimations[i + 1];

            animatorBrain.Play(nextAnimations[0], layerIndex);
        }
    }
}
