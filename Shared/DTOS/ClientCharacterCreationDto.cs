using System.Collections.Generic;

namespace Shared.DTOS
{
    // Miroir exact de ce qu'envoie le NUI (script.js)
    public class ClientFaceDto
    {
        public int shapeFirstID { get; set; }
        public int shapeSecondID { get; set; }
        public float shapeMix { get; set; }
        public float skinMix { get; set; }
    }

    public class ClientHairDto
    {
        public int drawable { get; set; }
        public int texture { get; set; }
        public int color { get; set; }
        public int highlight { get; set; }
    }

    public class ClientClothesDto
    {
        public int torso { get; set; }
        public int torsoTexture { get; set; }
        public int pants { get; set; }
        public int pantsTexture { get; set; }
        public int shoes { get; set; }
        public int shoesTexture { get; set; }
    }
}