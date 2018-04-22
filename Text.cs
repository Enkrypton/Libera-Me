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
    public class Text : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            SongName(473,1200,"Libera Me");
            SongName(2473,3746, Beatmap.Name); //changes per diff
            SongName(3746,5564,"Storyboarded by Enkrypton");
            SongName(5927,8473,"GDs by ezek, Gabe and IOException");

            SongName(103746,106655,"If so, we will paint the core to humanity");
        }
        void SongName(int Start, int End, string Name)
        {
            var font = LoadFont("sb/letters/" + Start, new FontDescription()
            {
                FontPath = "Lato-Light.ttf",
                FontSize = 26,
                Color = Color4.White,
                Padding = Vector2.Zero,
                //FontStyle = FontStyle.Regular,
            });
            GenerateSongName(font, false, Start, End, Name);
        }
        public void GenerateSongName(FontGenerator font, bool additive, int Start, int End, string Sentence)
        {
            double Beat = Beatmap.GetTimingPointAt(Start).BeatDuration;       
            float letterX = 320;
            var letterY = 300;
            var lineWidth = 0f;
            var lineHeight = 0f;
            float FontScale = 0.5f;
            int Delay = 0;
            OsbOrigin Origin = OsbOrigin.Centre;

            foreach (var letter in Sentence)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
            }
            var letterCenter = letterX;
            letterX = letterX - lineWidth * 0.5f;
            
            foreach (var letter in Sentence)
            {
                var texture = font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, letterY)
                        + texture.OffsetFor(Origin) * FontScale;

                    var sprite = GetLayer("Sentence").CreateSprite(texture.Path, Origin, position);
                    
                    sprite.Scale(OsbEasing.OutBack, Start  + Delay, Start  + Delay + 300, 0, FontScale);
                    sprite.Fade(Start  + Delay, Start  + Delay + 300, 0, 1);
                    sprite.Fade(End, End+300, 1, 0);

                    Delay += 60;
                }
                letterX += texture.BaseWidth * FontScale;
            }
        }
    }
}
