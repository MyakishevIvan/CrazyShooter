using CrazyShooter.Enum;
using CrazyShooter.Enums;

namespace Controllers
{
    public class GameModel
    {
        public PlayerType PlayerType { get; private set; }
        public WeaponType WeaponType { get;  private set; }
        public int CurrnentMapLevel { get;  private set; }

        public void InitGameMode(PlayerType playerType, WeaponType weaponType, int currentMapLevel)
        {
            PlayerType = playerType;
            WeaponType = weaponType;
            CurrnentMapLevel = currentMapLevel;
        }
    }
}