namespace SHG.AnimatorCoder
{
    /// <summary> ������ ������ �������� ���� ��������� �������� </summary>
    public enum Animations
    {
        //�������� ����������� ���� ������ �� �������� ��������� ����� ��������
        Idle,
        Walk,
        Run,
        Crouch,
        Boxing,
        Push,
        Punch,
        Kick,
        FlyingKick,
        Jump,
        JumpDown,
        JumpDown2,
        Fall,
        Climbing,
        ClimbingUpOnWall,
        DodgingBack,
        RunningSlide,
        WalkingOnStairs,
        Pickup,
        Hit1,
        Hit2,
        Hit3,
        Hit4,
        Dying,
        RESET  //����������� �����
    }

    /// <summary> ������ ������ ���� ���������� ��������� </summary>
    public enum Parameters
    {
        //�������� ����������� ���� ������ � ������������ � ������ ����������� ���������
        GROUNDED,
        FALLING
    }
}
