using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOS
{
    public class SkinDataDto
    {
        public class FaceDataDto
        {
            public int FatherShape { get; set; }
            public int MotherShape { get; set; }
            public float ShapeMix { get; set; } // De 0.0f à 1.0f
            public int FatherSkin { get; set; }
            public int MotherSkin { get; set; }
            public float SkinMix { get; set; }
        }

        public class HairDataDto
        {
            public int Style { get; set; }
            public int Color { get; set; }
            public int HighlightColor { get; set; }
        }

        public class ClothesDataDto
        {
            // Key = ComponentId (ex: 11 = Veste, 4 = Pantalon, 6 = Chaussures), Value = DrawableId
            public Dictionary<int, int> Components { get; set; } = new();
            public Dictionary<int, int> Textures { get; set; } = new();
        }
    }
}