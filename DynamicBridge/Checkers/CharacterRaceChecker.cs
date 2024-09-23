using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using DynamicBridge.IPC.Glamourer;
using ECommons.GameFunctions;
using Glamourer.Api.Enums;

namespace DynamicBridge.Core
{
    public enum Race : byte
    {
        Hyur = 1,
        Elezen = 2,
        Lalafell = 3,
        Miqote = 4,
        Roegadyn = 5,
        AuRa = 6,
        Hrothgar = 7,
        Viera = 8,
    }

    public static class CharacterRaceChecker 
    {
        public unsafe static byte Race(this IPlayerCharacter player, GlamourerManager glamourer) 
        {   
            if (C.EnableGlamourer && glamourer != null) 
            {
                var (response, characterDescriptor) = glamourer.GetStateIPC(player.GameObject()->ObjectIndex);
                if (response == GlamourerApiEc.Success && characterDescriptor != null) 
                {
                    var race = characterDescriptor.SelectToken("$.Customize.Race.Value");
                    if (race != null) 
                    {
                        return race.ToObject<byte>();
                    }
                }
            }
            return player.Customize[(int) CustomizeIndex.Race];
        }
    }
}
