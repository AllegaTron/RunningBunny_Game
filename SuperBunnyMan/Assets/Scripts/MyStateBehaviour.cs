using UnityEngine;

namespace SHG.AnimatorCoder
{
    public class MyStateBehaviour : StateMachineBehaviour
    {
        [SerializeField, Tooltip("�������� ��� ������������")]
        private Parameters parameter;
        [SerializeField, Tooltip("�������, ������ �� �� ���� ������� ��� ��������")]
        private bool target;
        [SerializeField, Tooltip("������� ��������, ��������������� ��� ���������� �������")]
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
