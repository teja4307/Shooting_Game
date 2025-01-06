// Copyright 2021, Infima Games. All Rights Reserved.

namespace InfimaGames.LowPolyShooterPack
{
    /// <summary>
    /// Game Mode Service.
    /// </summary>
    public interface RGameModeService : RGameService
    {
        /// <summary>
        /// Returns the Player Character.
        /// </summary>
        CharacterBehaviour GetPlayerCharacter();
    }
}