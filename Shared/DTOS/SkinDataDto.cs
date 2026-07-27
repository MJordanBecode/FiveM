using System;
using System.Collections.Generic;

namespace Shared.DTOS
{
    public class SkinDataDto
    {
        // =========================
        // VISAGE
        // =========================
        public class FaceDataDto
        {
            // Parents GTA
            public int FatherShape { get; set; }
            public int MotherShape { get; set; }
            public float ShapeMix { get; set; }

            public int FatherSkin { get; set; }
            public int MotherSkin { get; set; }
            public float SkinMix { get; set; }


            // Yeux
            public int EyeColor { get; set; }


            // Morphologie visage
            // Index GTA SetPedFaceFeature
            public Dictionary<int, float> FaceFeatures { get; set; } = new();


            // Overlays
            public Dictionary<int, OverlayDataDto> Overlays { get; set; } = new();
        }


        // =========================
        // CHEVEUX
        // =========================
        public class HairDataDto
        {
            public int Style { get; set; }
            public int Color { get; set; }
            public int HighlightColor { get; set; }
        }


        // =========================
        // VÊTEMENTS
        // =========================
        public class ClothesDataDto
        {
            /*
             Component GTA :
             
             0  Visage
             1  Masque
             2  Cheveux
             3  Bras
             4  Pantalon
             5  Sac
             6  Chaussures
             7  Accessoires
             8  T-Shirt
             9  Gilet
             10 Decals
             11 Veste
            */

            public Dictionary<int, ComponentDataDto> Components { get; set; } = new();
        }


        public class ComponentDataDto
        {
            public int Drawable { get; set; }
            public int Texture { get; set; }
        }



        // =========================
        // ACCESSOIRES
        // =========================
        public class PropsDataDto
        {
            /*
             0 Chapeau
             1 Lunettes
             2 Oreilles
             6 Montre
             7 Bracelet
            */

            public Dictionary<int, ComponentDataDto> Props { get; set; } = new();
        }



        // =========================
        // OVERLAYS GTA
        // =========================
        public class OverlayDataDto
        {
            public int Style { get; set; }
            public int Color { get; set; }
            public float Opacity { get; set; }
        }
    }
}