using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGraphicsProgram.Labs.LabTask2
{
    public sealed class ModelData : System.Object
    {
        public ModelData() : base() { }

        public System.Drawing.Point[] FaceBackground { get; private set; } = new Point[]
        {
            new Point(540, 320), new Point(515, 330), new Point(490, 335), new Point(460, 335),
            new Point(420, 335), new Point(385, 330), new Point(355, 320), new Point(325, 310),
            new Point(300, 300), new Point(280, 290), new Point(270, 265), new Point(255, 240),
            new Point(250, 190), new Point(260, 160), new Point(276, 125), new Point(285, 95),
            new Point(300, 75), new Point(325, 60), new Point(360, 60),

            new Point(400, 44), new Point(450, 48), new Point(500, 54), new Point(525, 80),
            new Point(532, 90), new Point(550, 94), new Point(568, 100), new Point(580, 112),
            new Point(590, 130), new Point(595, 150), new Point(590, 170),

            new Point(600, 200), new Point(600, 230), new Point(595, 260), new Point(585, 285),
            new Point(565, 310), new Point(540, 320),

        };
        public System.Drawing.Point[] LegsBackground { get; private set; } = new Point[]
        {
            new Point(540, 320), new Point(515, 330), new Point(490, 335), new Point(460, 335),
            new Point(420, 335), new Point(385, 330), new Point(355, 320), new Point(325, 310),
            new Point(300, 300), new Point(280, 290), new Point(270, 310), new Point(275, 345),
            new Point(275, 370), new Point(280, 395), new Point(290, 420), new Point(315, 430),
            new Point(340, 430),

            new Point(355, 415), new Point(355, 385), new Point(350, 350), new Point(340, 340),
            new Point(350, 350),

            new Point(380, 360), new Point(410, 360), new Point(440, 365), new Point(470, 360),
            new Point(484, 354), new Point(470, 360),

            new Point(475, 380), new Point(470, 410), new Point(475, 440),
            new Point(480, 455), new Point(500, 460), new Point(525, 455), new Point(540, 435),
            new Point(550, 405), new Point(550, 375), new Point(550, 375), new Point(545, 345),
            new Point(540, 320),

        };
        public System.Drawing.Point[] MouthBorder { get; private set; } = new Point[]
        {
            new Point(405, 270), new Point(425, 250), new Point(430, 225), new Point(425, 250),
            new Point(440, 275),

        };
        public System.Drawing.Point[] NouseBorder { get; private set; } = new Point[]
        {
            new Point(430, 220), new Point(420, 210), new Point(420, 206), new Point(428, 205),
            new Point(436, 206), new Point(442, 212), new Point(440, 214), new Point(433, 219)

        };
        public System.Drawing.Point[] LeftEarsBackground { get; private set; } = new Point[]
        {
            new Point(276, 125), new Point(282, 110), new Point(290, 95), new Point(298, 75),
            new Point(315, 65), new Point(334, 60), new Point(360, 60), new Point(360, 75),
            new Point(355, 95),new Point(345, 110), new Point(325, 122),

        };
        public System.Drawing.Point[] LeftEarsBorder { get; private set; } = new Point[]
        {
            new Point(355, 95),new Point(345, 110), new Point(325, 122), new Point(276, 125),

        };
        public System.Drawing.Point[] RightEarsBackground { get; private set; } = new Point[]
        {
            new Point(590, 170), new Point(595, 150), new Point(590, 130), new Point(580, 112),
            new Point(568, 100), new Point(550, 94), new Point(532, 90), new Point(526, 104),

            new Point(525, 124), new Point(530, 140), new Point(548, 158), new Point(566, 165),

        };
        public System.Drawing.Point[] RightEarsBorder { get; private set; } = new Point[]
        {
            new Point(590, 170), new Point(566, 165), new Point(548, 158), new Point(530, 140),
            new Point(525, 124),

        };
        public System.Drawing.Point[] SecondFaceBackground { get; private set; } = new Point[]
        {
            new Point(270, 274), new Point(290, 270), new Point(306, 265), new Point(325, 260),
            new Point(340, 248), new Point(358, 235), new Point(386, 180), new Point(395, 162),
            new Point(404, 138), new Point(412, 154), new Point(424, 166), new Point(434, 174),
            new Point(445, 170), new Point(456, 158), new Point(466, 145), new Point(468, 165),
            new Point(470, 184), new Point(474, 198), new Point(484, 250), new Point(490, 265),
            new Point(500, 280), new Point(515, 292), new Point(536, 302), new Point(568, 308),

            new Point(565, 310), new Point(540, 320), new Point(515, 330), new Point(490, 335),
            new Point(460, 335), new Point(420, 335), new Point(385, 330), new Point(355, 320),
            new Point(325, 310), new Point(300, 300), new Point(280, 290)
        };
        public System.Drawing.Point[][] BodyHairBackground { get; private set; } = new Point[][]
        {
            new Point[3] { new Point(375, 54), new Point(390, 98), new Point(394, 48) },
            new Point[3] { new Point(432, 45), new Point(436, 115), new Point(458, 50) },
            new Point[3] { new Point(494, 52), new Point(486, 108), new Point(508, 60) },

            new Point[3] { new Point(351, 356), new Point(315, 347), new Point(352, 370) },
            new Point[3] { new Point(354, 380), new Point(318, 369), new Point(355, 394) },

            new Point[3] { new Point(475, 375), new Point(520, 364), new Point(477, 389) },
            new Point[3] { new Point(472, 395), new Point(521, 386), new Point(472, 409) },
        };
    }
}
