using Microsoft.Msagl.Drawing;

namespace MSAGL.Frame.Utility
{
    public static class EdgeWeightExtension
    {
        public static double Weight(this Edge edge)
        {
            return double.Parse(edge.LabelText);
        }
    }
}
