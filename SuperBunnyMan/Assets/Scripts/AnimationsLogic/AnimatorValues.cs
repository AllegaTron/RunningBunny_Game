namespace SHG.AnimatorCoder
{
    /// <summary> ѕолный список названий всех состо€ний анимации </summary>
    public enum Animations
    {
        //»змените приведенный ниже список на названи€ состо€ний вашей анимации
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
        RESET  //ѕродолжайте сброс
    }

    /// <summary> ѕолный список всех параметров аниматора </summary>
    public enum Parameters
    {
        //»змените приведенный ниже список в соответствии с вашими параметрами аниматора
        GROUNDED,
        FALLING
    }
}
