using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Effects : StoryboardObjectGenerator
    {
        public override void Generate()
        {   
		    simple();
            big();
            StreamLight(10746,11382);
            StreamLight (12382,12746);
            StreamLight(13655,14291);
            StreamLight(27382,32200);
            StreamLight(40837,43200);    
            StreamLight(57200,68018);
            StreamLight(68928,69564);
            StreamLight(70534,70928);
            StreamLight(71837,72473);
            StreamLight(74746,75382);
            StreamLight(76200,76746);
            StreamLight(101564,103018);
            StreamLight(131473,132109);
            StreamLight(133109,133473);
            StreamLight(134382,135018);
            StreamLight(119018,130291);
            
        }
        void simple()
        {
            var beat = Beatmap.GetTimingPointAt(30448).BeatDuration;
            var hitobjectLayer = GetLayer("Effects");
            int[] melody1 = {9928,10109,10291,
            11564,11837,
            12837,13018,13200,
            14655,15018,15382,
            20109,20837,
            25564,25928,
            34655,36109,37564,
            43382,
            39746,
            44291,44473,44655,
            45200,46291,47746,49200,50655,52109,53564,55018,55746,56018,56291,
            57928,59382,60837,61200,61564,61928,62291,63746,65200,65928,
            68109,68291,68473,
            69746,70018,
            71018,71200,71382,
            73928,74473,
            75564,76018,
            76837,77564,
            84109,84837,88473,89200,89928,
            91382,94291,95018,95746,97200,
            108109,108837,
            110291,110473,110655,110837,111018,
            111746,111928,112109,112291,112473,113200,114655,
            116109,116291,116473,116655,116837,117018,117200,117382,117564,
            119018,120473,120837,121200,121928,
            123382,123564,123746,123928,124837,126291,126655,127018,
            127746,127928,128109,128291,128473,128655,128837,129018,129200,
            130655,130837,131018,
            132291,132564,
            133564,133746,133928,
            135200,135746,136109};
            
            foreach (var hitobject in Beatmap.HitObjects)
            {
                foreach (var time in melody1)
                {
                    if (time >= hitobject.StartTime - 5 && time <= hitobject.StartTime + 5)
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            var square = GetLayer("Effects").CreateSprite("sb/particles/pixel.png", OsbOrigin.Centre);


                            double t = Random(0, Math.PI * 2);
                            var radius = Random(20, 150);
                            double x = radius * Math.Cos(t) + hitobject.Position.X;
                            double y = radius * Math.Sin(t) + hitobject.Position.Y;

                            square.Move(OsbEasing.OutExpo, hitobject.StartTime + Random(0, 100), hitobject.StartTime + 1000, hitobject.Position, x, y);
                            square.Fade(OsbEasing.OutCubic, hitobject.StartTime, hitobject.StartTime + beat * 4, 1, 0);
                            square.Scale(hitobject.StartTime, Random(0.60, 0.65));

                        }
                    }
                }
            }
        }

        void big()
        {
            var beat = Beatmap.GetTimingPointAt(30448).BeatDuration;
            var hitobjectLayer = GetLayer("Effects");
            int[] melody1 = {10473,12109,13382,14473,15746,33200,38291,39018,44837,53200,56473,
            68655,70291,71564,
            72715,72958,73200,73443,73685,
            78291,79018,79746,
            107382,
            129928,131200,132837,134109,136473,
            148109,148473,148837,149200,149564};
            foreach (var hitobject in Beatmap.HitObjects)
            {
                foreach (var time in melody1)
                {
                    if (time >= hitobject.StartTime - 5 && time <= hitobject.StartTime + 5)
                    {

                        var ring = GetLayer("Effects").CreateSprite("sb/particles/big_r.png", OsbOrigin.Centre, hitobject.Position);
                        ring.Scale(OsbEasing.OutCubic, hitobject.StartTime, hitobject.EndTime + 700, 0.25, 0.25 * 3);
                        ring.Fade(OsbEasing.InCubic, hitobject.StartTime, hitobject.EndTime + 700, 1, 0);
                        for (int i = 0; i < 25; i++)
                        {
                            var square = GetLayer("Effects").CreateSprite("sb/particles/pixel.png", OsbOrigin.Centre);


                            double t = Random(0, Math.PI * 2);
                            var radius = Random(20, 150);
                            double x = radius * Math.Cos(t) + hitobject.Position.X;
                            double y = radius * Math.Sin(t) + hitobject.Position.Y;

                            square.Move(OsbEasing.OutExpo, hitobject.StartTime + Random(0, 100), hitobject.StartTime + 1000, hitobject.Position, x, y);
                            square.Fade(OsbEasing.OutCubic, hitobject.StartTime, hitobject.StartTime + beat * 4, 1, 0);
                            square.Scale(hitobject.StartTime, Random(0.60, 0.65));

                        }
                    }
                }
            }
        }

        void StreamLight(int StartTime, int EndTime)
        {
            var beat = Beatmap.GetTimingPointAt(30448).BeatDuration;
            var hitobjectLayer = GetLayer("Effects");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime)
                    continue;

                var hitlight = GetLayer("Effects").CreateSprite("sb/particles/hl.png", OsbOrigin.Centre, hitobject.Position);
                hitlight.Scale(OsbEasing.OutCubic, hitobject.StartTime, hitobject.EndTime + 1000, 0.7, 0.7 * 2);
                hitlight.Fade(OsbEasing.OutCubic, hitobject.StartTime, hitobject.EndTime + 1000, 1, 0);

            }
        }
    }
}
