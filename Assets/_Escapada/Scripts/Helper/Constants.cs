using UnityEngine;

namespace Constants 
{
	public static class PlayerAnimationParameters
	{
		public const string SPEED = "Speed";
		public const string JUMP = "Jump";
		public const string MELEE = "Melee";
		public const string THROW = "Throw";
		public const string HURT = "Hurt";
		public const string DEATH = "Death";
	}

	public static class CornAnimationParameters
	{
		public const string SPEED = "Speed";
		public const string MELEE = "Melee";
		public const string THROW = "Throw";
		public const string HURT = "Hurt";
		public const string DEATH = "Death";
	}

	public static class PlayerAnimationStates 
	{
		public const string IDLE = "Fam Idle";
		public const string WALK = "Fam Run";
		public const string JUMP = "Fam Jump";
		public const string MELEE = "Fam Melee";
		public const string THROW = "Fam Throw";
		public const string TAKE_DAMAGE = "Fam Hurt";
		public const string DIE = "Fam Death";
	}

	public static class OwlerAnimationStates
	{
		public const string IDLE = "Owler Idle";
		public const string WALK = "Owler Walk";
		public const string JUMP = "Owler Jump";
		public const string MELEE = "Owler Melee";
		public const string THROW = "Owler Throw";
	}

	public static class Layers
	{
		public const int PLAYER = 8;
		public const int ENEMY = 9;
		public const int TRAP = 10;
		public const int PLATFORM = 11;
		public const int WEAPON = 12;
		public const int FOOD = 13;
		public const int GOLD = 14;
		public const int BULLET = 15;
		public const int THROWING_WEAPON = 16;
		public const int ZONE = 17;
	}

	public static class PlayerPrefs 
	{
		public readonly static string LEVEL = "Level";
	}

	public static class ResultMessageConstants
	{
		public static readonly string FAILED = "LEVEL FAILED";
		public static readonly string COMPLETED = "LEVEL COMPLETED";
	}

	public static class Paths 
	{
		public static readonly string DATA = $"{Application.persistentDataPath}/Datas";
	}
}
