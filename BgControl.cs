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
    public class BgControl : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var Bgog = GetLayer("Background").CreateSprite("Libera me.png");
            Bgog.Fade(0, 0);

            var Vig = GetLayer("BackgroundTop").CreateSprite("sb/bgs/vignette.png");
            var Normal = GetLayer("Background").CreateSprite("sb/bgs/normal.png");
            var Dark = GetLayer("BackgroundBack").CreateSprite("sb/bgs/dark.png");
            var Blur = GetLayer("BackgroundBack").CreateSprite("sb/bgs/blur.png");
            Vig.Fade(8655, 33200, 1, 1);

            Normal.Fade(8655, 9928, 0, 1);
            Normal.Scale(8655, 480.0 / 1080);

            Vig.Fade(8655, 9928, 0, 1);
            Vig.Scale(8655, 480.0 / 1080);
            Vig.Fade(9928,0);

            int[] flashBrightTimes = {9928,10109,10291,10473,11564,11928,12109,12837,13018,13200,13382,14473,14837,15382,15746,
            20109,20837};

            foreach (var time in flashBrightTimes)
            {
                flashBright(time);
            }
            
            int[] flashBrightZoomTimes = {68291,68473,68655,
            69746,70109,70291,
            71018,71200,71382,71564,
            130837,131018,131200, 
            132291,132655,132837,
            133564,133746,133928,134109,
            135200,135382,135564,135655,135746,136109,136473,
            148109,148473,148837,149200,149564};
            foreach (var time in flashBrightZoomTimes)
            {
                flashBrightZoom(time);
            }

            int[] flashTimes = { 21564, 33200, 44837, 56473, 68109,119018 , 130655};

            foreach (var time in flashTimes)
            {
                flash(time);
            }

            Normal.Fade(9928, 15928, 1, 1);  //Normal Maintain
            Normal.Fade(15928, 21564, 1, 0.3); //Normal End
            Normal.Fade(21564,0);
            //section 2 bg movement
            Dark.Fade(21564, 33200, 1, 1);
            Dark.Scale(21564, 520.0 / 1080);
            Dark.Move(21564, 33200, 325, 250, 285, 235);
            Dark.Move(33201,320,240); //reset
            Dark.Fade(33200,0);
            //section 3 osculation
            osculationDark(33200, 42291);

            osculationKiai(44837, 56473);
            osculationKiai(56473, 68109);
            //section 4
            Normal.Scale(68109, 520.0 / 1080);
            Normal.Fade(68109,79018,1,1);
            Dark.Scale(79018, 520.0 / 1080);
            Dark.Fade(79018,103018,1,1);
            Normal.Fade(79018,79746,1,0);
            Dark.Fade(103018,103564,1,0);

            Pulse(73928,78655); 
            PulseSingle(78291);
            PulseSingle(79018);
            PulseSingle(79746);

            osculationKiai(107382,119018);
            osculationKiai(119018,130291); 

            Vig.Fade(136473,141200,0,1);
            Vig.Fade(141200,142291,1,1);
            Vig.Fade(142291,147928,1,0);
            Normal.Scale(130655,520.0 / 1080);
            Normal.Fade(130655,136473,1,1);
            Blur.Fade(136473,141200,1,1);
            Blur.Scale(136473, 520.0 / 1080);
            Normal.Fade(136473,141200,1,0);
            Blur.Fade(141200,142291,1,0);
            Dark.Fade(141200,151018,1,1);

            ending(149928);




        }
        public void ending(int now){ //now is final at 149928
            var t = GetLayer("BackgroundTop").CreateSprite("sb/bgs/top.png");
            var b = GetLayer("BackgroundTop").CreateSprite("sb/bgs/bottem.png");
            t.Fade(now,1);
            b.Fade(now,1);
            t.Scale(now, 480.0 / 1080);
            b.Scale(now, 480.0 / 1080);
            t.MoveY(OsbEasing.InExpo,now,150291,0,240);
            t.MoveY(150200,152473,240,240);
            b.MoveY(OsbEasing.InExpo,now,150291,480,240);
            b.MoveY(150291,152473,240,240);
            t.Fade(151018,152473,1,0);
            b.Fade(151018,152473,1,0);
        }
        public void flash(int now)
        {
            var White = GetLayer("BackgroundTop").CreateSprite("sb/bgs/flash.png");
            White.Scale(now, 480.0 / 1080);
            White.Fade(OsbEasing.OutCubic, now, now + 1200, 1, 0);
        }
        public void flashBright(int now)
        {
            var Bright = GetLayer("Background").CreateSprite("sb/bgs/bright.png");
            Bright.Scale(now, 480.0 / 1080);
            Bright.Fade(OsbEasing.OutCubic, now, now + 1200, 1, 0);
        }
        public void flashBrightZoom(int now)
        {
            var Bright = GetLayer("Background").CreateSprite("sb/bgs/bright.png");
            Bright.Scale(now, 520.0 / 1080);
            Bright.Fade(OsbEasing.OutCubic, now, now + 1200, 1, 0);
        }
        public void osculationDark(int Start, int End)
        {
            var Background = GetLayer("Background").CreateSprite("sb/bgs/dark.png");
            var beat = Beatmap.GetTimingPointAt(Start).BeatDuration;
            Background.Scale(Start, 520.0 / 1080);
            double rotationStart = 0.05;
            double rotationEnd = -0.05;
            double temp;
            for (double i = Start; i < End; i += beat * 4)
            {
                Background.Rotate(i, i + beat * 4, rotationStart, rotationEnd);
                temp = rotationStart;
                rotationStart = rotationEnd;
                rotationEnd = temp;
            }
        }
        void Pulse (int Start, int End){
            var beat1 = Beatmap.GetTimingPointAt(Start).BeatDuration;
            var Background = GetLayer("BackgroundTop").CreateSprite("sb/bgs/normal.png");

            for (double i = Start; i < End; i += beat1)
            {
                Background.Scale(OsbEasing.OutQuad, i, i + beat1, 540.0 / 1080, 540.0 / 1080);
                Background.Fade(i, i + beat1, 0.2, 0);
            }
        }
        void PulseSingle (int Start){
            var beat1 = Beatmap.GetTimingPointAt(Start).BeatDuration;
            var Background = GetLayer("BackgroundTop").CreateSprite("sb/bgs/normal.png");
                Background.Scale(OsbEasing.OutQuad, Start, Start + beat1, 540.0 / 1080, 540.0 / 1080);
                Background.Fade(Start, Start + beat1, 0.2, 0);
        }
        public void osculationKiai(int Start, int End)
        {
            Random r = new Random();
            var Background = GetLayer("Background").CreateSprite("sb/bgs/normal.png");
            var beat = Beatmap.GetTimingPointAt(Start).BeatDuration;
            Background.Scale(Start, 520.0 / 1080);
            Vector2 posKeep = new Vector2(320, 240);
            Vector2 posNew = new Vector2(318, 238);
            Boolean isNegative = true;
            Boolean switchAxis = true;
            Boolean extra = false;
            for (double i = Start; i < End; i += beat)
            {
                Background.Move(OsbEasing.InCubic, i, i + beat, posKeep, posNew);
                posKeep = posNew;
                    if (isNegative && switchAxis && extra == false) //up right
                    {
                        int change = r.Next(1, 3);
                        posNew.X = posNew.X + change;
                        posNew.Y = posNew.Y + change;
                        //go to down left
                        isNegative = false;
                        switchAxis = true;
                    }
                    else if (isNegative==false && switchAxis && extra == false)//down left
                    {
                        int change = r.Next(-3, -1);
                        posNew.X = posNew.X + change;
                        posNew.Y = posNew.Y + change;
                        //go to down right
                        isNegative = false;
                        switchAxis = false;
                    }              
                    else if (isNegative && switchAxis==false && extra == false) //up left
                    {
                        int change = r.Next(1, 3);
                        posNew.X = posNew.X - change;
                        posNew.Y = posNew.Y + change;
                        //go to down left extra
                        isNegative = false;
                        switchAxis = true;
                        extra = true;
                    }
                    else if (isNegative==false && switchAxis==false && extra == false)//down right
                    {
                        int change = r.Next(-3, -1);
                        posNew.X = posNew.X - change;
                        posNew.Y = posNew.Y + change;
                        //go to up left
                        isNegative = true;
                        switchAxis = false;
                    }
                    else if (isNegative==false && switchAxis && extra)//down left extra
                    {
                        int change = r.Next(-3, -1);
                        posNew.X = posNew.X + change;
                        posNew.Y = posNew.Y + change;
                        //go to up right extra
                        isNegative = true;
                        switchAxis = true;
                        extra = true;
                    }
                    else if (isNegative && switchAxis && extra) //up right extra
                    {
                        int change = r.Next(1, 3);
                        posNew.X = posNew.X + change;
                        posNew.Y = posNew.Y + change;
                        //go to up left extra
                        isNegative = true;
                        switchAxis = false;
                        extra = true;
                    }
                    else if (isNegative && switchAxis==false && extra ) //up left extra
                    {
                        int change = r.Next(1, 3);
                        posNew.X = posNew.X - change;
                        posNew.Y = posNew.Y + change;
                        //go to down right extra
                        isNegative = false;
                        switchAxis = false;
                        extra = true;
                    }
                    else if (isNegative==false && switchAxis==false && extra)//down right extra
                    {
                        int change = r.Next(-3, -1);
                        posNew.X = posNew.X - change;
                        posNew.Y = posNew.Y + change;
                        //go to up right and restart loop
                        isNegative = true;
                        switchAxis = true;
                        extra = false;
                    }
                    
            }
        }
    }
}
    