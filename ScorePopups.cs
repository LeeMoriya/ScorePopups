using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Partiality;
using Partiality.Modloader;

[assembly: IgnoresAccessChecksTo("Assembly-CSharp")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[module: UnverifiableCode]
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class IgnoresAccessChecksToAttribute : Attribute
    {
        public IgnoresAccessChecksToAttribute(string assemblyName)
        {
            AssemblyName = assemblyName;
        }
        public string AssemblyName { get; }
    }
}
public class ScorePopups : PartialityMod
{
    public static PartialityMod mod;
    public static int cycleScore = 0;
    public ScorePopups()
    {
        this.ModID = "ScorePopups";
        this.Version = "1.0";
        this.author = "LeeMoriya";
    }

    public override void OnLoad()
    {
        base.OnLoad();
        Hook();
        mod = this;
    }

    public static void Hook()
    {
        On.SocialEventRecognizer.Killing += SocialEventRecognizer_Killing;
        On.Player.ctor += Player_ctor;
    }

    private static void Player_ctor(On.Player.orig_ctor orig, Player self, AbstractCreature abstractCreature, World world)
    {
        orig.Invoke(self, abstractCreature, world);
        cycleScore = 0;
    }

    private static void SocialEventRecognizer_Killing(On.SocialEventRecognizer.orig_Killing orig, SocialEventRecognizer self, Creature killer, Creature victim)
    {
        orig.Invoke(self, killer, victim);
        if (killer is Player && self.room.game.rainWorld.progression.currentSaveState.saveStateNumber == 2)
        {
            int critNum = ReturnCreatureScoreValue(victim.Template.type);
            cycleScore += critNum;
            if (critNum > 0)
            {
                self.room.game.cameras[0].hud.textPrompt.AddMessage("Points: +" + critNum + " | Total: " + cycleScore, 1, 100, false, false);
            }
        }
    }
    public static int ReturnCreatureScoreValue(CreatureTemplate.Type crit)
    {
        switch (crit)
        {
            case CreatureTemplate.Type.PinkLizard:
                return 7;
            case CreatureTemplate.Type.GreenLizard:
                return 10;
            case CreatureTemplate.Type.BlueLizard:
                return 6;
            case CreatureTemplate.Type.YellowLizard:
                return 6;
            case CreatureTemplate.Type.WhiteLizard:
                return 8;
            case CreatureTemplate.Type.RedLizard:
                return 25;
            case CreatureTemplate.Type.BlackLizard:
                return 7;
            case CreatureTemplate.Type.Salamander:
                return 7;
            case CreatureTemplate.Type.CyanLizard:
                return 9;
            case CreatureTemplate.Type.Snail:
                return 1;
            case CreatureTemplate.Type.Vulture:
                return 15;
            case CreatureTemplate.Type.LanternMouse:
                return 2;
            case CreatureTemplate.Type.CicadaA:
                return 2;
            case CreatureTemplate.Type.CicadaB:
                return 2;
            case CreatureTemplate.Type.JetFish:
                return 4;
            case CreatureTemplate.Type.BigEel:
                return 25;
            case CreatureTemplate.Type.DaddyLongLegs:
                return 25;
            case CreatureTemplate.Type.BrotherLongLegs:
                return 14;
            case CreatureTemplate.Type.TentaclePlant:
                return 7;
            case CreatureTemplate.Type.PoleMimic:
                return 2;
            case CreatureTemplate.Type.MirosBird:
                return 16;
            case CreatureTemplate.Type.Centipede:
                return 7;
            case CreatureTemplate.Type.RedCentipede:
                return 19;
            case CreatureTemplate.Type.Centiwing:
                return 5;
            case CreatureTemplate.Type.SmallCentipede:
                return 4;
            case CreatureTemplate.Type.Scavenger:
                return 6;
            case CreatureTemplate.Type.EggBug:
                return 2;
            case CreatureTemplate.Type.BigSpider:
                return 4;
            case CreatureTemplate.Type.SpitterSpider:
                return 5;
            case CreatureTemplate.Type.BigNeedleWorm:
                return 5;
            case CreatureTemplate.Type.DropBug:
                return 5;
            case CreatureTemplate.Type.KingVulture:
                return 25;
            case CreatureTemplate.Type.Hazer:
                return 1;
        }
        return 0;
    }
}

