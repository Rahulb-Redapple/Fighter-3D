namespace Fighting3D
{
    internal enum PunchType
    {
        NONE = 0,
        JAB_R = 101,
        JAB_L = 102,
        HOOK_L = 103, 
        HOOK_R = 104,
        OVERHAND_R = 105,
        OVERHAND_L = 106,
        BACKFIST_ROUND_L = 107, 
        BACKFIST_ROUND_R = 108
    }

    internal enum KickType
    {
        NONE = 0
    }

    internal enum BodyPart
    {
        NONE,
        HEAD,
        ABDOMEN,
        RIGHT_HAND_FIST,
        LEFT_HAND_FIST
    }

    internal enum HitType
    {
        NONE = 0,
        M_HIGHFRONT_MID = 101,
        M_HIGHFRONT_STAGGER = 102,
        M_HIGHFRONT_WEAK = 103
    }

    internal enum AttackType
    {
        NONE,
        PUNCH,
        KICK
    }

    internal enum PlayerAction
    {
        NONE = 0,
        MOVE,
        KICK,
        PUNCH
    }
}
